using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace QuanLyCuaHangTapHoa.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "NguoiDungs",
                columns: table => new
                {
                    MaNguoiDung = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    HoTen = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    SoDienThoai = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    DiaChi = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NguoiDungs", x => x.MaNguoiDung);
                });

            migrationBuilder.CreateTable(
                name: "SanPhams",
                columns: table => new
                {
                    MaSP = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TenSP = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DanhMuc = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    DonGia = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SoLuongTon = table.Column<int>(type: "int", nullable: false),
                    TrangThaiSanPham = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SanPhams", x => x.MaSP);
                });

            migrationBuilder.CreateTable(
                name: "KhachHangs",
                columns: table => new
                {
                    MaNguoiDung = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    MaKhachHang = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    NgayDangKy = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KhachHangs", x => x.MaNguoiDung);
                    table.ForeignKey(
                        name: "FK_KhachHangs_NguoiDungs_MaNguoiDung",
                        column: x => x.MaNguoiDung,
                        principalTable: "NguoiDungs",
                        principalColumn: "MaNguoiDung",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "NhanViens",
                columns: table => new
                {
                    MaNguoiDung = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    MaNhanVien = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ChucVu = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NhanViens", x => x.MaNguoiDung);
                    table.ForeignKey(
                        name: "FK_NhanViens_NguoiDungs_MaNguoiDung",
                        column: x => x.MaNguoiDung,
                        principalTable: "NguoiDungs",
                        principalColumn: "MaNguoiDung",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TaiKhoans",
                columns: table => new
                {
                    MaNguoiDung = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    MaTaiKhoan = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    TenDangNhap = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    MatKhau = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    TrangThaiTaiKhoan = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaiKhoans", x => x.MaNguoiDung);
                    table.ForeignKey(
                        name: "FK_TaiKhoans_NguoiDungs_MaNguoiDung",
                        column: x => x.MaNguoiDung,
                        principalTable: "NguoiDungs",
                        principalColumn: "MaNguoiDung",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DonDatHangs",
                columns: table => new
                {
                    MaDon = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    NgayDat = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NgayDuyet = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TrangThaiDonHang = table.Column<int>(type: "int", nullable: false),
                    MaNguoiDungKhachHang = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DonDatHangs", x => x.MaDon);
                    table.ForeignKey(
                        name: "FK_DonDatHangs_KhachHangs_MaNguoiDungKhachHang",
                        column: x => x.MaNguoiDungKhachHang,
                        principalTable: "KhachHangs",
                        principalColumn: "MaNguoiDung",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Admins",
                columns: table => new
                {
                    MaNguoiDung = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Admins", x => x.MaNguoiDung);
                    table.ForeignKey(
                        name: "FK_Admins_NhanViens_MaNguoiDung",
                        column: x => x.MaNguoiDung,
                        principalTable: "NhanViens",
                        principalColumn: "MaNguoiDung",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ChiTietDonHangs",
                columns: table => new
                {
                    MaDon = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    MaSP = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    SoLuong = table.Column<int>(type: "int", nullable: false),
                    DonGia = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChiTietDonHangs", x => new { x.MaDon, x.MaSP });
                    table.ForeignKey(
                        name: "FK_ChiTietDonHangs_DonDatHangs_MaDon",
                        column: x => x.MaDon,
                        principalTable: "DonDatHangs",
                        principalColumn: "MaDon",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ChiTietDonHangs_SanPhams_MaSP",
                        column: x => x.MaSP,
                        principalTable: "SanPhams",
                        principalColumn: "MaSP",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "HoaDons",
                columns: table => new
                {
                    MaHoaDon = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    NgayLap = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TongTien = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MaDon = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    MaNguoiDungNhanVien = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HoaDons", x => x.MaHoaDon);
                    table.ForeignKey(
                        name: "FK_HoaDons_DonDatHangs_MaDon",
                        column: x => x.MaDon,
                        principalTable: "DonDatHangs",
                        principalColumn: "MaDon",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_HoaDons_NhanViens_MaNguoiDungNhanVien",
                        column: x => x.MaNguoiDungNhanVien,
                        principalTable: "NhanViens",
                        principalColumn: "MaNguoiDung",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "NguoiDungs",
                columns: new[] { "MaNguoiDung", "DiaChi", "Email", "HoTen", "SoDienThoai" },
                values: new object[,]
                {
                    { "ND001", "123 Main St, Ha Noi", "admin@grocery.com", "Nguyen Van Admin", "0900000001" },
                    { "ND002", "456 Side St, Ha Noi", "staff@grocery.com", "Tran Thi Nhan Vien", "0900000002" },
                    { "ND003", "789 Lane, Da Nang", "customer@gmail.com", "Le Van Khach Hang", "0900000003" }
                });

            migrationBuilder.InsertData(
                table: "SanPhams",
                columns: new[] { "MaSP", "DanhMuc", "DonGia", "SoLuongTon", "TenSP", "TrangThaiSanPham" },
                values: new object[,]
                {
                    { "SP001", "Do uong", 35000m, 50, "Sua tuoi Vinamilk 1L", 0 },
                    { "SP002", "Banh keo", 18000m, 100, "Banh qui Oreo 133g", 0 },
                    { "SP003", "Thuc pham an lien", 4500m, 200, "Mi hao hao Chua cay", 0 },
                    { "SP004", "Gia vi", 55000m, 30, "Dau an Simply 1L", 0 },
                    { "SP005", "Gia vi", 16000m, 40, "Nuoc tuong Chinsu 250ml", 0 },
                    { "SP006", "Do uong", 10000m, 120, "Coca Cola lon 320ml", 0 },
                    { "SP007", "Thuc pham kho", 190000m, 15, "Gao Thom ST25 5kg", 0 },
                    { "SP008", "Hoa my pham", 175000m, 10, "Nuoc giat Ariel 3.2kg", 0 },
                    { "SP009", "Hoa my pham", 32000m, 0, "Khan uot Mamamy 80 to", 3 },
                    { "SP010", "Banh keo", 6000m, 50, "Banh snack Oishi Bi do", 5 }
                });

            migrationBuilder.InsertData(
                table: "KhachHangs",
                columns: new[] { "MaNguoiDung", "MaKhachHang", "NgayDangKy" },
                values: new object[] { "ND003", "KH001", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "NhanViens",
                columns: new[] { "MaNguoiDung", "ChucVu", "MaNhanVien" },
                values: new object[,]
                {
                    { "ND001", "Quan Tri Vien", "NV000" },
                    { "ND002", "Thu Ngan", "NV001" }
                });

            migrationBuilder.InsertData(
                table: "TaiKhoans",
                columns: new[] { "MaNguoiDung", "MaTaiKhoan", "MatKhau", "TenDangNhap", "TrangThaiTaiKhoan" },
                values: new object[,]
                {
                    { "ND001", "TK001", "$2a$11$QWA6VYYwVuMeQt5URQjppuSYATFgmwKcp1xQ/LYfOXgFkqnB7dEVC", "admin", 0 },
                    { "ND002", "TK002", "$2a$11$6i8EYx0V/yxT6gkqIfdaFON4Q9GtLpGWQzTKbtjeR984fkStm0hiu", "nhanvien", 0 },
                    { "ND003", "TK003", "$2a$11$LcU9So22QpvpfW9jhdI0teIjIxFvsbuyO0D7lHGKFm9G2dezKk0Nm", "khachhang", 0 }
                });

            migrationBuilder.InsertData(
                table: "Admins",
                column: "MaNguoiDung",
                value: "ND001");

            migrationBuilder.CreateIndex(
                name: "IX_ChiTietDonHangs_MaSP",
                table: "ChiTietDonHangs",
                column: "MaSP");

            migrationBuilder.CreateIndex(
                name: "IX_DonDatHangs_MaNguoiDungKhachHang",
                table: "DonDatHangs",
                column: "MaNguoiDungKhachHang");

            migrationBuilder.CreateIndex(
                name: "IX_HoaDons_MaDon",
                table: "HoaDons",
                column: "MaDon");

            migrationBuilder.CreateIndex(
                name: "IX_HoaDons_MaNguoiDungNhanVien",
                table: "HoaDons",
                column: "MaNguoiDungNhanVien");

            migrationBuilder.CreateIndex(
                name: "IX_KhachHangs_MaKhachHang",
                table: "KhachHangs",
                column: "MaKhachHang",
                unique: true,
                filter: "[MaKhachHang] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_NguoiDungs_Email",
                table: "NguoiDungs",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_NguoiDungs_SoDienThoai",
                table: "NguoiDungs",
                column: "SoDienThoai",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_NhanViens_MaNhanVien",
                table: "NhanViens",
                column: "MaNhanVien",
                unique: true,
                filter: "[MaNhanVien] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_TaiKhoans_MaTaiKhoan",
                table: "TaiKhoans",
                column: "MaTaiKhoan",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TaiKhoans_TenDangNhap",
                table: "TaiKhoans",
                column: "TenDangNhap",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Admins");

            migrationBuilder.DropTable(
                name: "ChiTietDonHangs");

            migrationBuilder.DropTable(
                name: "HoaDons");

            migrationBuilder.DropTable(
                name: "TaiKhoans");

            migrationBuilder.DropTable(
                name: "SanPhams");

            migrationBuilder.DropTable(
                name: "DonDatHangs");

            migrationBuilder.DropTable(
                name: "NhanViens");

            migrationBuilder.DropTable(
                name: "KhachHangs");

            migrationBuilder.DropTable(
                name: "NguoiDungs");
        }
    }
}
