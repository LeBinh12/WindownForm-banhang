using System;
using System.Drawing;
using System.Windows.Forms;
using Guna.UI2.WinForms;
using QuanLyCuaHangTapHoa.Domain;

namespace QuanLyCuaHangTapHoa.Presentation.Modals
{
    public class FormTaiKhoanInput : Form
    {
        public NguoiDung User { get; private set; }
        public TaiKhoan Account { get; private set; }
        public string Role { get; private set; }
        
        private readonly bool _isEdit;
        private readonly string _currentUserId;

        private Guna2TextBox txtMaNguoiDung;
        private Guna2TextBox txtTenDangNhap;
        private Guna2ComboBox cbRole;
        private Guna2TextBox txtMatKhau;
        private Guna2TextBox txtHoTen;
        private Guna2TextBox txtEmail;
        private Guna2TextBox txtSoDienThoai;
        private Guna2TextBox txtDiaChi;
        private Guna2TextBox txtChucVu;
        private Guna2TextBox txtMaKhachHang;
        private Guna2ComboBox cbTrangThai;

        private Guna2Button btnSave;
        private Guna2Button btnCancel;

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
        }

        private void InitializeComponent()
        {
            this.Size = new Size(520, 630);
            this.BackColor = Color.White;
            this.FormBorderStyle = FormBorderStyle.None;
            this.StartPosition = FormStartPosition.CenterParent;

            Panel pnlMain = new Panel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(20),
                BorderStyle = BorderStyle.FixedSingle
            };
            this.Controls.Add(pnlMain);

            Label lblTitle = new Label
            {
                Text = _isEdit ? "CẬP NHẬT TÀI KHOẢN" : "TẠO TÀI KHOẢN MỚI",
                Font = ThemeHelper.FontSubheading,
                ForeColor = ThemeHelper.Primary,
                AutoSize = true,
                Location = new Point(20, 15)
            };
            pnlMain.Controls.Add(lblTitle);

            // TableLayout
            TableLayoutPanel tblFields = new TableLayoutPanel
            {
                Location = new Point(20, 50),
                Size = new Size(478, 510),
                ColumnCount = 2,
                RowCount = 11,
                BackColor = Color.Transparent
            };
            tblFields.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 150));
            tblFields.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));

            for (int i = 0; i < 11; i++)
            {
                tblFields.RowStyles.Add(new RowStyle(SizeType.Percent, 9.09F));
            }
            pnlMain.Controls.Add(tblFields);

            // Row 0: MaNguoiDung
            tblFields.Controls.Add(CreateLabel("Mã người dùng *"), 0, 0);
            txtMaNguoiDung = CreateTextBox(User?.MaNguoiDung, _isEdit);
            txtMaNguoiDung.PlaceholderText = "Ví dụ: admin, nv01, kh01...";
            tblFields.Controls.Add(txtMaNguoiDung, 1, 0);

            // Row 1: TenDangNhap
            tblFields.Controls.Add(CreateLabel("Tên đăng nhập *"), 0, 1);
            txtTenDangNhap = CreateTextBox(Account.TenDangNhap, _isEdit);
            txtTenDangNhap.PlaceholderText = "Tên đăng nhập duy nhất...";
            tblFields.Controls.Add(txtTenDangNhap, 1, 1);

            // Row 2: Role
            tblFields.Controls.Add(CreateLabel("Vai trò"), 0, 2);
            cbRole = new Guna2ComboBox
            {
                Dock = DockStyle.Fill,
                Enabled = !_isEdit // Lock role on edit to prevent identity shift
            };
            ThemeHelper.StyleComboBox(cbRole);
            cbRole.DataSource = EnumTranslator.TranslateVaiTro(false);
            cbRole.DisplayMember = "Text";
            cbRole.ValueMember = "Value";
            cbRole.SelectedValue = Role ?? "NhanVien";
            cbRole.SelectedIndexChanged += CbRole_SelectedIndexChanged;
            tblFields.Controls.Add(cbRole, 1, 2);

            // Row 3: MatKhau
            tblFields.Controls.Add(CreateLabel(_isEdit ? "Mật khẩu mới (bỏ trống để giữ)" : "Mật khẩu *"), 0, 3);
            txtMatKhau = CreateTextBox("");
            txtMatKhau.PasswordChar = '●';
            txtMatKhau.UseSystemPasswordChar = true;
            txtMatKhau.PlaceholderText = _isEdit ? "Bỏ trống nếu không đổi..." : "Nhập mật khẩu...";
            
            Button btnShowHide = new Button
            {
                Text = "👁",
                Size = new Size(30, 26),
                Dock = DockStyle.Right,
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.White,
                Cursor = Cursors.Hand
            };
            btnShowHide.FlatAppearance.BorderSize = 0;
            btnShowHide.Click += (s, e) =>
            {
                txtMatKhau.UseSystemPasswordChar = !txtMatKhau.UseSystemPasswordChar;
                txtMatKhau.PasswordChar = txtMatKhau.UseSystemPasswordChar ? '●' : '\0';
                btnShowHide.Text = txtMatKhau.UseSystemPasswordChar ? "👁" : "🙈";
            };
            txtMatKhau.Controls.Add(btnShowHide);
            btnShowHide.BringToFront();
            
            tblFields.Controls.Add(txtMatKhau, 1, 3);

            // Row 4: HoTen
            tblFields.Controls.Add(CreateLabel("Họ và tên *"), 0, 4);
            txtHoTen = CreateTextBox(User?.HoTen);
            txtHoTen.PlaceholderText = "Nhập đầy đủ họ tên...";
            tblFields.Controls.Add(txtHoTen, 1, 4);

            // Row 5: Email
            tblFields.Controls.Add(CreateLabel("Email *"), 0, 5);
            txtEmail = CreateTextBox(User?.Email);
            txtEmail.PlaceholderText = "Ví dụ: user@gmail.com";
            tblFields.Controls.Add(txtEmail, 1, 5);

            // Row 6: SoDienThoai
            tblFields.Controls.Add(CreateLabel("Số điện thoại *"), 0, 6);
            txtSoDienThoai = CreateTextBox(User?.SoDienThoai);
            txtSoDienThoai.PlaceholderText = "Ví dụ: 0912345678";
            tblFields.Controls.Add(txtSoDienThoai, 1, 6);

            // Row 7: DiaChi
            tblFields.Controls.Add(CreateLabel("Địa chỉ"), 0, 7);
            txtDiaChi = CreateTextBox(User?.DiaChi);
            txtDiaChi.PlaceholderText = "Số nhà, tên đường...";
            tblFields.Controls.Add(txtDiaChi, 1, 7);

            // Row 8: ChucVu
            tblFields.Controls.Add(CreateLabel("Chức vụ (Admin/Staff)"), 0, 8);
            string initialChucVu = "";
            if (User is Admin ad) initialChucVu = ad.ChucVu;
            else if (User is NhanVien nv) initialChucVu = nv.ChucVu;
            txtChucVu = CreateTextBox(initialChucVu);
            txtChucVu.PlaceholderText = "Ví dụ: Thu ngân, Quản kho...";
            tblFields.Controls.Add(txtChucVu, 1, 8);

            // Row 9: MaKhachHang
            tblFields.Controls.Add(CreateLabel("Mã khách hàng (KH)"), 0, 9);
            string initialKH = "";
            if (User is KhachHang kh) initialKH = kh.MaKhachHang;
            txtMaKhachHang = CreateTextBox(initialKH);
            txtMaKhachHang.PlaceholderText = "Ví dụ: KH1023";
            tblFields.Controls.Add(txtMaKhachHang, 1, 9);

            // Row 10: TrangThai
            tblFields.Controls.Add(CreateLabel("Trạng thái"), 0, 10);
            cbTrangThai = new Guna2ComboBox
            {
                Dock = DockStyle.Fill,
                Margin = new Padding(0)
            };
            ThemeHelper.StyleComboBox(cbTrangThai);
            cbTrangThai.DataSource = EnumTranslator.TranslateTrangThaiTaiKhoan(false);
            cbTrangThai.DisplayMember = "Text";
            cbTrangThai.ValueMember = "Value";
            cbTrangThai.SelectedValue = _isEdit ? Account.TrangThaiTaiKhoan : TrangThaiTaiKhoan.HoatDong;
            tblFields.Controls.Add(cbTrangThai, 1, 10);

            // Apply lock triggers initial
            CbRole_SelectedIndexChanged(null, null);

            // Cancel and Save buttons
            btnCancel = new Guna2Button
            {
                Text = "Hủy bỏ",
                Location = new Point(280, 575),
                Size = new Size(100, 38),
                BorderRadius = 19,
                FillColor = ThemeHelper.BorderLight,
                HoverState = { FillColor = ThemeHelper.Border },
                Font = ThemeHelper.FontBodyBold,
                ForeColor = ThemeHelper.TextSecondary,
                Cursor = Cursors.Hand
            };
            btnCancel.Click += (s, e) => this.Close();
            pnlMain.Controls.Add(btnCancel);

            btnSave = new Guna2Button
            {
                Text = "Lưu lại",
                Location = new Point(390, 575),
                Size = new Size(110, 38),
                BorderRadius = 19,
                FillColor = ThemeHelper.Success,
                HoverState = { FillColor = Color.FromArgb(4, 120, 87) },
                Font = ThemeHelper.FontBodyBold,
                ForeColor = Color.White,
                Cursor = Cursors.Hand
            };
            btnSave.Click += BtnSave_Click;
            pnlMain.Controls.Add(btnSave);
        }

        private void CbRole_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selected = cbRole.SelectedItem?.ToString();
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

        private Label CreateLabel(string text)
        {
            return new Label
            {
                Text = text,
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleRight,
                Font = ThemeHelper.FontCaptionBold,
                ForeColor = ThemeHelper.TextSecondary,
                Padding = new Padding(0, 0, 10, 0)
            };
        }

        private Guna2TextBox CreateTextBox(string val, bool isReadOnly = false)
        {
            return new Guna2TextBox
            {
                Dock = DockStyle.Fill,
                Size = new Size(200, 32),
                BorderRadius = 8,
                BorderColor = ThemeHelper.Border,
                Font = ThemeHelper.FontBody,
                ForeColor = ThemeHelper.Text,
                ReadOnly = isReadOnly,
                Text = val ?? ""
            };
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
                string role = cbRole.SelectedItem?.ToString();

                if (string.IsNullOrEmpty(userId)) throw new ArgumentException("Mã người dùng không được để trống.");
                if (string.IsNullOrEmpty(username)) throw new ArgumentException("Tên đăng nhập không được để trống.");
                if (!_isEdit && string.IsNullOrEmpty(password)) throw new ArgumentException("Mật khẩu không được để trống.");
                if (string.IsNullOrEmpty(hoten)) throw new ArgumentException("Họ tên không được để trống.");
                if (string.IsNullOrEmpty(email)) throw new ArgumentException("Email không được để trống.");
                if (string.IsNullOrEmpty(phone)) throw new ArgumentException("Số điện thoại không được để trống.");

                if (!Enum.TryParse(cbTrangThai.SelectedItem?.ToString(), out TrangThaiTaiKhoan tt))
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
