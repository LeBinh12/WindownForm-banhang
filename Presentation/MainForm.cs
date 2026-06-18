using System;
using System.Drawing;
using System.Windows.Forms;
using Guna.UI2.WinForms;
using QuanLyCuaHangTapHoa.Application.Interfaces;
using QuanLyCuaHangTapHoa.Domain;
using QuanLyCuaHangTapHoa.Presentation.UserControls;

namespace QuanLyCuaHangTapHoa.Presentation
{
    public partial class MainForm : Form
    {
        private readonly TaiKhoan _currentUser;
        private readonly IProductUseCase _productUseCase;
        private readonly IOrderUseCase _orderUseCase;
        private readonly IInvoiceUseCase _invoiceUseCase;
        private readonly IAccountUseCase _accountUseCase;

        // Collapsed state
        private bool _isCollapsed = false;

        // Parameterless constructor for Visual Studio Designer
        public MainForm()
        {
            InitializeComponent();
            SetupEvents();
            SetupData();
        }

        public MainForm(
            TaiKhoan currentUser,
            IProductUseCase productUseCase,
            IOrderUseCase orderUseCase,
            IInvoiceUseCase invoiceUseCase,
            IAccountUseCase accountUseCase)
        {
            _currentUser = currentUser;
            _productUseCase = productUseCase;
            _orderUseCase = orderUseCase;
            _invoiceUseCase = invoiceUseCase;
            _accountUseCase = accountUseCase;

            InitializeComponent();
            SetupEvents();
            SetupData();
            UpdateUserProfileDisplay();
            ConfigureMenuAccess();
            ShowDefaultTab();
        }

        private void SetupEvents()
        {
            btnProducts.Click += (s, e) => SwitchTab(new ucSanPham(_productUseCase, _currentUser), btnProducts, "Sản phẩm & Quản lý Kho");
            btnOrders.Click += (s, e) => SwitchTab(new ucDonHang(_orderUseCase, _productUseCase, _currentUser), btnOrders, "Yêu cầu Đặt giữ hàng");
            btnPOS.Click += (s, e) => SwitchTab(new ucThanhToan(_invoiceUseCase, _orderUseCase, _productUseCase, _currentUser), btnPOS, "Điểm bán lẻ POS & Đổi trả");
            btnAccounts.Click += (s, e) => SwitchTab(new ucTaiKhoan(_accountUseCase, _currentUser), btnAccounts, "Quản trị Tài khoản");

            pnlTopbar.Paint += (s, e) =>
            {
                using (var pen = new Pen(ThemeHelper.BorderLight, 2))
                {
                    e.Graphics.DrawLine(pen, 0, pnlTopbar.Height - 1, pnlTopbar.Width, pnlTopbar.Height - 1);
                }
            };

            pnlTopbar.SizeChanged += (s, e) =>
            {
                lblAvatar.Left = pnlTopbar.Width - 60;
                lblBell.Left = pnlTopbar.Width - 100;
            };
        }

        private void SetupData()
        {
            ThemeHelper.StyleForm(this);
            if (_currentUser != null && !string.IsNullOrEmpty(_currentUser.TenDangNhap))
            {
                lblAvatar.Text = _currentUser.TenDangNhap.Substring(0, Math.Min(2, _currentUser.TenDangNhap.Length)).ToUpper();
            }
            else
            {
                lblAvatar.Text = "US";
            }
            ThemeHelper.RoundControl(lblAvatar, 17);
        }

        private Guna2Button CreateMenuButton(string text)
        {
            var btn = new Guna2Button
            {
                Text = text,
                Size = new Size(230, 48),
                FillColor = ThemeHelper.SidebarBg,
                ForeColor = ThemeHelper.TextMuted,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                TextAlign = HorizontalAlignment.Left,
                TextOffset = new Point(10, 0),
                Cursor = Cursors.Hand,
                BorderRadius = 0,
                Margin = new Padding(0)
            };
            btn.HoverState.FillColor = ThemeHelper.SidebarActiveBg;
            btn.HoverState.ForeColor = Color.White;
            return btn;
        }

        private void ConfigureMenuAccess()
        {
            if (_currentUser == null) return;
            var user = _currentUser.NguoiDung;

            if (user is Admin)
            {
                btnProducts.Visible = true;
                btnOrders.Visible = true;
                btnPOS.Visible = true;
                btnAccounts.Visible = true;
            }
            else if (user is NhanVien)
            {
                btnProducts.Visible = true;
                btnOrders.Visible = true;
                btnPOS.Visible = true;
                btnAccounts.Visible = false;
            }
            else
            {
                btnProducts.Visible = true;
                btnOrders.Visible = true;
                btnPOS.Visible = false;
                btnAccounts.Visible = false;
            }
        }

        private void UpdateUserProfileDisplay()
        {
            if (_currentUser == null) return;

            lblUserGreeting.Text = _currentUser.NguoiDung?.HoTen ?? "User";

            string roleText = "Khách Hàng";
            if (_currentUser.NguoiDung is Admin) roleText = "Quản trị viên";
            else if (_currentUser.NguoiDung is NhanVien) roleText = "Nhân viên";

            lblUserRole.Text = roleText.ToUpper();
        }

        private void SwitchTab(UserControl uc, Guna2Button activeMenuButton, string title)
        {
            if (activeMenuButton == null) return;

            // Reset styles
            btnProducts.FillColor = ThemeHelper.SidebarBg;
            btnProducts.ForeColor = ThemeHelper.TextMuted;
            btnOrders.FillColor = ThemeHelper.SidebarBg;
            btnOrders.ForeColor = ThemeHelper.TextMuted;
            btnPOS.FillColor = ThemeHelper.SidebarBg;
            btnPOS.ForeColor = ThemeHelper.TextMuted;
            btnAccounts.FillColor = ThemeHelper.SidebarBg;
            btnAccounts.ForeColor = ThemeHelper.TextMuted;

            // Activate button
            activeMenuButton.FillColor = ThemeHelper.SidebarActiveBg;
            activeMenuButton.ForeColor = Color.White;

            // Set Page Title
            lblPageTitle.Text = title;

            // Load User Control
            pnlContent.Controls.Clear();
            uc.Dock = DockStyle.Fill;
            pnlContent.Controls.Add(uc);

            // Move indicator
            pnlIndicator.Location = new Point(0, flowSidebar.Location.Y + activeMenuButton.Location.Y);
            pnlIndicator.Visible = true;
            pnlIndicator.BringToFront();
        }

        private void ShowDefaultTab()
        {
            SwitchTab(new ucSanPham(_productUseCase, _currentUser), btnProducts, "Sản phẩm & Quản lý Kho");
        }

        private void BtnToggleSidebar_Click(object sender, EventArgs e)
        {
            _isCollapsed = !_isCollapsed;

            if (_isCollapsed)
            {
                rootLayout.ColumnStyles[0].Width = 64;
                pnlSidebar.Width = 64;
                pnlSidebarHeader.Width = 64;
                pnlProfile.Width = 64;
                flowSidebar.Width = 64;
                btnToggleSidebar.Text = "▶";
                btnToggleSidebar.Location = new Point(20, 20);
                lblAppTitle.Visible = false;
                pnlProfile.Visible = false;

                // Strip text from buttons to show icon emojis only
                btnProducts.Text = "📦";
                btnProducts.Width = 64;
                btnProducts.TextOffset = new Point(5, 0);

                btnOrders.Text = "📝";
                btnOrders.Width = 64;
                btnOrders.TextOffset = new Point(5, 0);

                btnPOS.Text = "💳";
                btnPOS.Width = 64;
                btnPOS.TextOffset = new Point(5, 0);

                btnAccounts.Text = "👤";
                btnAccounts.Width = 64;
                btnAccounts.TextOffset = new Point(5, 0);

                btnLogout.Text = "🚪";
                btnLogout.Width = 64;
                btnLogout.TextOffset = new Point(5, 0);
            }
            else
            {
                rootLayout.ColumnStyles[0].Width = 230;
                pnlSidebar.Width = 230;
                pnlSidebarHeader.Width = 230;
                pnlProfile.Width = 230;
                flowSidebar.Width = 230;
                btnToggleSidebar.Text = "◀";
                btnToggleSidebar.Location = new Point(190, 20);
                lblAppTitle.Visible = true;
                pnlProfile.Visible = true;

                // Restore labels
                btnProducts.Text = "📦  Sản phẩm & Kho";
                btnProducts.Width = 230;
                btnProducts.TextOffset = new Point(10, 0);

                btnOrders.Text = "📝  Đơn đặt giữ hàng";
                btnOrders.Width = 230;
                btnOrders.TextOffset = new Point(10, 0);

                btnPOS.Text = "💳  Thanh toán POS";
                btnPOS.Width = 230;
                btnPOS.TextOffset = new Point(10, 0);

                btnAccounts.Text = "👤  Quản lý tài khoản";
                btnAccounts.Width = 230;
                btnAccounts.TextOffset = new Point(10, 0);

                btnLogout.Text = "🚪  Đăng xuất";
                btnLogout.Width = 230;
                btnLogout.TextOffset = new Point(10, 0);
            }

            // Move active indicator to correct position
            Guna2Button activeBtn = null;
            if (pnlContent.Controls.Count > 0)
            {
                var uc = pnlContent.Controls[0];
                if (uc is ucSanPham) activeBtn = btnProducts;
                else if (uc is ucDonHang) activeBtn = btnOrders;
                else if (uc is ucThanhToan) activeBtn = btnPOS;
                else if (uc is ucTaiKhoan) activeBtn = btnAccounts;
            }
            if (activeBtn != null)
            {
                pnlIndicator.Location = new Point(0, flowSidebar.Location.Y + activeBtn.Location.Y);
                pnlIndicator.Visible = true;
                pnlIndicator.BringToFront();
            }
        }

        private void BtnLogout_Click(object sender, EventArgs e)
        {
            var confirm = MessageBox.Show("Bạn có chắc chắn muốn đăng xuất?", "Đăng xuất", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm == DialogResult.Yes)
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }
    }
}
