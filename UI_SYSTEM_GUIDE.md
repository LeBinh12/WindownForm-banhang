# TÀI LIỆU HƯỚNG DẪN HỆ THỐNG UI/UX TOÀN DIỆN (SYSTEM DESIGN SYSTEM & STYLE GUIDE)
## Hệ Thống Quản Lý & Điều Phối Tình Nguyện Số (Gov Volunteer Hub)

> [!NOTE]  
> Tài liệu này tổng hợp toàn bộ triết lý thiết kế, mã màu thực tế (Tailwind CSS v4 / Material UI), cấu trúc Layout, các Component tái sử dụng và luồng tương tác cốt lõi của cả hệ thống **Web (Quản trị)** và **Mobile (Đoàn viên)**. Đây là cẩm nang thiết kế chuẩn hóa giúp lập trình viên có thể dễ dàng duy trì hệ thống hiện tại hoặc nhân bản nhanh chóng sang các hệ thống quản trị hành chính công và đoàn thể khác.

---

## 1. Triết Lý Thiết Kế & Hướng Tiếp Cận

Hệ thống được phát triển với sự kết hợp giữa tính **chuyên nghiệp, nghiêm túc** của khối dịch vụ công (Government-grade) và tính **trẻ trung, năng động** của lực lượng thanh niên tình nguyện.

*   **Aesthetic (Mỹ thuật):** Giao diện phẳng hiện đại, kết hợp hiệu ứng kính (Glassmorphism), viền siêu mảnh (soft borders), đổ bóng nhẹ (soft elevation) và gradient mesh làm nền để tăng chiều sâu cho giao diện chat và dashboard.
*   **Chức năng (Functional UX):**
    *   **Web Dashboard:** Tối ưu hóa cho máy tính để bàn (Desktop), giao diện dạng khối (cards) rõ ràng, bố cục trực quan giúp Admin dễ thao tác lọc, quản lý dữ liệu lớn (DataTable) và theo dõi biểu đồ thống kê trực quan.
    *   **Mobile App (Mobile-First):** Thiết kế tối giản, tập trung vào thao tác nhanh bằng một tay (chỉ tối đa 3 chạm để hoàn thành Check-in). Chú trọng trải nghiệm ngoại tuyến (Offline-friendly), tải trang nhanh với hiệu ứng khung xương (Skeleton loading) và phản hồi rung vật lý (Haptic feedback) khi check-in thành công.

---

## 2. Hệ Thống Design Tokens (CSS / Tailwind v4 / MUI)

Dưới đây là các biến CSS thực tế đang được sử dụng trong mã nguồn của hệ thống (file `src/frontend/src/index.css` sử dụng Tailwind CSS v4 `@theme` directive).

### 2.1 Bảng Màu Hệ Thống (Color Palette)

| Loại Màu | Tên Biến CSS | Mã Màu Hex | Ứng Dụng Thực Tế / Trạng Thế |
| :--- | :--- | :--- | :--- |
| **Primary (Brand)** | `--color-primary` | `#1A3C8F` | Xanh dương đậm đặc trưng Đoàn TN |
| **Primary Hover** | `--color-primary-hover` | `#152F73` | Trạng thái hover của nút bấm chính |
| **Primary Light** | `--color-primary-light` | `#E8EDF5` | Nền menu active, thẻ tin nhắn, activity-tag |
| **Primary Lighter** | `--color-primary-lighter` | `#F1F4F9` | Nền phụ, hover nhẹ |
| **On Primary** | `--color-on-primary` | `#FFFFFF` | Chữ hiển thị trên nền màu primary |
| **Secondary** | `--color-success` | `#059669` | Xanh lá tình nguyện / Trạng thái hoạt động **Mở** |
| **Success Light** | `--color-success-light`| `#ECFDF5` | Nền badge hoặc thông báo thành công |
| **Warning** | `--color-warning` | `#D97706` | Trạng thái hoạt động **Chưa bắt đầu** / Cảnh báo |
| **Warning Light** | `--color-warning-light`| `#FFFBEB` | Nền badge hoặc thông báo cảnh báo |
| **Danger / Red** | `--color-danger` | `#DC2626` | Trạng thái hoạt động **Khóa** / Lỗi / Đóng |
| **Danger Light** | `--color-danger-light` | `#FEF2F2` | Nền badge hoặc thông báo lỗi |
| **Gold (Gold Light)**| `--color-gold` | `#D97706` | Sử dụng cho hệ thống danh hiệu, hạng **Vàng** |
| **Background App** | `--color-bg` | `#F9FAFB` | Màu nền chung toàn bộ website và ứng dụng |
| **Background Card**| `--color-bg-card` | `#FFFFFF` | Nền của các thẻ (Card), bảng biểu, hộp hội thoại |
| **Text Primary** | `--color-text` | `#1F2937` | Màu chữ chính, độ tương phản cao |
| **Text Secondary**| `--color-text-secondary`| `#6B7280` | Màu chữ phụ, mô tả ngắn, caption, thời gian |
| **Text Muted** | `--color-text-muted` | `#9CA3AF` | Chữ gợi ý (Placeholder), biểu tượng vô hiệu hóa |
| **Border Dark** | `--color-border` | `#E5E7EB` | Đường viền ngăn cách chính |
| **Border Light** | `--color-border-light` | `#F3F4F6` | Đường viền mảnh phụ, ngăn cách dòng |

### 2.2 Typography & Fonts

*   **Font chữ chính:** `Inter`, `system-ui`, `-apple-system`, `sans-serif`. 
*   **Hỗ trợ Emoji Quốc kỳ:** Tích hợp font `"Twemoji Country Flags"` hỗ trợ hiển thị đồng bộ cờ quốc gia trên mọi hệ điều hành (tránh lỗi hiển thị ô vuông hoặc chữ cái viết tắt trên Windows).
*   **Quy định kích cỡ chữ (Font-size):**
    *   **Heading 1:** `24px` (Inter Bold) - Dành cho tiêu đề trang lớn.
    *   **Heading 2:** `20px` (Inter SemiBold) - Dành cho tiêu đề section hoặc panel.
    *   **Subheading:** `16px` (Inter Medium) - Tiêu đề card, nhóm tin nhắn.
    *   **Body (Web):** `13px` - `14px` (Inter Regular) - Nội dung hiển thị dữ liệu bảng, danh sách.
    *   **Body (Mobile):** `14sp` (Inter Regular) - Cỡ chữ chuẩn cho ứng dụng di động.
    *   **Caption / Tab:** `11px` - `12px` - Dành cho thời gian, ghi chú nhỏ, tab navigation.
    *   **iOS Safari Auto-zoom Fix:** Ép kích thước font-size của tất cả `input, textarea, select` lên **tối thiểu `16px` trên các thiết bị di động** (`max-width: 640px`) để loại bỏ hoàn toàn tính năng tự động phóng to (zoom-in) gây khó chịu của iOS Safari khi người dùng chạm vào các ô nhập liệu.

### 2.3 Độ Cong (Radius), Đổ Bóng (Shadows) & Spacing

*   **Radius (Độ bo góc):**
    *   `--radius-sm: 4px` - Dùng cho nút nhỏ, các checkbox, input mini.
    *   `--radius-md: 6px` - Dùng cho input thông thường, nút tiêu chuẩn.
    *   `--radius-lg: 8px` - Dùng cho các khung bubble chat, menu con.
    *   `--radius-xl: 12px` - Dùng cho các thẻ Card dữ liệu chính, bảng biểu.
    *   `--radius-full: 9999px` - Dùng cho Avatar, các nút dạng Pill (Pill button).
*   **Shadows (Đổ bóng):**
    *   `--shadow-sm: 0 1px 2px rgba(0,0,0,0.05)` - Đổ bóng viền cực nhẹ cho card phụ.
    *   `--shadow-md: 0 2px 4px rgba(0,0,0,0.06)` - Đổ bóng tiêu chuẩn cho card chính.
    *   `--shadow-lg: 0 4px 8px rgba(0,0,0,0.08)` - Đổ bóng nổi bật cho Popup, Dialog, Header nổi.

---

## 3. Kiến Trúc Bố Cục (Layout Architecture)

### 3.1 Bố Cục Web Admin (Desktop & Tablet)

Web Admin sử dụng mô hình **Sidebar Menu bên trái cố định** kết hợp với **Header Top Bar** chứa các thanh công cụ điều khiển nhanh.

```
+---------------------------------------------------------------------------------+
| [🛡️ Logo] HỆ THỐNG ĐIỀU PHỐI SỐ    | [🔍 Tìm kiếm...]   [🔔(3)] [Avatar Nguyễn ▼] |
+-----------------+---------------------------------------------------------------+
| 📊 Dashboard    | Trang chủ > Quản lý hoạt động > Chi tiết                      |
| 📋 Hoạt động    | +-----------------------------------------------------------+ |
| 🗺️ Bản đồ số    | | CHI TIẾT HOẠT ĐỘNG: CHIẾN DỊCH MÙA HÈ XANH  [🟢 ĐANG MỞ]   | |
| 📰 Quản lý tin  | |-----------------------------------------------------------| |
| 📊 Báo cáo      | | - Đơn vị tổ chức: Đoàn xã Phú Hòa                         | |
| 👤 Tài khoản    | | - Địa điểm: Nhà Văn Hóa xã Phú Hòa                        | |
| ⚙️ Hệ thống     | | - Tọa độ: 📍 10.xxxx, 106.xxxx                            | |
|                 | +-----------------------------------------------------------+ |
|                 |                                                               |
| [◀ Thu gọn menu] | [© 2026 Gov Volunteer Hub]                          [Trợ giúp] |
+-----------------+---------------------------------------------------------------+
```

*   **Tính năng Phân quyền Menu:**
    *   **Admin:** Nhìn thấy toàn bộ danh mục cài đặt hệ thống và quản lý đơn vị toàn tỉnh.
    *   **Tỉnh đoàn:** Nhìn thấy tất cả hoạt động, bản đồ số, tin tức, thống kê nhưng ẩn phần "Cài đặt hệ thống".
    *   **Đoàn cấp xã:** Chỉ hiển thị các hoạt động do chính đơn vị mình tổ chức, ẩn mục "Quản lý đơn vị".

### 3.2 Bố Cục Ứng Dụng Mobile

Mobile sử dụng thanh điều hướng dưới cùng (**Bottom Tab Bar**), được tùy biến cấu trúc tự động dựa vào vai trò của tài khoản đăng nhập.

*   **Dành cho Đoàn viên (User thông thường):**
    *   `Trang chủ (Home)`: Xem hoạt động nổi bật gần bạn, tin tức mới.
    *   `Hoạt động (Activities)`: Danh sách tất cả hoạt động mở đăng ký/đã khóa.
    *   `Check-in (Check-in)`: Phím tắt trung tâm để Quét mã QR hoặc chụp ảnh xác thực vị trí.
    *   `Tin tức (News Feed)`: Trang mạng xã hội thu nhỏ hiển thị hình ảnh tình nguyện và tin tức.
    *   `Cá nhân (Profile)`: Lịch sử tham gia, tích lũy điểm thưởng và các hạng chứng nhận (Vàng, Bạc, Đồng).

*   **Dành cho Quản trị viên Cấp Xã / Tỉnh:**
    *   `Trang chủ (Home)`: Thống kê nhanh số đoàn viên đang hoạt động trong ngày.
    *   `Quản lý HĐ (Manage)`: Danh sách hoạt động do mình quản lý, khóa/mở nhanh.
    *   `Tạo HĐ (Create HĐ)`: Phím tắt tạo nhanh hoạt động tình nguyện bằng form di động.
    *   `Thống kê (Analytics)`: Biểu đồ cột và thống kê số lượng check-in thực tế.
    *   `Cá nhân (Profile)`: Cài đặt tài khoản quản trị và đăng xuất.

---

## 4. Đặc Tả Các Component Tái Sử Dụng Cốt Lõi

Để có thể nhân bản UI này sang hệ thống khác, bạn cần xây dựng các Component dùng chung (Reusable Components) dưới đây theo đúng cấu trúc mã nguồn hiện tại:

### 4.1 Bảng Dữ Liệu (DataTable) & Thanh Lọc (FilterBar)

Sử dụng thư viện `@mui/x-data-grid` kết hợp với styling Tailwind CSS v4.

*   **FilterBar:** Đặt ngay trên bảng dữ liệu. Gồm các ô chọn đơn vị (Dropdown), ô trạng thái (Status badge dropdown), khoảng ngày (DateRangePicker) và ô tìm kiếm tự do (Search input). Luôn có nút "Tìm kiếm" và nút "Xóa bộ lọc" (Reset filter).
*   **DataTable:**
    *   Hỗ trợ phân trang ở góc dưới bên phải (`Pagination: 10 / 25 / 50 kết quả`).
    *   Cột đầu tiên có tùy chọn Chọn tất cả/Chọn nhiều dòng (Checkbox) để thực hiện thao tác hàng loạt (Ví dụ: Khóa hàng loạt, duyệt hàng loạt).
    *   Cột cuối cùng luôn là **Hành động (Actions)** chứa các biểu tượng trực quan: ✏️ (Sửa), 🗑️ (Xóa), 👁️ (Xem chi tiết), 🔒/🔓 (Khóa/Mở khóa).

### 4.2 Huy Hiệu Trạng Thái (Status Badges)

Được chuẩn hóa với màu nền nhạt đi kèm chữ có độ tương phản cao nhằm tăng khả năng đọc lướt dữ liệu:

*   **Trạng thái "Mở" (Active/Open):** Nền xanh lá nhạt (`--color-success-light`), chữ xanh lá đậm (`--color-success`). Đi kèm chấm tròn 🟢 nhấp nháy nhẹ.
*   **Trạng thái "Khóa" (Locked/Closed):** Nền đỏ nhạt (`--color-danger-light`), chữ đỏ đậm (`--color-danger`). Biểu tượng 🔴.
*   **Trạng thái "Chờ Duyệt" (Pending):** Nền cam/vàng nhạt (`--color-warning-light`), chữ vàng đậm (`--color-warning`). Biểu tượng 🟡.

### 4.3 Khung Chat Hiện Đại & Giao Diện Luồng Bình Luận (Chat & Comments UX)

Một trong những phần có độ hoàn thiện UI cao nhất hệ thống là Chat và luồng bình luận với 2 phong cách lấy cảm hứng từ các mạng xã hội phổ biến:

*   **Zalo-style Chat List Layout:**
    *   **Header Gradient:** Nền xanh chuyển sắc (`#1A3C8F` → `#4D8EF0`) tích hợp thanh tìm kiếm mờ (opacity 18%).
    *   **Phân tab hội thoại:** Tab "Ưu tiên" và "Khác" phân cách bởi viền mảnh dưới. Tab đang chọn có gạch chân màu primary.
    *   **Unread Badge Overlay:** Huy hiệu hiển thị số tin nhắn chưa đọc màu xám đậm (`#94A3B8`) nằm đè lên góc dưới bên phải của Avatar thành viên, bo viền trắng nổi bật.
*   **Facebook-style Comment Thread Indicators:**
    *   Hỗ trợ luồng bình luận nhiều cấp độ (Nested comments).
    *   Sử dụng đường nối dọc màu xám nhạt (`.comment-thread-container::before`) chạy từ avatar của bình luận cha xuống đến bình luận con cuối cùng, kết thúc bằng một góc bo cong (elbow) 90 độ nối thẳng vào avatar của bình luận phản hồi để tăng tính liên kết trực quan.
*   **TipTap Rich Text Editor:**
    *   Hỗ trợ định dạng in đậm, in nghiêng, gạch chân, trích dẫn (blockquote), danh sách số và danh sách chấm tròn.
    *   **Đặc tả Mention Badge (`@`):** Khi gõ kí tự `@` và chọn thành viên, hệ thống sẽ chèn một badge chữ màu xám đậm nền xám nhạt (`rgba(15,23,42,0.08)`) bo tròn góc 4px. Nếu tin nhắn là của chính người gửi (mine chat bubble), badge mention sẽ tự động chuyển sang chữ trắng nền trắng mờ (`rgba(255,255,255,0.25)`).
    *   **Đặc tả Activity Tag Badge (`#`):** Khi chèn liên kết đến một hoạt động tình nguyện bằng kí tự `#`, một badge màu xanh dương nhạt (`--color-primary-light`) sẽ xuất hiện đi kèm với icon sự kiện dạng SVG nhỏ phía trước.

---

## 5. Đặc Tả Chi Tiết Luồng Tương Tác Cốt Lõi (Core UX Flows)

### 5.1 Luồng Check-in Định Vị GPS & Camera Thực Tế (Mobile)

Đây là chức năng quan trọng nhất của hệ thống, đòi hỏi sự chính xác và thao tác nhanh chóng từ đoàn viên.

*   **Bước 1: Chọn hoạt động**
    *   Chọn thông qua quét camera mã QR tại địa điểm hoạt động hoặc chọn hoạt động đang diễn ra gần nhất được đề xuất tự động ở đầu danh sách bằng định vị GPS (`expo-location`).
*   **Bước 2: Chụp ảnh xác thực**
    *   Ứng dụng mở camera yêu cầu chụp 1-3 ảnh thực tế tại địa điểm hoạt động (`expo-camera` và `expo-image-picker`).
*   **Bước 3: So khớp khoảng cách địa lý**
    *   Hệ thống so sánh vị trí hiện tại của thiết bị (Kinh độ/Vĩ độ) với tọa độ đã đăng ký của hoạt động.
    *   *Nếu khoảng cách < 500 mét:* Cho phép check-in thành công ngay lập tức.
    *   *Nếu khoảng cách > 500 mét:* Đưa vào trạng thái **"Chờ duyệt"** kèm cảnh báo và hình ảnh để Ban tổ chức cấp xã phê duyệt thủ công.
*   **Bước 4: Thành công**
    *   Giao diện hiển thị hiệu ứng pháo hoa xoay tròn (Lottie Animation) và điện thoại rung nhẹ một nhịp (Haptic feedback) tạo cảm xúc tích cực cho người tham gia.

### 5.2 Luồng Khóa / Mở Khóa Hoạt Động (Web Admin)

Chức năng khóa hoạt động được thiết kế dưới dạng hộp hội thoại nhanh (Quick Dialog) thay vì chuyển trang nhằm tiết kiệm thời gian cho quản trị viên:

1.  **Bước 1:** Quản trị viên chọn hoạt động trên DataTable bằng cách bấm biểu tượng 🔒 (Khóa) hoặc chọn nhiều hoạt động và bấm nút "Khóa đã chọn".
2.  **Bước 2:** Một Modal Dialog hiện lên giữa màn hình với hiệu ứng làm mờ nền (backdrop blur).
3.  **Bước 3:** Quản trị viên nhập **Số lượng tham gia thực tế** (Hệ thống tự động gợi ý số lượng đoàn viên đã check-in qua app để admin đối chiếu) và **Thời gian khóa** (mặc định lấy thời gian hiện tại).
4.  **Bước 4:** Bấm "Xác nhận khóa" -> Hệ thống đóng dialog -> Hiển thị Toast thông báo góc phải trên màn hình -> Chuyển Badge trạng thái của hoạt động sang màu đỏ 🔴 Khóa.

---

## 6. Cẩm Nang Hướng Dẫn Nhân Bản Hệ Thống UI Cho Các Dự Án Khác

Nếu bạn muốn mang thiết kế và trải nghiệm UI này áp dụng cho một hệ thống khác (Ví dụ: *Hệ thống Quản lý Y tế Cơ sở*, *Cổng thông tin Giáo dục Học đường*, hoặc *Hệ thống Điều hành Sự cố Đô thị*), hãy thực hiện theo checklist dưới đây:

### 6.1 Thay Thế Brand Identity (Đổi Màu Màu Chủ Đạo)

Không cần phải viết lại code, chỉ cần chỉnh sửa các Token màu trong file cấu hình CSS. Ví dụ, đổi từ màu Xanh Đoàn Thanh niên sang các màu ngành đặc trưng:

*   **Nếu làm hệ thống Y tế (Medical Hub):** Thay màu xanh Đoàn bằng màu Xanh Ngọc bảo hộ.
    ```css
    --color-primary: #00897B;       /* Teal - Xanh ngọc */
    --color-primary-hover: #00695C;
    --color-primary-light: #E0F2F1;
    ```
*   **Nếu làm hệ thống Giáo dục (Edu Hub):** Thay bằng màu Cam Đất hoặc Đỏ Gạch ấm áp.
    ```css
    --color-primary: #E64A19;       /* Deep Orange - Cam gạch */
    --color-primary-hover: #D84315;
    --color-primary-light: #FBE9E7;
    ```
*   **Nếu làm hệ thống An ninh / Phòng cháy (Security Hub):** Thay bằng màu Đỏ Lửa.
    ```css
    --color-primary: #D32F2F;       /* Red - Đỏ */
    --color-primary-hover: #C62828;
    --color-primary-light: #FFEBEE;
    ```

### 6.2 Áp Dụng Triết Lý Layout Phân Quyền Hàng Dọc

Hầu hết các hệ thống dịch vụ công đều có cấu trúc 3 cấp tương tự như Đoàn Thanh niên (Tỉnh -> Huyện/Xã -> Đoàn viên). Khi cấu hình dữ liệu cho hệ thống mới, hãy giữ nguyên mô hình phân cấp:
1.  **Cấp 1 (Chỉ huy/Sở ban ngành):** Quyền xem tổng quan toàn tỉnh trên Dashboard biểu đồ và Bản đồ số.
2.  **Cấp 2 (Cơ sở/Phường xã/Bệnh viện/Trường học):** Quyền tổ chức, cập nhật thông tin và phê duyệt hồ sơ check-in/yêu cầu.
3.  **Cấp 3 (Người dân/Học sinh/Bệnh nhân):** Sử dụng Mobile App để quét QR, báo cáo sự cố hoặc thực hiện dịch vụ công với giao diện tối giản.

### 6.3 Quy Tắc Responsive & Đồng Nhất UX Trên Thiết Bị Di Động

Khi phát triển các màn hình mới cho hệ thống nhân bản, bạn phải tuân thức nghiêm ngặt các quy tắc kỹ thuật sau:
*   **Chặn cuộn nảy (Overscroll Bounce):** Đảm bảo gán `overscroll-behavior: none` cho cặp thẻ `html, body` trên phiên bản Web/Mobile web để ngăn hiện tượng trình duyệt tự động kích hoạt tính năng kéo để tải lại trang (pull-to-refresh) của iOS Safari hay Chrome khi người dùng cuộn kịch khung, giữ giao diện ứng dụng luôn ổn định giống như ứng dụng bản địa (Native App).
*   **Biểu diễn Trạng Thái Trực Quan:** Bất kỳ danh sách nào cũng phải có Badge trạng thái màu nhạt, tránh việc sử dụng màu chữ trơn không có nền hoặc màu sắc quá rực rỡ gây mỏi mắt khi làm việc lâu.
*   **Quy tắc Form Nhập Liệu:** Các trường thông tin bắt buộc phải có dấu sao màu đỏ (`*`). Trường chọn vị trí địa lý phải hiển thị bản đồ trực quan cho phép ghim trực tiếp (📍 Marker) thay vì chỉ cho nhập kinh độ/vĩ độ bằng tay.
*   **Thông báo phản hồi (Toast):** Mọi hành động lưu, xóa, sửa, khóa đều phải có Toast thông báo kết quả xuất hiện ở góc màn hình và tự biến mất sau 3 giây để tránh làm gián đoạn luồng làm việc của người dùng.

---

> [!TIP]  
> Hệ thống UI hiện tại đã được kiểm thử e2e tự động bằng Playwright trên web và Jest trên Mobile nhằm đảm bảo hiệu năng tối ưu và khả năng tương thích trình duyệt tốt nhất. Khi nhân bản, hãy cố gắng tận dụng lại cấu trúc thư mục của dự án `src/frontend` và `src/volunteer-app` để không phải thiết lập cấu hình build (Vite/Expo) từ đầu!
