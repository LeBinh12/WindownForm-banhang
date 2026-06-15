using QuanLyCuaHangTapHoa.Application.Interfaces;
using QuanLyCuaHangTapHoa.Domain;
using QuanLyCuaHangTapHoa.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace QuanLyCuaHangTapHoa.Application.UseCases
{
    public class ProductUseCase : IProductUseCase
    {
        private readonly IProductRepository _productRepository;

        public ProductUseCase(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public List<SanPham> SearchProducts(string keyword, string category)
        {
            var products = _productRepository.GetAll();

            // Business Rule BR-01: Only SanSang and SoLuongTon > 0 products are searchable for customers
            var query = products.Where(p => p.TrangThaiSanPham == TrangThaiSanPham.SanSang && p.SoLuongTon > 0);

            if (!string.IsNullOrWhiteSpace(keyword))
            {
                var lowerKeyword = keyword.ToLower();
                query = query.Where(p => p.TenSP.ToLower().Contains(lowerKeyword) || p.MaSP.ToLower().Contains(lowerKeyword));
            }

            if (!string.IsNullOrWhiteSpace(category) && category != "Tất cả")
            {
                var lowerCategory = category.ToLower();
                query = query.Where(p => p.DanhMuc.ToLower() == lowerCategory);
            }

            return query.ToList();
        }

        public List<SanPham> GetAllProducts()
        {
            return _productRepository.GetAll();
        }

        public SanPham GetProductById(string id)
        {
            return _productRepository.GetById(id);
        }

        public void AddProduct(SanPham product)
        {
            if (string.IsNullOrWhiteSpace(product.MaSP))
                throw new ArgumentException("Mã sản phẩm không được trống.");
            if (string.IsNullOrWhiteSpace(product.TenSP))
                throw new ArgumentException("Tên sản phẩm không được trống.");
            if (product.DonGia <= 0)
                throw new ArgumentException("Đơn giá phải lớn hơn 0.");
            if (product.SoLuongTon < 0)
                throw new ArgumentException("Số lượng tồn không được âm.");

            var existing = _productRepository.GetById(product.MaSP);
            if (existing != null)
                throw new InvalidOperationException("Mã sản phẩm đã tồn tại.");

            _productRepository.Add(product);
        }

        public void UpdateProduct(SanPham product)
        {
            if (string.IsNullOrWhiteSpace(product.TenSP))
                throw new ArgumentException("Tên sản phẩm không được trống.");
            if (product.DonGia <= 0)
                throw new ArgumentException("Đơn giá phải lớn hơn 0.");
            if (product.SoLuongTon < 0)
                throw new ArgumentException("Số lượng tồn không được âm.");

            var existing = _productRepository.GetById(product.MaSP);
            if (existing == null)
                throw new KeyNotFoundException("Không tìm thấy sản phẩm cần sửa.");

            existing.TenSP = product.TenSP;
            existing.DanhMuc = product.DanhMuc;
            existing.DonGia = product.DonGia;
            existing.SoLuongTon = product.SoLuongTon;
            existing.TrangThaiSanPham = product.TrangThaiSanPham;

            _productRepository.Update(existing);
        }

        public void DeleteProduct(string id)
        {
            var existing = _productRepository.GetById(id);
            if (existing == null)
                throw new KeyNotFoundException("Không tìm thấy sản phẩm cần xóa.");

            _productRepository.Delete(id);
        }

        public void UpdateInventory(string productId, int newQuantity, TrangThaiSanPham status)
        {
            var product = _productRepository.GetById(productId);
            if (product == null)
                throw new KeyNotFoundException("Không tìm thấy sản phẩm.");

            product.SoLuongTon = newQuantity;
            product.TrangThaiSanPham = status;
            _productRepository.Update(product);
        }
    }
}
