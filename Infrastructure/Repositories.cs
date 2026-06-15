using Microsoft.EntityFrameworkCore;
using QuanLyCuaHangTapHoa.Domain;
using QuanLyCuaHangTapHoa.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace QuanLyCuaHangTapHoa.Infrastructure
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext _context;

        public ProductRepository(AppDbContext context)
        {
            _context = context;
        }

        public List<SanPham> GetAll()
        {
            _context.ChangeTracker.Clear();
            return _context.SanPhams.ToList();
        }

        public SanPham GetById(string id)
        {
            _context.ChangeTracker.Clear();
            return _context.SanPhams.FirstOrDefault(s => s.MaSP == id);
        }

        public void Add(SanPham product)
        {
            _context.SanPhams.Add(product);
            _context.SaveChanges();
        }

        public void Update(SanPham product)
        {
            _context.Entry(product).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void Delete(string id)
        {
            var product = GetById(id);
            if (product != null)
            {
                // Soft delete by changing status to NgungKinhDoanh
                product.TrangThaiSanPham = TrangThaiSanPham.NgungKinhDoanh;
                Update(product);
            }
        }
    }

    public class OrderRepository : IOrderRepository
    {
        private readonly AppDbContext _context;

        public OrderRepository(AppDbContext context)
        {
            _context = context;
        }

        public List<DonDatHang> GetAll()
        {
            _context.ChangeTracker.Clear();
            return _context.DonDatHangs
                .Include(d => d.KhachHang)
                .ThenInclude(kh => kh.TaiKhoan)
                .Include(d => d.ChiTietDonHangs)
                .ThenInclude(ct => ct.SanPham)
                .ToList();
        }

        public DonDatHang GetById(string id)
        {
            _context.ChangeTracker.Clear();
            return _context.DonDatHangs
                .Include(d => d.KhachHang)
                .ThenInclude(kh => kh.TaiKhoan)
                .Include(d => d.ChiTietDonHangs)
                .ThenInclude(ct => ct.SanPham)
                .FirstOrDefault(d => d.MaDon == id);
        }

        public void Add(DonDatHang order)
        {
            _context.DonDatHangs.Add(order);
            _context.SaveChanges();
        }

        public void Update(DonDatHang order)
        {
            _context.Entry(order).State = EntityState.Modified;
            _context.SaveChanges();
        }
    }

    public class InvoiceRepository : IInvoiceRepository
    {
        private readonly AppDbContext _context;

        public InvoiceRepository(AppDbContext context)
        {
            _context = context;
        }

        public List<HoaDon> GetAll()
        {
            _context.ChangeTracker.Clear();
            return _context.HoaDons
                .Include(h => h.DonDatHang)
                .Include(h => h.NhanVien)
                .ToList();
        }

        public HoaDon GetById(string id)
        {
            _context.ChangeTracker.Clear();
            return _context.HoaDons
                .Include(h => h.DonDatHang)
                .Include(h => h.NhanVien)
                .FirstOrDefault(h => h.MaHoaDon == id);
        }

        public void Add(HoaDon invoice)
        {
            _context.HoaDons.Add(invoice);
            _context.SaveChanges();
        }

        public void Update(HoaDon invoice)
        {
            _context.Entry(invoice).State = EntityState.Modified;
            _context.SaveChanges();
        }
    }

    public class AccountRepository : IAccountRepository
    {
        private readonly AppDbContext _context;

        public AccountRepository(AppDbContext context)
        {
            _context = context;
        }

        public List<TaiKhoan> GetAll()
        {
            _context.ChangeTracker.Clear();
            return _context.TaiKhoans
                .Include(tk => tk.NguoiDung)
                .ToList();
        }

        public TaiKhoan GetByUsername(string username)
        {
            _context.ChangeTracker.Clear();
            var tk = _context.TaiKhoans
                .Include(tk => tk.NguoiDung)
                .FirstOrDefault(tk => tk.TenDangNhap == username);
            if (tk != null) ResolveNguoiDungType(tk);
            return tk;
        }

        public TaiKhoan GetByUserId(string userId)
        {
            _context.ChangeTracker.Clear();
            var tk = _context.TaiKhoans
                .Include(tk => tk.NguoiDung)
                .FirstOrDefault(tk => tk.MaNguoiDung == userId);
            if (tk != null) ResolveNguoiDungType(tk);
            return tk;
        }

        /// <summary>
        /// EF Core TPT: Include(tk => tk.NguoiDung) only joins NguoiDungs table
        /// and returns the base NguoiDung type. We must explicitly load from the
        /// derived DbSets (Admins > NhanViens > KhachHangs) to get the correct
        /// runtime type so that `NguoiDung is Admin` etc. work throughout the app.
        /// </summary>
        private void ResolveNguoiDungType(TaiKhoan tk)
        {
            var id = tk.MaNguoiDung;

            // Admin inherits NhanVien — check most-derived first
            var admin = _context.Admins.FirstOrDefault(a => a.MaNguoiDung == id);
            if (admin != null) { tk.NguoiDung = admin; return; }

            var nhanVien = _context.NhanViens.FirstOrDefault(nv => nv.MaNguoiDung == id);
            if (nhanVien != null) { tk.NguoiDung = nhanVien; return; }

            var khachHang = _context.KhachHangs.FirstOrDefault(kh => kh.MaNguoiDung == id);
            if (khachHang != null) { tk.NguoiDung = khachHang; }
            // else: stays as base NguoiDung (unknown role)
        }

        public void Add(NguoiDung user, TaiKhoan account)
        {
            _context.NguoiDungs.Add(user);
            _context.TaiKhoans.Add(account);
            _context.SaveChanges();
        }

        public void Update(NguoiDung user, TaiKhoan account)
        {
            _context.Entry(user).State = EntityState.Modified;
            _context.Entry(account).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void UpdateAccountState(string userId, TrangThaiTaiKhoan state)
        {
            var tk = _context.TaiKhoans.FirstOrDefault(t => t.MaNguoiDung == userId);
            if (tk != null)
            {
                tk.TrangThaiTaiKhoan = state;
                _context.SaveChanges();
            }
        }
    }
}
