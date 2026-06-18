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
            this.pnlMain = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.tblFields = new System.Windows.Forms.TableLayoutPanel();
            this.lblMaNguoiDung = new System.Windows.Forms.Label();
            this.txtMaNguoiDung = new Guna.UI2.WinForms.Guna2TextBox();
            this.lblTenDangNhap = new System.Windows.Forms.Label();
            this.txtTenDangNhap = new Guna.UI2.WinForms.Guna2TextBox();
            this.lblRole = new System.Windows.Forms.Label();
            this.cbRole = new Guna.UI2.WinForms.Guna2ComboBox();
            this.lblPasswordLabel = new System.Windows.Forms.Label();
            this.txtMatKhau = new Guna.UI2.WinForms.Guna2TextBox();
            this.btnShowHide = new System.Windows.Forms.Button();
            this.lblHoTen = new System.Windows.Forms.Label();
            this.txtHoTen = new Guna.UI2.WinForms.Guna2TextBox();
            this.lblEmail = new System.Windows.Forms.Label();
            this.txtEmail = new Guna.UI2.WinForms.Guna2TextBox();
            this.lblSoDienThoai = new System.Windows.Forms.Label();
            this.txtSoDienThoai = new Guna.UI2.WinForms.Guna2TextBox();
            this.lblDiaChi = new System.Windows.Forms.Label();
            this.txtDiaChi = new Guna.UI2.WinForms.Guna2TextBox();
            this.lblChucVu = new System.Windows.Forms.Label();
            this.txtChucVu = new Guna.UI2.WinForms.Guna2TextBox();
            this.lblMaKhachHang = new System.Windows.Forms.Label();
            this.txtMaKhachHang = new Guna.UI2.WinForms.Guna2TextBox();
            this.lblTrangThai = new System.Windows.Forms.Label();
            this.cbTrangThai = new Guna.UI2.WinForms.Guna2ComboBox();
            this.btnCancel = new Guna.UI2.WinForms.Guna2Button();
            this.btnSave = new Guna.UI2.WinForms.Guna2Button();

            // 
            // pnlMain
            // 
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Padding = new System.Windows.Forms.Padding(20);
            this.pnlMain.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.pnlMain);

            // 
            // lblTitle
            // 
            this.lblTitle.Text = "TÀI KHOẢN";
            this.lblTitle.Font = ThemeHelper.FontSubheading;
            this.lblTitle.ForeColor = ThemeHelper.Primary;
            this.lblTitle.AutoSize = true;
            this.lblTitle.Location = new System.Drawing.Point(20, 15);
            this.pnlMain.Controls.Add(this.lblTitle);

            // 
            // tblFields
            // 
            this.tblFields.Location = new System.Drawing.Point(20, 50);
            this.tblFields.Size = new System.Drawing.Size(478, 510);
            this.tblFields.ColumnCount = 2;
            this.tblFields.RowCount = 11;
            this.tblFields.BackColor = System.Drawing.Color.Transparent;
            this.tblFields.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 150));
            this.tblFields.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100));
            this.tblFields.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 9.09F));
            this.tblFields.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 9.09F));
            this.tblFields.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 9.09F));
            this.tblFields.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 9.09F));
            this.tblFields.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 9.09F));
            this.tblFields.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 9.09F));
            this.tblFields.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 9.09F));
            this.tblFields.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 9.09F));
            this.tblFields.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 9.09F));
            this.tblFields.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 9.09F));
            this.tblFields.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 9.09F));
            this.pnlMain.Controls.Add(this.tblFields);

            // 
            // lblMaNguoiDung
            // 
            this.lblMaNguoiDung.Text = "Mã người dùng *";
            this.lblMaNguoiDung.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblMaNguoiDung.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblMaNguoiDung.Font = ThemeHelper.FontCaptionBold;
            this.lblMaNguoiDung.ForeColor = ThemeHelper.TextSecondary;
            this.lblMaNguoiDung.Padding = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this.tblFields.Controls.Add(this.lblMaNguoiDung, 0, 0);

            // 
            // txtMaNguoiDung
            // 
            this.txtMaNguoiDung.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtMaNguoiDung.Size = new System.Drawing.Size(200, 32);
            this.txtMaNguoiDung.BorderRadius = 8;
            this.txtMaNguoiDung.BorderColor = ThemeHelper.Border;
            this.txtMaNguoiDung.Font = ThemeHelper.FontBody;
            this.txtMaNguoiDung.ForeColor = ThemeHelper.Text;
            this.txtMaNguoiDung.PlaceholderText = "Ví dụ: admin, nv01, kh01...";
            this.txtMaNguoiDung.Text = "admin";
            this.tblFields.Controls.Add(this.txtMaNguoiDung, 1, 0);

            // 
            // lblTenDangNhap
            // 
            this.lblTenDangNhap.Text = "Tên đăng nhập *";
            this.lblTenDangNhap.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTenDangNhap.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblTenDangNhap.Font = ThemeHelper.FontCaptionBold;
            this.lblTenDangNhap.ForeColor = ThemeHelper.TextSecondary;
            this.lblTenDangNhap.Padding = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this.tblFields.Controls.Add(this.lblTenDangNhap, 0, 1);

            // 
            // txtTenDangNhap
            // 
            this.txtTenDangNhap.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtTenDangNhap.Size = new System.Drawing.Size(200, 32);
            this.txtTenDangNhap.BorderRadius = 8;
            this.txtTenDangNhap.BorderColor = ThemeHelper.Border;
            this.txtTenDangNhap.Font = ThemeHelper.FontBody;
            this.txtTenDangNhap.ForeColor = ThemeHelper.Text;
            this.txtTenDangNhap.PlaceholderText = "Tên đăng nhập duy nhất...";
            this.txtTenDangNhap.Text = "admin_test";
            this.tblFields.Controls.Add(this.txtTenDangNhap, 1, 1);

            // 
            // lblRole
            // 
            this.lblRole.Text = "Vai trò";
            this.lblRole.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblRole.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblRole.Font = ThemeHelper.FontCaptionBold;
            this.lblRole.ForeColor = ThemeHelper.TextSecondary;
            this.lblRole.Padding = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this.tblFields.Controls.Add(this.lblRole, 0, 2);

            // 
            // cbRole
            // 
            this.cbRole.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblFields.Controls.Add(this.cbRole, 1, 2);

            // 
            // lblPasswordLabel
            // 
            this.lblPasswordLabel.Text = "Mật khẩu *";
            this.lblPasswordLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblPasswordLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblPasswordLabel.Font = ThemeHelper.FontCaptionBold;
            this.lblPasswordLabel.ForeColor = ThemeHelper.TextSecondary;
            this.lblPasswordLabel.Padding = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this.tblFields.Controls.Add(this.lblPasswordLabel, 0, 3);

            // 
            // txtMatKhau
            // 
            this.txtMatKhau.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtMatKhau.Size = new System.Drawing.Size(200, 32);
            this.txtMatKhau.BorderRadius = 8;
            this.txtMatKhau.BorderColor = ThemeHelper.Border;
            this.txtMatKhau.Font = ThemeHelper.FontBody;
            this.txtMatKhau.ForeColor = ThemeHelper.Text;
            this.txtMatKhau.PasswordChar = '●';
            this.txtMatKhau.UseSystemPasswordChar = true;
            this.txtMatKhau.PlaceholderText = "Nhập mật khẩu...";
            this.txtMatKhau.Text = "********";
            this.tblFields.Controls.Add(this.txtMatKhau, 1, 3);

            // 
            // btnShowHide
            // 
            this.btnShowHide.Text = "👁";
            this.btnShowHide.Size = new System.Drawing.Size(30, 26);
            this.btnShowHide.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnShowHide.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnShowHide.BackColor = System.Drawing.Color.White;
            this.btnShowHide.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnShowHide.FlatAppearance.BorderSize = 0;
            this.txtMatKhau.Controls.Add(this.btnShowHide);
            this.btnShowHide.BringToFront();

            // 
            // lblHoTen
            // 
            this.lblHoTen.Text = "Họ và tên *";
            this.lblHoTen.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblHoTen.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblHoTen.Font = ThemeHelper.FontCaptionBold;
            this.lblHoTen.ForeColor = ThemeHelper.TextSecondary;
            this.lblHoTen.Padding = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this.tblFields.Controls.Add(this.lblHoTen, 0, 4);

            // 
            // txtHoTen
            // 
            this.txtHoTen.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtHoTen.Size = new System.Drawing.Size(200, 32);
            this.txtHoTen.BorderRadius = 8;
            this.txtHoTen.BorderColor = ThemeHelper.Border;
            this.txtHoTen.Font = ThemeHelper.FontBody;
            this.txtHoTen.ForeColor = ThemeHelper.Text;
            this.txtHoTen.PlaceholderText = "Nhập đầy đủ họ tên...";
            this.txtHoTen.Text = "Lê Bình";
            this.tblFields.Controls.Add(this.txtHoTen, 1, 4);

            // 
            // lblEmail
            // 
            this.lblEmail.Text = "Email *";
            this.lblEmail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblEmail.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblEmail.Font = ThemeHelper.FontCaptionBold;
            this.lblEmail.ForeColor = ThemeHelper.TextSecondary;
            this.lblEmail.Padding = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this.tblFields.Controls.Add(this.lblEmail, 0, 5);

            // 
            // txtEmail
            // 
            this.txtEmail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtEmail.Size = new System.Drawing.Size(200, 32);
            this.txtEmail.BorderRadius = 8;
            this.txtEmail.BorderColor = ThemeHelper.Border;
            this.txtEmail.Font = ThemeHelper.FontBody;
            this.txtEmail.ForeColor = ThemeHelper.Text;
            this.txtEmail.PlaceholderText = "Ví dụ: user@gmail.com";
            this.txtEmail.Text = "lebinh@gmail.com";
            this.tblFields.Controls.Add(this.txtEmail, 1, 5);

            // 
            // lblSoDienThoai
            // 
            this.lblSoDienThoai.Text = "Số điện thoại *";
            this.lblSoDienThoai.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblSoDienThoai.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblSoDienThoai.Font = ThemeHelper.FontCaptionBold;
            this.lblSoDienThoai.ForeColor = ThemeHelper.TextSecondary;
            this.lblSoDienThoai.Padding = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this.tblFields.Controls.Add(this.lblSoDienThoai, 0, 6);

            // 
            // txtSoDienThoai
            // 
            this.txtSoDienThoai.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtSoDienThoai.Size = new System.Drawing.Size(200, 32);
            this.txtSoDienThoai.BorderRadius = 8;
            this.txtSoDienThoai.BorderColor = ThemeHelper.Border;
            this.txtSoDienThoai.Font = ThemeHelper.FontBody;
            this.txtSoDienThoai.ForeColor = ThemeHelper.Text;
            this.txtSoDienThoai.PlaceholderText = "Ví dụ: 0912345678";
            this.txtSoDienThoai.Text = "0912345678";
            this.tblFields.Controls.Add(this.txtSoDienThoai, 1, 6);

            // 
            // lblDiaChi
            // 
            this.lblDiaChi.Text = "Địa chỉ";
            this.lblDiaChi.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblDiaChi.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblDiaChi.Font = ThemeHelper.FontCaptionBold;
            this.lblDiaChi.ForeColor = ThemeHelper.TextSecondary;
            this.lblDiaChi.Padding = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this.tblFields.Controls.Add(this.lblDiaChi, 0, 7);

            // 
            // txtDiaChi
            // 
            this.txtDiaChi.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtDiaChi.Size = new System.Drawing.Size(200, 32);
            this.txtDiaChi.BorderRadius = 8;
            this.txtDiaChi.BorderColor = ThemeHelper.Border;
            this.txtDiaChi.Font = ThemeHelper.FontBody;
            this.txtDiaChi.ForeColor = ThemeHelper.Text;
            this.txtDiaChi.PlaceholderText = "Số nhà, tên đường...";
            this.txtDiaChi.Text = "123 Đường 3/2, Cần Thơ";
            this.tblFields.Controls.Add(this.txtDiaChi, 1, 7);

            // 
            // lblChucVu
            // 
            this.lblChucVu.Text = "Chức vụ (Admin/Staff)";
            this.lblChucVu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblChucVu.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblChucVu.Font = ThemeHelper.FontCaptionBold;
            this.lblChucVu.ForeColor = ThemeHelper.TextSecondary;
            this.lblChucVu.Padding = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this.tblFields.Controls.Add(this.lblChucVu, 0, 8);

            // 
            // txtChucVu
            // 
            this.txtChucVu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtChucVu.Size = new System.Drawing.Size(200, 32);
            this.txtChucVu.BorderRadius = 8;
            this.txtChucVu.BorderColor = ThemeHelper.Border;
            this.txtChucVu.Font = ThemeHelper.FontBody;
            this.txtChucVu.ForeColor = ThemeHelper.Text;
            this.txtChucVu.PlaceholderText = "Ví dụ: Thu ngân, Quản kho...";
            this.txtChucVu.Text = "Quản trị viên";
            this.tblFields.Controls.Add(this.txtChucVu, 1, 8);

            // 
            // lblMaKhachHang
            // 
            this.lblMaKhachHang.Text = "Mã khách hàng (KH)";
            this.lblMaKhachHang.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblMaKhachHang.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblMaKhachHang.Font = ThemeHelper.FontCaptionBold;
            this.lblMaKhachHang.ForeColor = ThemeHelper.TextSecondary;
            this.lblMaKhachHang.Padding = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this.tblFields.Controls.Add(this.lblMaKhachHang, 0, 9);

            // 
            // txtMaKhachHang
            // 
            this.txtMaKhachHang.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtMaKhachHang.Size = new System.Drawing.Size(200, 32);
            this.txtMaKhachHang.BorderRadius = 8;
            this.txtMaKhachHang.BorderColor = ThemeHelper.Border;
            this.txtMaKhachHang.Font = ThemeHelper.FontBody;
            this.txtMaKhachHang.ForeColor = ThemeHelper.Text;
            this.txtMaKhachHang.PlaceholderText = "Ví dụ: KH1023";
            this.txtMaKhachHang.Text = "KH1023";
            this.tblFields.Controls.Add(this.txtMaKhachHang, 1, 9);

            // 
            // lblTrangThai
            // 
            this.lblTrangThai.Text = "Trạng thái";
            this.lblTrangThai.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTrangThai.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblTrangThai.Font = ThemeHelper.FontCaptionBold;
            this.lblTrangThai.ForeColor = ThemeHelper.TextSecondary;
            this.lblTrangThai.Padding = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this.tblFields.Controls.Add(this.lblTrangThai, 0, 10);

            // 
            // cbTrangThai
            // 
            this.cbTrangThai.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cbTrangThai.Margin = new System.Windows.Forms.Padding(0);
            this.tblFields.Controls.Add(this.cbTrangThai, 1, 10);

            // 
            // btnCancel
            // 
            this.btnCancel.Text = "Hủy bỏ";
            this.btnCancel.Location = new System.Drawing.Point(280, 575);
            this.btnCancel.Size = new System.Drawing.Size(100, 38);
            this.btnCancel.BorderRadius = 19;
            this.btnCancel.FillColor = ThemeHelper.BorderLight;
            this.btnCancel.HoverState.FillColor = ThemeHelper.Border;
            this.btnCancel.Font = ThemeHelper.FontBodyBold;
            this.btnCancel.ForeColor = ThemeHelper.TextSecondary;
            this.btnCancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pnlMain.Controls.Add(this.btnCancel);

            // 
            // btnSave
            // 
            this.btnSave.Text = "Lưu lại";
            this.btnSave.Location = new System.Drawing.Point(390, 575);
            this.btnSave.Size = new System.Drawing.Size(110, 38);
            this.btnSave.BorderRadius = 19;
            this.btnSave.FillColor = ThemeHelper.Success;
            this.btnSave.HoverState.FillColor = System.Drawing.Color.FromArgb(4, 120, 87);
            this.btnSave.Font = ThemeHelper.FontBodyBold;
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pnlMain.Controls.Add(this.btnSave);

            // FORM properties
            this.Size = new System.Drawing.Size(520, 630);
            this.BackColor = System.Drawing.Color.White;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
        }

        #endregion

        private System.Windows.Forms.Panel pnlMain;
        private System.Windows.Forms.TableLayoutPanel tblFields;
        private System.Windows.Forms.Label lblMaNguoiDung;
        private System.Windows.Forms.Label lblTenDangNhap;
        private System.Windows.Forms.Label lblRole;
        private System.Windows.Forms.Button btnShowHide;
        private System.Windows.Forms.Label lblHoTen;
        private System.Windows.Forms.Label lblEmail;
        private System.Windows.Forms.Label lblSoDienThoai;
        private System.Windows.Forms.Label lblDiaChi;
        private System.Windows.Forms.Label lblChucVu;
        private System.Windows.Forms.Label lblMaKhachHang;
        private System.Windows.Forms.Label lblTrangThai;

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
