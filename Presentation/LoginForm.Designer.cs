using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using Guna.UI2.WinForms;

namespace QuanLyCuaHangTapHoa.Presentation
{
    partial class LoginForm
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
            this.Size = new Size(1000, 620);
            this.FormBorderStyle = FormBorderStyle.None;
            this.StartPosition = FormStartPosition.CenterScreen;

            // MAIN CONTAINER TABLE LAYOUT
            TableLayoutPanel mainLayout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 2,
                RowCount = 1,
                BackColor = Color.White
            };
            mainLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 45F)); // Left: 45%
            mainLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 55F)); // Right: 55%
            this.Controls.Add(mainLayout);

            // 1. LEFT PANEL (Branded Gradient)
            pnlLeft = new Guna2GradientPanel
            {
                Dock = DockStyle.Fill,
                FillColor = ThemeHelper.Primary,
                FillColor2 = Color.FromArgb(77, 142, 240),
                GradientMode = LinearGradientMode.BackwardDiagonal,
                Padding = new Padding(30)
            };


            lblTitleLeft = new Label
            {
                Text = "QUẢN LÝ CHUỖI\nCỬA HÀNG TẠP HÓA",
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 20F, FontStyle.Bold),
                Location = new Point(35, 60),
                Size = new Size(380, 80),
                BackColor = Color.Transparent
            };

            lblSlogan = new Label
            {
                Text = "Hệ thống quản lý thông minh và tinh gọn.",
                ForeColor = Color.FromArgb(235, 240, 255),
                Font = new Font("Segoe UI", 10.5F, FontStyle.Italic),
                Location = new Point(35, 150),
                Size = new Size(380, 30),
                BackColor = Color.Transparent
            };

            lblCartIcon = new Label
            {
                Text = "🛒",
                Font = new Font("Segoe UI", 120F),
                ForeColor = Color.FromArgb(60, 255, 255, 255), // semi-transparent white
                Location = new Point(60, 200),
                Size = new Size(330, 250),
                TextAlign = ContentAlignment.MiddleCenter,
                BackColor = Color.Transparent
            };



            pnlLeft.Controls.AddRange(new Control[] { lblTitleLeft, lblSlogan, lblCartIcon });
            mainLayout.Controls.Add(pnlLeft, 0, 0);

            // 2. RIGHT PANEL (Login Form Input)
            pnlRight = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.White,
                Padding = new Padding(40)
            };


            // Frameless Control Box (Top-Right)
            btnClose = new Button
            {
                Text = "✕",
                Size = new Size(32, 32),
                Location = new Point(510, 10),
                FlatStyle = FlatStyle.Flat,
                ForeColor = ThemeHelper.TextSecondary,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                Cursor = Cursors.Hand,
                BackColor = Color.Transparent
            };
            btnClose.FlatAppearance.BorderSize = 0;

            btnMinimize = new Button
            {
                Text = "—",
                Size = new Size(32, 32),
                Location = new Point(475, 10),
                FlatStyle = FlatStyle.Flat,
                ForeColor = ThemeHelper.TextSecondary,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                Cursor = Cursors.Hand,
                BackColor = Color.Transparent
            };
            btnMinimize.FlatAppearance.BorderSize = 0;

            pnlRight.Controls.AddRange(new Control[] { btnClose, btnMinimize });

            // Form container centered in the panel
            Panel formContainer = new Panel
            {
                Size = new Size(380, 420),
                Location = new Point(85, 100),
                BackColor = Color.Transparent
            };

            lblTitleRight = new Label
            {
                Text = "Đăng nhập",
                Font = ThemeHelper.FontH1,
                ForeColor = ThemeHelper.Text,
                Location = new Point(0, 10),
                AutoSize = true
            };

            lblSubtitleRight = new Label
            {
                Text = "Chào mừng bạn quay lại 👋",
                Font = ThemeHelper.FontBody,
                ForeColor = ThemeHelper.TextSecondary,
                Location = new Point(0, 45),
                AutoSize = true
            };

            // Username
            txtUsername = new Guna2TextBox
            {
                Location = new Point(0, 95),
                Size = new Size(380, 44),
                BorderRadius = 8,
                BorderColor = ThemeHelper.Border,
                Font = ThemeHelper.FontBody,
                ForeColor = ThemeHelper.Text,
                PlaceholderText = "Tên đăng nhập",
                Text = "admin"
            };

            // Password
            txtPassword = new Guna2TextBox
            {
                Location = new Point(0, 160),
                Size = new Size(380, 44),
                BorderRadius = 8,
                BorderColor = ThemeHelper.Border,
                Font = ThemeHelper.FontBody,
                ForeColor = ThemeHelper.Text,
                PlaceholderText = "Mật khẩu",
                PasswordChar = '●',
                UseSystemPasswordChar = true,
                Text = "admin123"
            };


            chkRemember = new CheckBox
            {
                Text = "Ghi nhớ đăng nhập",
                Font = ThemeHelper.FontCaption,
                ForeColor = ThemeHelper.TextSecondary,
                Location = new Point(2, 220),
                AutoSize = true
            };

            lnkForgotPassword = new LinkLabel
            {
                Text = "Quên mật khẩu?",
                Font = ThemeHelper.FontCaption,
                LinkColor = ThemeHelper.Primary,
                ActiveLinkColor = ThemeHelper.PrimaryHover,
                Location = new Point(275, 220),
                AutoSize = true,
                LinkBehavior = LinkBehavior.HoverUnderline
            };


            btnLogin = new Guna2Button
            {
                Text = "ĐĂNG NHẬP",
                Location = new Point(0, 270),
                Size = new Size(380, 46),
                BorderRadius = 23, // Pill format
                FillColor = ThemeHelper.Primary,
                HoverState = { FillColor = ThemeHelper.PrimaryHover },
                Font = ThemeHelper.FontBodyBold,
                ForeColor = Color.White,
                Cursor = Cursors.Hand
            };


            lblError = new Label
            {
                Text = "",
                ForeColor = ThemeHelper.Danger,
                Font = ThemeHelper.FontBodyBold,
                Location = new Point(0, 335),
                Size = new Size(380, 60),
                TextAlign = ContentAlignment.TopCenter
            };

            formContainer.Controls.AddRange(new Control[] {
                lblTitleRight, lblSubtitleRight, txtUsername, txtPassword,
                chkRemember, lnkForgotPassword, btnLogin, lblError
            });

            pnlRight.Controls.Add(formContainer);
            mainLayout.Controls.Add(pnlRight, 1, 0);

            // Accept enter button
            this.AcceptButton = btnLogin;
        }

        #endregion

        // UI Controls
        private Guna2GradientPanel pnlLeft;
        private Panel pnlRight;

        private Label lblTitleLeft;
        private Label lblSlogan;
        private Label lblCartIcon;

        private Label lblTitleRight;
        private Label lblSubtitleRight;

        private Guna2TextBox txtUsername;
        private Guna2TextBox txtPassword;
        private CheckBox chkRemember;
        private LinkLabel lnkForgotPassword;
        private Guna2Button btnLogin;
        private Label lblError;

        // Form Title Bar actions
        private Button btnClose;
        private Button btnMinimize;
    }
}
