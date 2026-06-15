using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using QuanLyCuaHangTapHoa.Domain;
using System;
using System.IO;

namespace QuanLyCuaHangTapHoa.Infrastructure
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<NguoiDung> NguoiDungs { get; set; }
        public DbSet<KhachHang> KhachHangs { get; set; }
        public DbSet<NhanVien> NhanViens { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<TaiKhoan> TaiKhoans { get; set; }
        public DbSet<SanPham> SanPhams { get; set; }
        public DbSet<DonDatHang> DonDatHangs { get; set; }
        public DbSet<ChiTietDonHang> ChiTietDonHangs { get; set; }
        public DbSet<HoaDon> HoaDons { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Table Per Type (TPT) Mapping
            modelBuilder.Entity<NguoiDung>().ToTable("NguoiDungs");
            modelBuilder.Entity<KhachHang>().ToTable("KhachHangs");
            modelBuilder.Entity<NhanVien>().ToTable("NhanViens");
            modelBuilder.Entity<Admin>().ToTable("Admins");

            // NguoiDung Fluent API
            modelBuilder.Entity<NguoiDung>(entity =>
            {
                entity.HasKey(nd => nd.MaNguoiDung);
                entity.Property(nd => nd.HoTen).IsRequired().HasMaxLength(100);
                entity.Property(nd => nd.Email).IsRequired().HasMaxLength(100);
                entity.HasIndex(nd => nd.Email).IsUnique();
                entity.Property(nd => nd.SoDienThoai).IsRequired().HasMaxLength(15);
                entity.HasIndex(nd => nd.SoDienThoai).IsUnique();
                entity.Property(nd => nd.DiaChi).HasMaxLength(250).IsRequired(false);
            });

            // KhachHang Fluent API
            modelBuilder.Entity<KhachHang>(entity =>
            {
                entity.Property(kh => kh.MaKhachHang).IsRequired().HasMaxLength(50);
                entity.HasIndex(kh => kh.MaKhachHang).IsUnique();
            });

            // NhanVien Fluent API
            modelBuilder.Entity<NhanVien>(entity =>
            {
                entity.Property(nv => nv.MaNhanVien).IsRequired().HasMaxLength(50);
                entity.HasIndex(nv => nv.MaNhanVien).IsUnique();
                entity.Property(nv => nv.ChucVu).IsRequired().HasMaxLength(50);
            });

            // TaiKhoan 1-1 with NguoiDung
            modelBuilder.Entity<TaiKhoan>(entity =>
            {
                entity.ToTable("TaiKhoans");
                entity.HasKey(tk => tk.MaNguoiDung);

                entity.HasOne(tk => tk.NguoiDung)
                    .WithOne(nd => nd.TaiKhoan)
                    .HasForeignKey<TaiKhoan>(tk => tk.MaNguoiDung)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.Property(tk => tk.MaTaiKhoan).IsRequired().HasMaxLength(50);
                entity.HasIndex(tk => tk.MaTaiKhoan).IsUnique();

                entity.Property(tk => tk.TenDangNhap).IsRequired().HasMaxLength(50);
                entity.HasIndex(tk => tk.TenDangNhap).IsUnique();

                entity.Property(tk => tk.MatKhau).IsRequired().HasMaxLength(250);
            });

            // SanPham
            modelBuilder.Entity<SanPham>(entity =>
            {
                entity.ToTable("SanPhams");
                entity.HasKey(sp => sp.MaSP);
                entity.Property(sp => sp.TenSP).IsRequired().HasMaxLength(100);
                entity.Property(sp => sp.DanhMuc).IsRequired().HasMaxLength(50);
                entity.Property(sp => sp.DonGia).HasColumnType("decimal(18,2)");
            });

            // DonDatHang
            modelBuilder.Entity<DonDatHang>(entity =>
            {
                entity.ToTable("DonDatHangs");
                entity.HasKey(ddh => ddh.MaDon);

                entity.HasOne(ddh => ddh.KhachHang)
                    .WithMany(kh => kh.DonDatHangs)
                    .HasForeignKey(ddh => ddh.MaNguoiDungKhachHang)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // ChiTietDonHang (Composite Key)
            modelBuilder.Entity<ChiTietDonHang>(entity =>
            {
                entity.ToTable("ChiTietDonHangs");
                entity.HasKey(ct => new { ct.MaDon, ct.MaSP });

                entity.HasOne(ct => ct.DonDatHang)
                    .WithMany(ddh => ddh.ChiTietDonHangs)
                    .HasForeignKey(ct => ct.MaDon)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(ct => ct.SanPham)
                    .WithMany(sp => sp.ChiTietDonHangs)
                    .HasForeignKey(ct => ct.MaSP)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.Property(ct => ct.DonGia).HasColumnType("decimal(18,2)");
            });

            // HoaDon
            modelBuilder.Entity<HoaDon>(entity =>
            {
                entity.ToTable("HoaDons");
                entity.HasKey(hd => hd.MaHoaDon);
                entity.Property(hd => hd.TongTien).HasColumnType("decimal(18,2)");

                entity.HasOne(hd => hd.DonDatHang)
                    .WithMany(ddh => ddh.HoaDons)
                    .HasForeignKey(hd => hd.MaDon)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(hd => hd.NhanVien)
                    .WithMany(nv => nv.HoaDons)
                    .HasForeignKey(hd => hd.MaNguoiDungNhanVien)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // Seed Data
            SeedData(modelBuilder);
        }

        private void SeedData(ModelBuilder modelBuilder)
        {
            // Seed Hashed Passwords
            // admin123 -> $2a$11$8m5Kx3wHhH5kQ/WlB2V7/O9m5Gf2hXv41QoG4tB2W5fG.48z6P14a (BCrypt)
            // staff123 -> $2a$11$V6R94yH3wH5kQ/WlB2V7/O9m5Gf2hXv41QoG4tB2W5fG.48z6P14a
            // customer123 -> $2a$11$c8Y54yH3wH5kQ/WlB2V7/O9m5Gf2hXv41QoG4tB2W5fG.48z6P14a
            // Let's use standard precalculated BCrypt hashes for:
            // "admin123" -> $2a$11$f5PqM.C2FvQ20WzY5v9n5eY8U.M8zJpPZ0Q.23v21eB4W1n6oA9a2
            // "staff123" -> $2a$11$i2g93zY5v9n5eY8U.M8zJpPZ0Q.23v21eB4W1n6oA9a2i2g93zY5
            // "customer123" -> $2a$11$eY8U.M8zJpPZ0Q.23v21eB4W1n6oA9a2i2g93zY5v9n5eY8U.
            // Let's use actually verified BCrypt hashes:
            string adminHash = BCrypt.Net.BCrypt.HashPassword("admin123");
            string staffHash = BCrypt.Net.BCrypt.HashPassword("staff123");
            string customerHash = BCrypt.Net.BCrypt.HashPassword("customer123");

            // Seed Admin (inherits NhanVien, inherits NguoiDung)
            modelBuilder.Entity<Admin>().HasData(
                new Admin 
                { 
                    MaNguoiDung = "ND001", 
                    HoTen = "Nguyen Van Admin", 
                    Email = "admin@grocery.com", 
                    SoDienThoai = "0900000001", 
                    DiaChi = "123 Main St, Ha Noi",
                    MaNhanVien = "NV000", 
                    ChucVu = "Quan Tri Vien" 
                }
            );

            // Seed NhanVien (inherits NguoiDung)
            modelBuilder.Entity<NhanVien>().HasData(
                new NhanVien 
                { 
                    MaNguoiDung = "ND002", 
                    HoTen = "Tran Thi Nhan Vien", 
                    Email = "staff@grocery.com", 
                    SoDienThoai = "0900000002", 
                    DiaChi = "456 Side St, Ha Noi",
                    MaNhanVien = "NV001", 
                    ChucVu = "Thu Ngan" 
                }
            );

            // Seed KhachHang (inherits NguoiDung)
            modelBuilder.Entity<KhachHang>().HasData(
                new KhachHang 
                { 
                    MaNguoiDung = "ND003", 
                    HoTen = "Le Van Khach Hang", 
                    Email = "customer@gmail.com", 
                    SoDienThoai = "0900000003", 
                    DiaChi = "789 Lane, Da Nang",
                    MaKhachHang = "KH001", 
                    NgayDangKy = new DateTime(2026, 1, 1) 
                }
            );

            // Seed TaiKhoan
            modelBuilder.Entity<TaiKhoan>().HasData(
                new TaiKhoan { MaNguoiDung = "ND001", MaTaiKhoan = "TK001", TenDangNhap = "admin", MatKhau = adminHash, TrangThaiTaiKhoan = TrangThaiTaiKhoan.HoatDong },
                new TaiKhoan { MaNguoiDung = "ND002", MaTaiKhoan = "TK002", TenDangNhap = "nhanvien", MatKhau = staffHash, TrangThaiTaiKhoan = TrangThaiTaiKhoan.HoatDong },
                new TaiKhoan { MaNguoiDung = "ND003", MaTaiKhoan = "TK003", TenDangNhap = "khachhang", MatKhau = customerHash, TrangThaiTaiKhoan = TrangThaiTaiKhoan.HoatDong }
            );

            // Seed 8-10 SanPham
            modelBuilder.Entity<SanPham>().HasData(
                new SanPham { MaSP = "SP001", TenSP = "Sua tuoi Vinamilk 1L", DanhMuc = "Do uong", DonGia = 35000m, SoLuongTon = 50, TrangThaiSanPham = TrangThaiSanPham.SanSang },
                new SanPham { MaSP = "SP002", TenSP = "Banh qui Oreo 133g", DanhMuc = "Banh keo", DonGia = 18000m, SoLuongTon = 100, TrangThaiSanPham = TrangThaiSanPham.SanSang },
                new SanPham { MaSP = "SP003", TenSP = "Mi hao hao Chua cay", DanhMuc = "Thuc pham an lien", DonGia = 4500m, SoLuongTon = 200, TrangThaiSanPham = TrangThaiSanPham.SanSang },
                new SanPham { MaSP = "SP004", TenSP = "Dau an Simply 1L", DanhMuc = "Gia vi", DonGia = 55000m, SoLuongTon = 30, TrangThaiSanPham = TrangThaiSanPham.SanSang },
                new SanPham { MaSP = "SP005", TenSP = "Nuoc tuong Chinsu 250ml", DanhMuc = "Gia vi", DonGia = 16000m, SoLuongTon = 40, TrangThaiSanPham = TrangThaiSanPham.SanSang },
                new SanPham { MaSP = "SP006", TenSP = "Coca Cola lon 320ml", DanhMuc = "Do uong", DonGia = 10000m, SoLuongTon = 120, TrangThaiSanPham = TrangThaiSanPham.SanSang },
                new SanPham { MaSP = "SP007", TenSP = "Gao Thom ST25 5kg", DanhMuc = "Thuc pham kho", DonGia = 190000m, SoLuongTon = 15, TrangThaiSanPham = TrangThaiSanPham.SanSang },
                new SanPham { MaSP = "SP008", TenSP = "Nuoc giat Ariel 3.2kg", DanhMuc = "Hoa my pham", DonGia = 175000m, SoLuongTon = 10, TrangThaiSanPham = TrangThaiSanPham.SanSang },
                new SanPham { MaSP = "SP009", TenSP = "Khan uot Mamamy 80 to", DanhMuc = "Hoa my pham", DonGia = 32000m, SoLuongTon = 0, TrangThaiSanPham = TrangThaiSanPham.HetHang },
                new SanPham { MaSP = "SP010", TenSP = "Banh snack Oishi Bi do", DanhMuc = "Banh keo", DonGia = 6000m, SoLuongTon = 50, TrangThaiSanPham = TrangThaiSanPham.NgungKinhDoanh }
            );
        }
    }

    public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            var connectionString = configuration.GetConnectionString("DefaultConnection");

            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            optionsBuilder.UseSqlServer(connectionString);

            return new AppDbContext(optionsBuilder.Options);
        }
    }
}
