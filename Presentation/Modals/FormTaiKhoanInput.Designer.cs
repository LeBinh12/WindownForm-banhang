using System;
using System.Drawing;
using System.Windows.Forms;
using Guna.UI2.WinForms;
using QuanLyCuaHangTapHoa.Domain;

namespace QuanLyCuaHangTapHoa.Presentation.Modals
{
    partial class FormTaiKhoanInput
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
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

            lblTitle = new Label
            {
                Text = "TÀI KHOẢN",
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

            tblFields.RowStyles.Add(new RowStyle(SizeType.Percent, 9.09F));
            tblFields.RowStyles.Add(new RowStyle(SizeType.Percent, 9.09F));
            tblFields.RowStyles.Add(new RowStyle(SizeType.Percent, 9.09F));
            tblFields.RowStyles.Add(new RowStyle(SizeType.Percent, 9.09F));
            tblFields.RowStyles.Add(new RowStyle(SizeType.Percent, 9.09F));
            tblFields.RowStyles.Add(new RowStyle(SizeType.Percent, 9.09F));
            tblFields.RowStyles.Add(new RowStyle(SizeType.Percent, 9.09F));
            tblFields.RowStyles.Add(new RowStyle(SizeType.Percent, 9.09F));
            tblFields.RowStyles.Add(new RowStyle(SizeType.Percent, 9.09F));
            tblFields.RowStyles.Add(new RowStyle(SizeType.Percent, 9.09F));
            tblFields.RowStyles.Add(new RowStyle(SizeType.Percent, 9.09F));
            pnlMain.Controls.Add(tblFields);

            // Row 0: MaNguoiDung
            tblFields.Controls.Add(CreateLabel("Mã người dùng *"), 0, 0);
            txtMaNguoiDung = CreateTextBox("", false);
            txtMaNguoiDung.PlaceholderText = "Ví dụ: admin, nv01, kh01...";
            tblFields.Controls.Add(txtMaNguoiDung, 1, 0);

            // Row 1: TenDangNhap
            tblFields.Controls.Add(CreateLabel("Tên đăng nhập *"), 0, 1);
            txtTenDangNhap = CreateTextBox("", false);
            txtTenDangNhap.PlaceholderText = "Tên đăng nhập duy nhất...";
            tblFields.Controls.Add(txtTenDangNhap, 1, 1);

            // Row 2: Role
            tblFields.Controls.Add(CreateLabel("Vai trò"), 0, 2);
            cbRole = new Guna2ComboBox
            {
                Dock = DockStyle.Fill
            };
            ThemeHelper.StyleComboBox(cbRole);
            tblFields.Controls.Add(cbRole, 1, 2);

            // Row 3: MatKhau
            lblPasswordLabel = CreateLabel("Mật khẩu *");
            tblFields.Controls.Add(lblPasswordLabel, 0, 3);
            txtMatKhau = CreateTextBox("");
            txtMatKhau.PasswordChar = '●';
            txtMatKhau.UseSystemPasswordChar = true;
            txtMatKhau.PlaceholderText = "Nhập mật khẩu...";
            
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
            txtHoTen = CreateTextBox("");
            txtHoTen.PlaceholderText = "Nhập đầy đủ họ tên...";
            tblFields.Controls.Add(txtHoTen, 1, 4);

            // Row 5: Email
            tblFields.Controls.Add(CreateLabel("Email *"), 0, 5);
            txtEmail = CreateTextBox("");
            txtEmail.PlaceholderText = "Ví dụ: user@gmail.com";
            tblFields.Controls.Add(txtEmail, 1, 5);

            // Row 6: SoDienThoai
            tblFields.Controls.Add(CreateLabel("Số điện thoại *"), 0, 6);
            txtSoDienThoai = CreateTextBox("");
            txtSoDienThoai.PlaceholderText = "Ví dụ: 0912345678";
            tblFields.Controls.Add(txtSoDienThoai, 1, 6);

            // Row 7: DiaChi
            tblFields.Controls.Add(CreateLabel("Địa chỉ"), 0, 7);
            txtDiaChi = CreateTextBox("");
            txtDiaChi.PlaceholderText = "Số nhà, tên đường...";
            tblFields.Controls.Add(txtDiaChi, 1, 7);

            // Row 8: ChucVu
            tblFields.Controls.Add(CreateLabel("Chức vụ (Admin/Staff)"), 0, 8);
            txtChucVu = CreateTextBox("");
            txtChucVu.PlaceholderText = "Ví dụ: Thu ngân, Quản kho...";
            tblFields.Controls.Add(txtChucVu, 1, 8);

            // Row 9: MaKhachHang
            tblFields.Controls.Add(CreateLabel("Mã khách hàng (KH)"), 0, 9);
            txtMaKhachHang = CreateTextBox("");
            txtMaKhachHang.PlaceholderText = "Ví dụ: KH1023";
            tblFields.Controls.Add(txtMaKhachHang, 1, 9);

            // Row 10: TrangThai
            tblFields.Controls.Add(CreateLabel("Trạng thái"), 0, 10);
            cbTrangThai = new Guna2ComboBox
            {
                Dock = DockStyle.Fill,
                Margin = new Padding(0)
            };
            tblFields.Controls.Add(cbTrangThai, 1, 10);

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

        #endregion

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
        private Label lblTitle;
        private Label lblPasswordLabel;
    }
}
