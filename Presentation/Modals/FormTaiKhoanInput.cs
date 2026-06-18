using System;
using System.Drawing;
using System.Windows.Forms;
using Guna.UI2.WinForms;
using QuanLyCuaHangTapHoa.Domain;

namespace QuanLyCuaHangTapHoa.Presentation.Modals
{
    public partial class FormTaiKhoanInput : Form
    {
        public NguoiDung User { get; private set; }
        public TaiKhoan Account { get; private set; }
        public string Role { get; private set; }
        
        private readonly bool _isEdit;
        private readonly string _currentUserId;

        // Parameterless constructor for Visual Studio Designer
        public FormTaiKhoanInput()
        {
            Account = new TaiKhoan();
            User = new NhanVien(); // Default dummy user
            InitializeComponent();
            SetupData();
            SetupEvents();
        }

        public FormTaiKhoanInput(string currentUserId, TaiKhoan existingAccount = null)
        {
            _currentUserId = currentUserId;
            _isEdit = existingAccount != null;

            if (_isEdit)
            {
                Account = new TaiKhoan
                {
                    MaTaiKhoan = existingAccount.MaTaiKhoan,
                    TenDangNhap = existingAccount.TenDangNhap,
                    TrangThaiTaiKhoan = existingAccount.TrangThaiTaiKhoan,
                    MaNguoiDung = existingAccount.MaNguoiDung
                };

                var originalUser = existingAccount.NguoiDung;
                if (originalUser is Admin ad)
                {
                    Role = "Admin";
                    User = new Admin { MaNguoiDung = ad.MaNguoiDung, HoTen = ad.HoTen, Email = ad.Email, SoDienThoai = ad.SoDienThoai, DiaChi = ad.DiaChi, MaNhanVien = ad.MaNhanVien, ChucVu = ad.ChucVu };
                }
                else if (originalUser is NhanVien nv)
                {
                    Role = "NhanVien";
                    User = new NhanVien { MaNguoiDung = nv.MaNguoiDung, HoTen = nv.HoTen, Email = nv.Email, SoDienThoai = nv.SoDienThoai, DiaChi = nv.DiaChi, MaNhanVien = nv.MaNhanVien, ChucVu = nv.ChucVu };
                }
                else if (originalUser is KhachHang kh)
                {
                    Role = "KhachHang";
                    User = new KhachHang { MaNguoiDung = kh.MaNguoiDung, HoTen = kh.HoTen, Email = kh.Email, SoDienThoai = kh.SoDienThoai, DiaChi = kh.DiaChi, MaKhachHang = kh.MaKhachHang, NgayDangKy = kh.NgayDangKy };
                }
            }
            else
            {
                Account = new TaiKhoan();
                // Default roles
                Role = "NhanVien";
            }

            InitializeComponent();
            SetupData();
            SetupEvents();
        }

        private void SetupData()
        {
            lblTitle.Text = _isEdit ? "CẬP NHẬT TÀI KHOẢN" : "TẠO TÀI KHOẢN MỚI";
            lblPasswordLabel.Text = _isEdit ? "Mật khẩu mới (bỏ trống để giữ)" : "Mật khẩu *";
            txtMatKhau.PlaceholderText = _isEdit ? "Bỏ trống nếu không đổi..." : "Nhập mật khẩu...";

            txtMaNguoiDung.Text = User?.MaNguoiDung ?? "";
            txtMaNguoiDung.ReadOnly = _isEdit;

            txtTenDangNhap.Text = Account.TenDangNhap ?? "";
            txtTenDangNhap.ReadOnly = _isEdit;

            cbRole.Enabled = !_isEdit;
            cbRole.DataSource = EnumTranslator.TranslateVaiTro(false);
            cbRole.DisplayMember = "Text";
            cbRole.ValueMember = "Value";
            cbRole.SelectedValue = Role ?? "NhanVien";

            txtHoTen.Text = User?.HoTen ?? "";
            txtEmail.Text = User?.Email ?? "";
            txtSoDienThoai.Text = User?.SoDienThoai ?? "";
            txtDiaChi.Text = User?.DiaChi ?? "";

            string initialChucVu = "";
            if (User is Admin ad) initialChucVu = ad.ChucVu;
            else if (User is NhanVien nv) initialChucVu = nv.ChucVu;
            txtChucVu.Text = initialChucVu;

            string initialKH = "";
            if (User is KhachHang kh) initialKH = kh.MaKhachHang;
            txtMaKhachHang.Text = initialKH;

            ThemeHelper.StyleComboBox(cbTrangThai);
            cbTrangThai.DataSource = EnumTranslator.TranslateTrangThaiTaiKhoan(false);
            cbTrangThai.DisplayMember = "Text";
            cbTrangThai.ValueMember = "Value";
            cbTrangThai.SelectedValue = _isEdit ? Account.TrangThaiTaiKhoan : TrangThaiTaiKhoan.HoatDong;

            // Apply lock triggers initial
            CbRole_SelectedIndexChanged(null, null);
        }

        private void SetupEvents()
        {
            cbRole.SelectedIndexChanged += CbRole_SelectedIndexChanged;
            btnCancel.Click += (s, e) => this.Close();
        }

        private void CbRole_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbRole.SelectedItem == null) return;
            string selected = cbRole.SelectedValue?.ToString();
            if (selected == "KhachHang")
            {
                txtChucVu.Enabled = false;
                txtChucVu.Text = "";
                txtMaKhachHang.Enabled = true;
            }
            else
            {
                txtChucVu.Enabled = true;
                txtMaKhachHang.Enabled = false;
                txtMaKhachHang.Text = "";
            }
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                string userId = txtMaNguoiDung.Text.Trim();
                string username = txtTenDangNhap.Text.Trim();
                string password = txtMatKhau.Text.Trim();
                string hoten = txtHoTen.Text.Trim();
                string email = txtEmail.Text.Trim();
                string phone = txtSoDienThoai.Text.Trim();
                string role = cbRole.SelectedValue?.ToString();

                if (string.IsNullOrEmpty(userId)) throw new ArgumentException("Mã người dùng không được để trống.");
                if (string.IsNullOrEmpty(username)) throw new ArgumentException("Tên đăng nhập không được để trống.");
                if (!_isEdit && string.IsNullOrEmpty(password)) throw new ArgumentException("Mật khẩu không được để trống.");
                if (string.IsNullOrEmpty(hoten)) throw new ArgumentException("Họ tên không được để trống.");
                if (string.IsNullOrEmpty(email)) throw new ArgumentException("Email không được để trống.");
                if (string.IsNullOrEmpty(phone)) throw new ArgumentException("Số điện thoại không được để trống.");

                if (!Enum.TryParse(cbTrangThai.SelectedValue?.ToString(), out TrangThaiTaiKhoan tt))
                    tt = TrangThaiTaiKhoan.HoatDong;

                // Self lock protection check
                if (_isEdit && tt == TrangThaiTaiKhoan.BiKhoa && userId == _currentUserId)
                {
                    throw new ArgumentException("Bạn không thể tự khóa tài khoản của chính mình!");
                }

                // Construct structures
                if (role == "Admin")
                {
                    User = new Admin { MaNguoiDung = userId, HoTen = hoten, Email = email, SoDienThoai = phone, DiaChi = txtDiaChi.Text.Trim(), MaNhanVien = "NV" + Guid.NewGuid().ToString().Substring(0, 5).ToUpper(), ChucVu = txtChucVu.Text.Trim() };
                }
                else if (role == "NhanVien")
                {
                    User = new NhanVien { MaNguoiDung = userId, HoTen = hoten, Email = email, SoDienThoai = phone, DiaChi = txtDiaChi.Text.Trim(), MaNhanVien = "NV" + Guid.NewGuid().ToString().Substring(0, 5).ToUpper(), ChucVu = txtChucVu.Text.Trim() };
                }
                else
                {
                    User = new KhachHang { MaNguoiDung = userId, HoTen = hoten, Email = email, SoDienThoai = phone, DiaChi = txtDiaChi.Text.Trim(), MaKhachHang = txtMaKhachHang.Text.Trim().ToUpper(), NgayDangKy = DateTime.Now };
                }

                Account.MaNguoiDung = userId;
                Account.TenDangNhap = username;
                Account.MatKhau = password; // Set raw password (hashing handled in UseCase)
                Account.TrangThaiTaiKhoan = tt;
                Role = role;

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                Toast.Show(ex.Message, "danger");
            }
        }
    }
}
