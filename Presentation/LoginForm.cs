using System;
using System.Drawing;
using System.Windows.Forms;
using QuanLyCuaHangTapHoa.Application.Interfaces;
using QuanLyCuaHangTapHoa.Domain;

namespace QuanLyCuaHangTapHoa.Presentation
{
    public partial class LoginForm : Form
    {
        private readonly IAccountUseCase _accountUseCase;
        private readonly IProductUseCase _productUseCase;
        private readonly IOrderUseCase _orderUseCase;
        private readonly IInvoiceUseCase _invoiceUseCase;

        // Custom dragging fields
        private bool _isDragging = false;
        private Point _dragStartPoint = new Point(0, 0);

        // Parameterless constructor for Visual Studio Designer
        public LoginForm()
        {
            InitializeComponent();
            SetupEvents();
        }

        public LoginForm(
            IAccountUseCase accountUseCase,
            IProductUseCase productUseCase,
            IOrderUseCase orderUseCase,
            IInvoiceUseCase invoiceUseCase)
        {
            _accountUseCase = accountUseCase;
            _productUseCase = productUseCase;
            _orderUseCase = orderUseCase;
            _invoiceUseCase = invoiceUseCase;

            InitializeComponent();
            SetupEvents();
        }

        private void SetupEvents()
        {
            if (System.ComponentModel.LicenseManager.CurrentContext.UsageMode == System.ComponentModel.LicenseUsageMode.Designtime)
                return;

            // Apply Theme & Round Corners
            ThemeHelper.StyleForm(this);
            ThemeHelper.RoundControl(this, 16);

            // Drag Handlers
            pnlLeft.MouseDown += TitleBar_MouseDown;
            pnlLeft.MouseMove += TitleBar_MouseMove;
            pnlLeft.MouseUp += TitleBar_MouseUp;

            pnlRight.MouseDown += TitleBar_MouseDown;
            pnlRight.MouseMove += TitleBar_MouseMove;
            pnlRight.MouseUp += TitleBar_MouseUp;

            // Custom Paint on Left Panel
            pnlLeft.Paint += (s, e) =>
            {
                using (var brush = new SolidBrush(Color.FromArgb(15, 255, 255, 255)))
                {
                    e.Graphics.FillEllipse(brush, -50, -50, 200, 200);
                    e.Graphics.FillEllipse(brush, 350, 480, 180, 180);
                    e.Graphics.FillEllipse(brush, 320, 50, 80, 80);
                }
            };

            // Title Bar Buttons
            btnClose.MouseEnter += (s, e) => btnClose.ForeColor = ThemeHelper.Danger;
            btnClose.MouseLeave += (s, e) => btnClose.ForeColor = ThemeHelper.TextSecondary;
            btnClose.Click += (s, e) => System.Windows.Forms.Application.Exit();

            btnMinimize.MouseEnter += (s, e) => btnMinimize.BackColor = Color.FromArgb(240, 240, 240);
            btnMinimize.MouseLeave += (s, e) => btnMinimize.BackColor = Color.Transparent;
            btnMinimize.Click += (s, e) => this.WindowState = FormWindowState.Minimized;

            // Login Button
            btnLogin.Click += BtnLogin_Click;

            // Forgot Password
            lnkForgotPassword.LinkClicked += (s, e) => MessageBox.Show(
                "Vui lòng liên hệ Quản Trị Viên hệ thống để cấp lại mật khẩu.", 
                "Quên mật khẩu", 
                MessageBoxButtons.OK, 
                MessageBoxIcon.Information);

            // Password Show/Hide Button
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
                txtPassword.UseSystemPasswordChar = !txtPassword.UseSystemPasswordChar;
                txtPassword.PasswordChar = txtPassword.UseSystemPasswordChar ? '●' : '\0';
                btnShowHide.Text = txtPassword.UseSystemPasswordChar ? "👁" : "🙈";
            };
            txtPassword.Controls.Add(btnShowHide);
            btnShowHide.BringToFront();
        }

        // Mouse Drag handlers for Form Movement
        private void TitleBar_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                _isDragging = true;
                _dragStartPoint = new Point(e.X, e.Y);
            }
        }

        private void TitleBar_MouseMove(object sender, MouseEventArgs e)
        {
            if (_isDragging)
            {
                Point p = PointToScreen(e.Location);
                this.Location = new Point(p.X - _dragStartPoint.X, p.Y - _dragStartPoint.Y);
            }
        }

        private void TitleBar_MouseUp(object sender, MouseEventArgs e)
        {
            _isDragging = false;
        }

        private void BtnLogin_Click(object sender, EventArgs e)
        {
            lblError.Text = "";
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text;

            try
            {
                var account = _accountUseCase.Login(username, password);
                if (account == null)
                {
                    lblError.Text = "Tên đăng nhập hoặc mật khẩu không chính xác. ❌";
                    return;
                }

                // Successful login -> Redirect to MainForm
                this.Hide();
                using (var mainForm = new MainForm(account, _productUseCase, _orderUseCase, _invoiceUseCase, _accountUseCase))
                {
                    var result = mainForm.ShowDialog();
                    if (result == DialogResult.OK)
                    {
                        txtPassword.Clear();
                        lblError.Text = "";
                        this.Show();
                    }
                    else
                    {
                        this.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message;
            }
        }
    }
}
