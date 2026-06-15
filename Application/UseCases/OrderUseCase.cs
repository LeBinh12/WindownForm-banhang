using QuanLyCuaHangTapHoa.Application.Interfaces;
using QuanLyCuaHangTapHoa.Domain;
using QuanLyCuaHangTapHoa.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace QuanLyCuaHangTapHoa.Application.UseCases
{
    public class OrderUseCase : IOrderUseCase
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IProductRepository _productRepository;

        public OrderUseCase(IOrderRepository orderRepository, IProductRepository productRepository)
        {
            _orderRepository = orderRepository;
            _productRepository = productRepository;
        }

        public DonDatHang CreateOrder(string khachHangId, List<(string productId, int quantity)> items)
        {
            if (items == null || !items.Any())
                throw new ArgumentException("Đơn hàng phải có ít nhất một sản phẩm.");

            // Create new order
            var order = new DonDatHang
            {
                MaDon = "DH" + DateTime.Now.ToString("yyyyMMddHHmmss"),
                NgayDat = DateTime.Now,
                TrangThaiDonHang = TrangThaiDonHang.ChoDuyet,
                MaNguoiDungKhachHang = khachHangId,
                ChiTietDonHangs = new List<ChiTietDonHang>()
            };

            foreach (var item in items)
            {
                var product = _productRepository.GetById(item.productId);
                
                // Business Rule BR-01: Only SanSang and SoLuongTon > 0 can be ordered
                if (product == null)
                    throw new KeyNotFoundException($"Không tìm thấy sản phẩm {item.productId}.");
                if (product.TrangThaiSanPham != TrangThaiSanPham.SanSang)
                    throw new InvalidOperationException($"Sản phẩm {product.TenSP} không ở trạng thái Sẵn sàng.");
                if (product.SoLuongTon < item.quantity)
                    throw new InvalidOperationException($"Sản phẩm {product.TenSP} không đủ số lượng tồn (Yêu cầu: {item.quantity}, Tồn: {product.SoLuongTon}).");

                // Deduct inventory and move product state to ChoXuatKho
                product.SoLuongTon -= item.quantity;
                product.TrangThaiSanPham = TrangThaiSanPham.ChoXuatKho;
                _productRepository.Update(product);

                // Add detail
                order.ChiTietDonHangs.Add(new ChiTietDonHang
                {
                    MaDon = order.MaDon,
                    MaSP = product.MaSP,
                    SoLuong = item.quantity,
                    DonGia = product.DonGia
                });
            }

            _orderRepository.Add(order);
            Console.WriteLine($"[LUỒNG A ĐẶT GIỮ] Đã tạo đơn đặt giữ {order.MaDon} với trạng thái {order.TrangThaiDonHang}");
            return order;
        }

        public void ApproveOrder(string orderId)
        {
            var order = _orderRepository.GetById(orderId);
            if (order == null)
                throw new KeyNotFoundException("Không tìm thấy đơn hàng.");
            if (order.TrangThaiDonHang != TrangThaiDonHang.ChoDuyet)
                throw new InvalidOperationException("Chỉ có thể duyệt đơn hàng ở trạng thái Chờ duyệt.");

            order.TrangThaiDonHang = TrangThaiDonHang.DaDuyet;
            order.NgayDuyet = DateTime.Now;

            _orderRepository.Update(order);
        }

        public void CancelOrRejectOrder(string orderId, bool isReject)
        {
            var order = _orderRepository.GetById(orderId);
            if (order == null)
                throw new KeyNotFoundException("Không tìm thấy đơn hàng.");
            if (order.TrangThaiDonHang != TrangThaiDonHang.ChoDuyet && order.TrangThaiDonHang != TrangThaiDonHang.DaDuyet)
                throw new InvalidOperationException("Chỉ có thể hủy hoặc từ chối đơn hàng ở trạng thái Chờ duyệt hoặc Đã duyệt.");

            // Update order status
            order.TrangThaiDonHang = isReject ? TrangThaiDonHang.TuChoi : TrangThaiDonHang.DaHuy;

            // Return items back to SanSang
            foreach (var detail in order.ChiTietDonHangs)
            {
                var product = _productRepository.GetById(detail.MaSP);
                if (product != null)
                {
                    product.SoLuongTon += detail.SoLuong;
                    product.TrangThaiSanPham = TrangThaiSanPham.SanSang;
                    _productRepository.Update(product);
                }
            }

            _orderRepository.Update(order);
        }

        public List<DonDatHang> GetAllOrders()
        {
            return _orderRepository.GetAll();
        }

        public List<DonDatHang> GetOrdersByCustomer(string khachHangId)
        {
            return _orderRepository.GetAll().Where(o => o.MaNguoiDungKhachHang == khachHangId).ToList();
        }

        public void CheckAndCancelExpiredOrders()
        {
            // Business Rule BR-05: Reservation holds for max 3 days from approval
            var expiredOrders = _orderRepository.GetAll()
                .Where(o => o.TrangThaiDonHang == TrangThaiDonHang.DaDuyet && 
                            o.NgayDuyet.HasValue && 
                            (DateTime.Now - o.NgayDuyet.Value).TotalDays > 3)
                .ToList();

            foreach (var order in expiredOrders)
            {
                // Auto cancel expired order
                order.TrangThaiDonHang = TrangThaiDonHang.DaHuy;
                
                foreach (var detail in order.ChiTietDonHangs)
                {
                    var product = _productRepository.GetById(detail.MaSP);
                    if (product != null)
                    {
                        product.SoLuongTon += detail.SoLuong;
                        product.TrangThaiSanPham = TrangThaiSanPham.SanSang;
                        _productRepository.Update(product);
                    }
                }
                
                _orderRepository.Update(order);
            }
        }

        public DonDatHang GetById(string orderId)
        {
            return _orderRepository.GetById(orderId);
        }

        public List<DonDatHang> GetByStatus(TrangThaiDonHang status)
        {
            return _orderRepository.GetAll().Where(o => o.TrangThaiDonHang == status).ToList();
        }
    }
}
