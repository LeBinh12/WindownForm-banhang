using System;
using System.Collections.Generic;

namespace QuanLyCuaHangTapHoa.Domain
{
    public class NguoiDung
    {
        public string MaNguoiDung { get; set; }
        public string HoTen { get; set; }
        public string Email { get; set; }
        public string SoDienThoai { get; set; }
        public string DiaChi { get; set; }

        // Navigation
        public virtual TaiKhoan TaiKhoan { get; set; }
    }

    public class KhachHang : NguoiDung
    {
        public string MaKhachHang { get; set; }
        public DateTime NgayDangKy { get; set; }

        // Navigation
        public virtual ICollection<DonDatHang> DonDatHangs { get; set; } = new List<DonDatHang>();
    }

    public class NhanVien : NguoiDung
    {
        public string MaNhanVien { get; set; }
        public string ChucVu { get; set; }

        // Navigation
        public virtual ICollection<HoaDon> HoaDons { get; set; } = new List<HoaDon>();
    }

    public class Admin : NhanVien
    {
        // No additional fields
    }

    public class TaiKhoan
    {
        public string MaNguoiDung { get; set; } // Primary Key & Foreign Key to NguoiDung
        public string MaTaiKhoan { get; set; }
        public string TenDangNhap { get; set; }
        public string MatKhau { get; set; }
        public TrangThaiTaiKhoan TrangThaiTaiKhoan { get; set; }

        // Navigation
        public virtual NguoiDung NguoiDung { get; set; }
    }

    public class SanPham
    {
        public string MaSP { get; set; }
        public string TenSP { get; set; }
        public string DanhMuc { get; set; }
        public decimal DonGia { get; set; }
        public int SoLuongTon { get; set; }
        public TrangThaiSanPham TrangThaiSanPham { get; set; }

        // Navigation
        public virtual ICollection<ChiTietDonHang> ChiTietDonHangs { get; set; } = new List<ChiTietDonHang>();
    }

    public class DonDatHang
    {
        public string MaDon { get; set; }
        public DateTime NgayDat { get; set; }
        public DateTime? NgayDuyet { get; set; }
        public TrangThaiDonHang TrangThaiDonHang { get; set; }
        public string MaNguoiDungKhachHang { get; set; }

        // Navigation
        public virtual KhachHang KhachHang { get; set; }
        public virtual ICollection<ChiTietDonHang> ChiTietDonHangs { get; set; } = new List<ChiTietDonHang>();
        public virtual ICollection<HoaDon> HoaDons { get; set; } = new List<HoaDon>();
    }

    public class ChiTietDonHang
    {
        public string MaDon { get; set; }
        public string MaSP { get; set; }
        public int SoLuong { get; set; }
        public decimal DonGia { get; set; }

        // Navigation
        public virtual DonDatHang DonDatHang { get; set; }
        public virtual SanPham SanPham { get; set; }
    }

    public class HoaDon
    {
        public string MaHoaDon { get; set; }
        public DateTime NgayLap { get; set; }
        public decimal TongTien { get; set; }
        public string MaDon { get; set; }
        public string MaNguoiDungNhanVien { get; set; }

        // Navigation
        public virtual DonDatHang DonDatHang { get; set; }
        public virtual NhanVien NhanVien { get; set; }
    }
}
