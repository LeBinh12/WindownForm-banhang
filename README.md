# 🏪 Hệ Thống Quản Lý Cửa Hàng Tạp Hóa

> **Windows Forms · .NET 8 · Entity Framework Core (TPT) · SQL Server · Dependency Injection**

---

## 🔑 Tài Khoản Đăng Nhập

| Vai trò | Username | Mật khẩu | Quyền hạn |
|:---|:---|:---|:---|
| **Quản Trị Viên** | `admin` | `admin123` | Toàn quyền: sản phẩm, kho, đơn hàng, POS, tài khoản |
| **Nhân Viên** | `nhanvien` | `staff123` | Sản phẩm, duyệt đơn, POS, đổi trả. Không quản lý tài khoản |
| **Khách Hàng** | `khachhang` | `customer123` | Xem sản phẩm, tạo yêu cầu đặt giữ hàng |

> 🔒 Mật khẩu được mã hóa một chiều bằng **BCrypt** — không thể giải mã ngược.

---

## 🚀 Hướng Dẫn Cài Đặt & Chạy

### Yêu cầu

- **.NET 8.0 SDK**
- **SQL Server** (LocalDB hoặc SQLEXPRESS)
- *(Tuỳ chọn)* `dotnet-ef` CLI để quản lý migration thủ công:
  ```powershell
  dotnet tool install --global dotnet-ef
  ```

### Cấu hình kết nối DB

Mở `appsettings.json` và chỉnh Connection String:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=TEN_MAY\\SQLEXPRESS;Database=QuanLyCuaHangTapHoa;Trusted_Connection=True;TrustServerCertificate=True"
  }
}
```

### Chạy ứng dụng

```powershell
# App tự động chạy migration và tạo DB + seed data khi khởi động
dotnet run
```

---

## 📋 Luồng Nghiệp Vụ Chi Tiết

### Luồng A — Đặt Giữ Hàng (Reservation)

> Khách hàng đặt trước, nhân viên/admin duyệt, sau đó thanh toán tại quầy.

```
┌─────────────────────────────────────────────────────────────────────┐
│  BƯỚC 1 — Khách hàng tạo yêu cầu                                   │
│  Login: khachhang                                                    │
│  Tab "Đơn đặt giữ hàng" → nút [+ Tạo yêu cầu đặt giữ]             │
│  → Chọn sản phẩm + số lượng → [Gửi yêu cầu]                        │
│                                                                       │
│  Kết quả: Đơn tạo ra với trạng thái  🟡 Chờ duyệt (ChoDuyet)       │
│           Sản phẩm chuyển sang       ⏳ Chờ xuất kho (ChoXuatKho)   │
└───────────────────────────┬─────────────────────────────────────────┘
                            │
                            ▼
┌─────────────────────────────────────────────────────────────────────┐
│  BƯỚC 2 — Admin / Nhân viên duyệt đơn                               │
│  Login: admin hoặc nhanvien                                          │
│  Tab "Đơn đặt giữ hàng" → thấy đơn mới "🟡 Chờ duyệt"             │
│  → Bấm [Duyệt] → xác nhận                                           │
│                                                                       │
│  Kết quả: Đơn chuyển sang trạng thái 🟢 Đã duyệt (DaDuyet)         │
│  ⏱ Đơn được giữ tối đa 3 ngày kể từ lúc duyệt (BR-05)             │
└───────────────────────────┬─────────────────────────────────────────┘
                            │
                            ▼
┌─────────────────────────────────────────────────────────────────────┐
│  BƯỚC 3 — Thanh toán tại quầy POS                                   │
│  Login: admin hoặc nhanvien                                          │
│  Tab "Thanh toán POS" → phần "Đơn đặt giữ đã duyệt"                │
│  → Chọn đơn từ dropdown → xem chi tiết → [Lập hóa đơn]             │
│                                                                       │
│  Kết quả: Đơn chuyển sang  🔵 Đã thanh toán (DaThanhToan)           │
│           HoaDon được tạo và lưu vào DB                              │
│           Sản phẩm chuyển sang SanSang (nếu còn tồn) / HetHang      │
└───────────────────────────┬─────────────────────────────────────────┘
                            │
                            ▼
┌─────────────────────────────────────────────────────────────────────┐
│  BƯỚC 4 — Xem lịch sử hóa đơn (tuỳ chọn)                           │
│  Tab "Thanh toán POS" → phần "Lịch sử hóa đơn"                     │
│  → Chọn hóa đơn → [Chi tiết] để xem sản phẩm + tổng tiền           │
│  → [Đổi trả] nếu khách trả hàng trong vòng 7 ngày (BR-06)          │
└─────────────────────────────────────────────────────────────────────┘
```

---

### Luồng B — Bán Lẻ Trực Tiếp (POS Walk-in)

> Không qua bước đặt giữ, không cần duyệt — thanh toán ngay tại quầy.

```
Login: admin hoặc nhanvien
Tab "Thanh toán POS" → phần bên trái "Bán lẻ trực tiếp"
→ Chọn sản phẩm + số lượng → [Thêm vào giỏ]
→ [Thanh toán bán lẻ]

Kết quả: HoaDon tạo ngay
          DonDatHang backend tạo thẳng với DaThanhToan
          ⚠ Đơn này KHÔNG xuất hiện trong màn "Đơn đặt giữ hàng"
```

---

### Luồng C — Hủy / Từ Chối Đơn

```
Admin / Nhân viên — Tab "Đơn đặt giữ hàng"
→ Bấm [Hủy đơn] trên đơn ở trạng thái ChoDuyet hoặc DaDuyet

Kết quả: Đơn chuyển sang  🔴 Từ chối (TuChoi)
          Số lượng tồn kho được hoàn trả về sản phẩm
          Sản phẩm chuyển về SanSang để bán lại
```

---

### Luồng D — Thu Hồi Đơn Quá Hạn

```
Admin / Nhân viên — Tab "Đơn đặt giữ hàng"
→ Nút [Thu hồi đơn hết hạn]

Điều kiện: Đơn ở trạng thái DaDuyet và đã quá 3 ngày kể từ ngày duyệt
Kết quả:   Đơn chuyển sang ⚫ Đã hủy (DaHuy)
            Tồn kho được hoàn trả, sản phẩm về SanSang
```

---

## 📊 Vòng Đời Trạng Thái Đơn Hàng

```
                   [Khách tạo]
                       │
                  🟡 ChoDuyet (0)
                  ┌────┴────┐
             [Duyệt]    [Hủy/Từ chối]
                │              │
          🟢 DaDuyet (1)   🔴 TuChoi (2)
          ┌────┴────┐
    [Thanh toán]  [Hủy]
          │           │
  🔵 DaThanhToan (3) ⚫ DaHuy (4)

  [POS bán lẻ] → tạo thẳng DaThanhToan (3), bỏ qua mọi bước trên
```

---

## 🏛️ Kiến Trúc 4 Tầng

```
Presentation/          ← WinForms UI (UserControls, Forms, ThemeHelper)
Application/           ← Use Cases: OrderUseCase, InvoiceUseCase, ...
Domain/                ← Entities, Enums, Repository Interfaces
Infrastructure/        ← AppDbContext, Repositories (EF Core)
```

### Phân quyền theo TPT Inheritance

```
NguoiDung (base)
├── KhachHang   → table: KhachHangs   → chỉ tạo đơn đặt giữ
├── NhanVien    → table: NhanViens    → duyệt đơn, POS, sản phẩm
└── Admin       → table: Admins       ← kế thừa NhanVien + quản lý tài khoản
```

---

## 📐 Quy Tắc Nghiệp Vụ (Business Rules)

| Mã | Mô tả |
|:---|:---|
| **BR-01** | Khách hàng chỉ đặt được sản phẩm `SanSang` và `SoLuongTon > 0` |
| **BR-05** | Đơn đã duyệt tự động hủy nếu chưa thanh toán sau **3 ngày** |
| **BR-06** | Đổi trả hàng lỗi trong tối đa **7 ngày** từ ngày lập hóa đơn |
| **SR-03** | Mật khẩu lưu bằng **BCrypt** — không thể giải mã ngược |
| **SR-04** | Không xóa vật lý: tài khoản → `BiKhoa`, sản phẩm → `NgungKinhDoanh` |
| **SR-05** | Admin đang đăng nhập **không thể tự khóa** tài khoản của mình |

---

## 🛠️ Stack Công Nghệ

| Thành phần | Công nghệ |
|:---|:---|
| Framework | .NET 8 Windows Forms |
| ORM | Entity Framework Core 8 (Code First, TPT Inheritance) |
| Database | SQL Server / LocalDB |
| UI Library | Guna UI 2 (controls bo góc, hiệu ứng) |
| DI Container | `Microsoft.Extensions.DependencyInjection` |
| Password Hash | BCrypt.Net-Next |
| Pattern | Repository + Use Case + 4-Tier Architecture |
