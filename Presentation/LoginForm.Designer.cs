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
            this.mainLayout = new System.Windows.Forms.TableLayoutPanel();
            this.pnlLeft = new Guna.UI2.WinForms.Guna2GradientPanel();
            this.lblTitleLeft = new System.Windows.Forms.Label();
            this.lblSlogan = new System.Windows.Forms.Label();
            this.lblCartIcon = new System.Windows.Forms.Label();
            this.pnlRight = new System.Windows.Forms.Panel();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnMinimize = new System.Windows.Forms.Button();
            this.formContainer = new System.Windows.Forms.Panel();
            this.lblTitleRight = new System.Windows.Forms.Label();
            this.lblSubtitleRight = new System.Windows.Forms.Label();
            this.txtUsername = new Guna.UI2.WinForms.Guna2TextBox();
            this.txtPassword = new Guna.UI2.WinForms.Guna2TextBox();
            this.chkRemember = new System.Windows.Forms.CheckBox();
            this.lnkForgotPassword = new System.Windows.Forms.LinkLabel();
            this.btnLogin = new Guna.UI2.WinForms.Guna2Button();
            this.lblError = new System.Windows.Forms.Label();

            // 
            // mainLayout
            // 
            this.mainLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainLayout.ColumnCount = 2;
            this.mainLayout.RowCount = 1;
            this.mainLayout.BackColor = System.Drawing.Color.White;
            this.mainLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 45F));
            this.mainLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 55F));
            this.Controls.Add(this.mainLayout);

            // 
            // pnlLeft
            // 
            this.pnlLeft.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlLeft.FillColor = ThemeHelper.Primary;
            this.pnlLeft.FillColor2 = System.Drawing.Color.FromArgb(77, 142, 240);
            this.pnlLeft.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.BackwardDiagonal;
            this.pnlLeft.Padding = new System.Windows.Forms.Padding(30);

            // 
            // lblTitleLeft
            // 
            this.lblTitleLeft.Text = "QUẢN LÝ CHUỖI\nCỬA HÀNG TẠP HÓA";
            this.lblTitleLeft.ForeColor = System.Drawing.Color.White;
            this.lblTitleLeft.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold);
            this.lblTitleLeft.Location = new System.Drawing.Point(35, 60);
            this.lblTitleLeft.Size = new System.Drawing.Size(380, 80);
            this.lblTitleLeft.BackColor = System.Drawing.Color.Transparent;

            // 
            // lblSlogan
            // 
            this.lblSlogan.Text = "Hệ thống quản lý thông minh và tinh gọn.";
            this.lblSlogan.ForeColor = System.Drawing.Color.FromArgb(235, 240, 255);
            this.lblSlogan.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Italic);
            this.lblSlogan.Location = new System.Drawing.Point(35, 150);
            this.lblSlogan.Size = new System.Drawing.Size(380, 30);
            this.lblSlogan.BackColor = System.Drawing.Color.Transparent;

            // 
            // lblCartIcon
            // 
            this.lblCartIcon.Text = "🛒";
            this.lblCartIcon.Font = new System.Drawing.Font("Segoe UI", 120F);
            this.lblCartIcon.ForeColor = System.Drawing.Color.FromArgb(60, 255, 255, 255);
            this.lblCartIcon.Location = new System.Drawing.Point(60, 200);
            this.lblCartIcon.Size = new System.Drawing.Size(330, 250);
            this.lblCartIcon.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblCartIcon.BackColor = System.Drawing.Color.Transparent;

            this.pnlLeft.Controls.Add(this.lblTitleLeft);
            this.pnlLeft.Controls.Add(this.lblSlogan);
            this.pnlLeft.Controls.Add(this.lblCartIcon);
            this.mainLayout.Controls.Add(this.pnlLeft, 0, 0);

            // 
            // pnlRight
            // 
            this.pnlRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlRight.BackColor = System.Drawing.Color.White;
            this.pnlRight.Padding = new System.Windows.Forms.Padding(40);

            // 
            // btnClose
            // 
            this.btnClose.Text = "✕";
            this.btnClose.Size = new System.Drawing.Size(32, 32);
            this.btnClose.Location = new System.Drawing.Point(510, 10);
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.ForeColor = ThemeHelper.TextSecondary;
            this.btnClose.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnClose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnClose.BackColor = System.Drawing.Color.Transparent;
            this.btnClose.FlatAppearance.BorderSize = 0;

            // 
            // btnMinimize
            // 
            this.btnMinimize.Text = "—";
            this.btnMinimize.Size = new System.Drawing.Size(32, 32);
            this.btnMinimize.Location = new System.Drawing.Point(475, 10);
            this.btnMinimize.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMinimize.ForeColor = ThemeHelper.TextSecondary;
            this.btnMinimize.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnMinimize.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnMinimize.BackColor = System.Drawing.Color.Transparent;
            this.btnMinimize.FlatAppearance.BorderSize = 0;

            this.pnlRight.Controls.Add(this.btnClose);
            this.pnlRight.Controls.Add(this.btnMinimize);

            // 
            // formContainer
            // 
            this.formContainer.Size = new System.Drawing.Size(380, 420);
            this.formContainer.Location = new System.Drawing.Point(85, 100);
            this.formContainer.BackColor = System.Drawing.Color.Transparent;

            // 
            // lblTitleRight
            // 
            this.lblTitleRight.Text = "Đăng nhập";
            this.lblTitleRight.Font = ThemeHelper.FontH1;
            this.lblTitleRight.ForeColor = ThemeHelper.Text;
            this.lblTitleRight.Location = new System.Drawing.Point(0, 10);
            this.lblTitleRight.AutoSize = true;

            // 
            // lblSubtitleRight
            // 
            this.lblSubtitleRight.Text = "Chào mừng bạn quay lại 👋";
            this.lblSubtitleRight.Font = ThemeHelper.FontBody;
            this.lblSubtitleRight.ForeColor = ThemeHelper.TextSecondary;
            this.lblSubtitleRight.Location = new System.Drawing.Point(0, 45);
            this.lblSubtitleRight.AutoSize = true;

            // 
            // txtUsername
            // 
            this.txtUsername.Location = new System.Drawing.Point(0, 95);
            this.txtUsername.Size = new System.Drawing.Size(380, 44);
            this.txtUsername.BorderRadius = 8;
            this.txtUsername.BorderColor = ThemeHelper.Border;
            this.txtUsername.Font = ThemeHelper.FontBody;
            this.txtUsername.ForeColor = ThemeHelper.Text;
            this.txtUsername.PlaceholderText = "Tên đăng nhập";
            this.txtUsername.Text = "admin";

            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(0, 160);
            this.txtPassword.Size = new System.Drawing.Size(380, 44);
            this.txtPassword.BorderRadius = 8;
            this.txtPassword.BorderColor = ThemeHelper.Border;
            this.txtPassword.Font = ThemeHelper.FontBody;
            this.txtPassword.ForeColor = ThemeHelper.Text;
            this.txtPassword.PlaceholderText = "Mật khẩu";
            this.txtPassword.PasswordChar = '●';
            this.txtPassword.UseSystemPasswordChar = true;
            this.txtPassword.Text = "admin123";

            // 
            // chkRemember
            // 
            this.chkRemember.Text = "Ghi nhớ đăng nhập";
            this.chkRemember.Font = ThemeHelper.FontCaption;
            this.chkRemember.ForeColor = ThemeHelper.TextSecondary;
            this.chkRemember.Location = new System.Drawing.Point(2, 220);
            this.chkRemember.AutoSize = true;

            // 
            // lnkForgotPassword
            // 
            this.lnkForgotPassword.Text = "Quên mật khẩu?";
            this.lnkForgotPassword.Font = ThemeHelper.FontCaption;
            this.lnkForgotPassword.LinkColor = ThemeHelper.Primary;
            this.lnkForgotPassword.ActiveLinkColor = ThemeHelper.PrimaryHover;
            this.lnkForgotPassword.Location = new System.Drawing.Point(275, 220);
            this.lnkForgotPassword.AutoSize = true;
            this.lnkForgotPassword.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;

            // 
            // btnLogin
            // 
            this.btnLogin.Text = "ĐĂNG NHẬP";
            this.btnLogin.Location = new System.Drawing.Point(0, 270);
            this.btnLogin.Size = new System.Drawing.Size(380, 46);
            this.btnLogin.BorderRadius = 23;
            this.btnLogin.FillColor = ThemeHelper.Primary;
            this.btnLogin.Font = ThemeHelper.FontBodyBold;
            this.btnLogin.ForeColor = System.Drawing.Color.White;
            this.btnLogin.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLogin.HoverState.FillColor = ThemeHelper.PrimaryHover;

            // 
            // lblError
            // 
            this.lblError.Text = "";
            this.lblError.ForeColor = ThemeHelper.Danger;
            this.lblError.Font = ThemeHelper.FontBodyBold;
            this.lblError.Location = new System.Drawing.Point(0, 335);
            this.lblError.Size = new System.Drawing.Size(380, 60);
            this.lblError.TextAlign = System.Drawing.ContentAlignment.TopCenter;

            this.formContainer.Controls.Add(this.lblTitleRight);
            this.formContainer.Controls.Add(this.lblSubtitleRight);
            this.formContainer.Controls.Add(this.txtUsername);
            this.formContainer.Controls.Add(this.txtPassword);
            this.formContainer.Controls.Add(this.chkRemember);
            this.formContainer.Controls.Add(this.lnkForgotPassword);
            this.formContainer.Controls.Add(this.btnLogin);
            this.formContainer.Controls.Add(this.lblError);

            this.pnlRight.Controls.Add(this.formContainer);
            this.mainLayout.Controls.Add(this.pnlRight, 1, 0);

            // FORM properties
            this.Size = new System.Drawing.Size(1000, 620);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.AcceptButton = this.btnLogin;
        }

        #endregion

        // UI Controls
        private System.Windows.Forms.TableLayoutPanel mainLayout;
        private System.Windows.Forms.Panel formContainer;
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
