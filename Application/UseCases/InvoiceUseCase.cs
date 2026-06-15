using QuanLyCuaHangTapHoa.Application.Interfaces;
using QuanLyCuaHangTapHoa.Domain;
using QuanLyCuaHangTapHoa.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace QuanLyCuaHangTapHoa.Application.UseCases
{
    public class InvoiceUseCase : IInvoiceUseCase
    {
        private readonly IInvoiceRepository _invoiceRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IProductRepository _productRepository;

        public InvoiceUseCase(
            IInvoiceRepository invoiceRepository,
            IOrderRepository orderRepository,
            IProductRepository productRepository)
        {
            _invoiceRepository = invoiceRepository;
            _orderRepository = orderRepository;
            _productRepository = productRepository;
        }

        public HoaDon CreateInvoice(string orderId, string nhanVienId)
        {
            var order = _orderRepository.GetById(orderId);
            if (order == null)
                throw new KeyNotFoundException("Không tìm thấy đơn đặt hàng.");
            if (order.TrangThaiDonHang != TrangThaiDonHang.DaDuyet)
                throw new InvalidOperationException("Chỉ có thể thanh toán đơn hàng đã được duyệt.");

            // Calculate total price
            decimal total = order.ChiTietDonHangs.Sum(ct => ct.SoLuong * ct.DonGia);

            // Create Invoice
            var invoice = new HoaDon
            {
                MaHoaDon = "HD" + DateTime.Now.ToString("yyyyMMddHHmmss"),
                NgayLap = DateTime.Now,
                TongTien = total,
                MaDon = order.MaDon,
                MaNguoiDungNhanVien = nhanVienId
            };

            // Update order status to DaThanhToan
            order.TrangThaiDonHang = TrangThaiDonHang.DaThanhToan;
            _orderRepository.Update(order);

            // Update product status
            foreach (var detail in order.ChiTietDonHangs)
            {
                var product = _productRepository.GetById(detail.MaSP);
                if (product != null)
                {
                    product.TrangThaiSanPham = product.SoLuongTon > 0 ? TrangThaiSanPham.SanSang : TrangThaiSanPham.HetHang;
                    _productRepository.Update(product);
                }
            }

            _invoiceRepository.Add(invoice);
            return invoice;
        }

        public HoaDon CreatePOSInvoice(string nhanVienId, List<(string productId, int quantity)> items)
        {
            if (items == null || !items.Any())
                throw new ArgumentException("Hóa đơn bán lẻ phải có ít nhất một sản phẩm.");

            // Direct POS sales require creating a backend order first to maintain foreign key constraints
            var order = new DonDatHang
            {
                MaDon = "DH" + DateTime.Now.ToString("yyyyMMddHHmmss"),
                NgayDat = DateTime.Now,
                TrangThaiDonHang = TrangThaiDonHang.DaThanhToan,
                MaNguoiDungKhachHang = "ND003", // Default Customer (Le Van Khach Hang seeded)
                ChiTietDonHangs = new List<ChiTietDonHang>()
            };

            decimal total = 0;

            foreach (var item in items)
            {
                var product = _productRepository.GetById(item.productId);
                if (product == null)
                    throw new KeyNotFoundException($"Không tìm thấy sản phẩm {item.productId}.");
                if (product.TrangThaiSanPham != TrangThaiSanPham.SanSang && product.TrangThaiSanPham != TrangThaiSanPham.ChoXuatKho)
                    throw new InvalidOperationException($"Sản phẩm {product.TenSP} không sẵn sàng để bán lẻ.");
                if (product.SoLuongTon < item.quantity)
                    throw new InvalidOperationException($"Sản phẩm {product.TenSP} không đủ số lượng tồn (Yêu cầu: {item.quantity}, Tồn: {product.SoLuongTon}).");

                // Deduct stock and set product status
                product.SoLuongTon -= item.quantity;
                product.TrangThaiSanPham = product.SoLuongTon > 0 ? TrangThaiSanPham.SanSang : TrangThaiSanPham.HetHang;
                _productRepository.Update(product);

                order.ChiTietDonHangs.Add(new ChiTietDonHang
                {
                    MaDon = order.MaDon,
                    MaSP = product.MaSP,
                    SoLuong = item.quantity,
                    DonGia = product.DonGia
                });

                total += item.quantity * product.DonGia;
            }

            // Save the backing order
            _orderRepository.Add(order);

            // Create Invoice
            var invoice = new HoaDon
            {
                MaHoaDon = "HD" + DateTime.Now.ToString("yyyyMMddHHmmss"),
                NgayLap = DateTime.Now,
                TongTien = total,
                MaDon = order.MaDon,
                MaNguoiDungNhanVien = nhanVienId
            };

            _invoiceRepository.Add(invoice);
            Console.WriteLine($"[LUỒNG B POS] Đã tạo đơn bán lẻ trực tiếp {order.MaDon} với trạng thái {order.TrangThaiDonHang}");
            return invoice;
        }

        public void ReturnProduct(string invoiceId, string productId, int returnQty, string reason)
        {
            var invoice = _invoiceRepository.GetById(invoiceId);
            if (invoice == null)
                throw new KeyNotFoundException("Không tìm thấy hóa đơn.");

            // Business Rule BR-06: Refund/Return allowed only within 7 days
            if ((DateTime.Now - invoice.NgayLap).TotalDays > 7)
                throw new InvalidOperationException("Hóa đơn đã quá 7 ngày, không thể thực hiện đổi trả.");

            var order = _orderRepository.GetById(invoice.MaDon);
            if (order == null)
                throw new InvalidOperationException("Không tìm thấy thông tin đơn hàng tương ứng với hóa đơn.");

            var detail = order.ChiTietDonHangs.FirstOrDefault(ct => ct.MaSP == productId);
            if (detail == null)
                throw new InvalidOperationException("Sản phẩm không có trong hóa đơn này.");
            if (detail.SoLuong < returnQty)
                throw new ArgumentException("Số lượng trả lớn hơn số lượng đã mua.");

            var product = _productRepository.GetById(productId);
            if (product != null)
            {
                // Handle returned goods
                product.SoLuongTon += returnQty;
                
                // If it's return because of fault, set status to Hong, otherwise back to SanSang
                if (reason.ToLower().Contains("hỏng") || reason.ToLower().Contains("lỗi"))
                {
                    product.TrangThaiSanPham = TrangThaiSanPham.Hong;
                }
                else
                {
                    product.TrangThaiSanPham = TrangThaiSanPham.SanSang;
                }
                _productRepository.Update(product);
            }

            // Deduct detail and adjust invoice total
            detail.SoLuong -= returnQty;
            invoice.TongTien -= returnQty * detail.DonGia;

            _orderRepository.Update(order);
            _invoiceRepository.Update(invoice);
        }

        public List<HoaDon> GetAllInvoices()
        {
            return _invoiceRepository.GetAll();
        }

        public HoaDon GetInvoiceById(string id)
        {
            return _invoiceRepository.GetById(id);
        }
    }
}
