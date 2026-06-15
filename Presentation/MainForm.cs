using System;
using System.Drawing;
using System.Windows.Forms;
using Guna.UI2.WinForms;
using QuanLyCuaHangTapHoa.Application.Interfaces;
using QuanLyCuaHangTapHoa.Domain;
using QuanLyCuaHangTapHoa.Presentation.UserControls;

namespace QuanLyCuaHangTapHoa.Presentation
{
    public class MainForm : Form
    {
        private readonly TaiKhoan _currentUser;
        private readonly IProductUseCase _productUseCase;
        private readonly IOrderUseCase _orderUseCase;
        private readonly IInvoiceUseCase _invoiceUseCase;
        private readonly IAccountUseCase _accountUseCase;

        // Collapsed state
        private bool _isCollapsed = false;

        // UI Panels & Layouts
        private TableLayoutPanel rootLayout;
        private TableLayoutPanel rightLayout;
        private Panel pnlSidebar;
        private Panel pnlTopbar;
        private Panel pnlContent;
        private Panel pnlProfile;
        private Panel pnlIndicator; // Moves to active menu button

        // Header controls
        private Label lblAppTitle;
        private Label lblPageTitle;
        private Label lblUserGreeting;
        private Label lblUserRole;
        private Label lblBell;
        private Label lblAvatar;

        // Menu FlowLayout & Buttons
        private FlowLayoutPanel flowSidebar;
        private Panel pnlSidebarHeader;
        private Guna2Button btnProducts;
        private Guna2Button btnOrders;
        private Guna2Button btnPOS;
        private Guna2Button btnAccounts;
        private Guna2Button btnLogout;
        private Button btnToggleSidebar;

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
            ShowDefaultTab();
        }

        private void InitializeComponent()
        {
            this.Size = new Size(1150, 720);
            this.MinimumSize = new Size(1100, 650);
            this.Text = "Hệ thống Quản Lý Cửa Hàng Tạp Hóa";
            this.StartPosition = FormStartPosition.CenterScreen;
            ThemeHelper.StyleForm(this);

            // Master TableLayoutPanel (2 Columns x 1 Row)
            rootLayout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 2,
                RowCount = 1,
                Margin = new Padding(0),
                Padding = new Padding(0)
            };
            rootLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 230F)); // Col 0: Sidebar
            rootLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));  // Col 1: Right Side
            rootLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            this.Controls.Add(rootLayout);

            // 1. SIDEBAR PANEL (Col 0)
            pnlSidebar = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = ThemeHelper.SidebarBg,
                Margin = new Padding(0)
            };
            rootLayout.Controls.Add(pnlSidebar, 0, 0);

            // FlowLayout for vertical menu list
            flowSidebar = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.TopDown,
                WrapContents = false,
                BackColor = Color.Transparent,
                Margin = new Padding(0)
            };
            pnlSidebar.Controls.Add(flowSidebar);

            // Sidebar Header (App Title & Toggle Button)
            pnlSidebarHeader = new Panel
            {
                Width = 230,
                Height = 65,
                BackColor = Color.Transparent,
                Margin = new Padding(0)
            };

            lblAppTitle = new Label
            {
                Text = "🏪 MART SYSTEM",
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 13F, FontStyle.Bold),
                Location = new Point(15, 18),
                Size = new Size(160, 30),
                BackColor = Color.Transparent
            };

            btnToggleSidebar = new Button
            {
                Text = "◀",
                Size = new Size(25, 25),
                Location = new Point(190, 20),
                FlatStyle = FlatStyle.Flat,
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 8F),
                Cursor = Cursors.Hand,
                BackColor = Color.Transparent
            };
            btnToggleSidebar.FlatAppearance.BorderSize = 0;
            btnToggleSidebar.Click += BtnToggleSidebar_Click;

            pnlSidebarHeader.Controls.AddRange(new Control[] { lblAppTitle, btnToggleSidebar });
            flowSidebar.Controls.Add(pnlSidebarHeader);

            // Profile info block
            pnlProfile = new Panel
            {
                Width = 230,
                Height = 85,
                BackColor = Color.FromArgb(35, 50, 85),
                Margin = new Padding(0)
            };

            lblUserGreeting = new Label
            {
                Text = _currentUser.NguoiDung.HoTen,
                ForeColor = Color.White,
                Font = ThemeHelper.FontBodyBold,
                Location = new Point(15, 18),
                Width = 200,
                AutoEllipsis = true
            };

            string roleText = "Khách Hàng";
            if (_currentUser.NguoiDung is Admin) roleText = "Quản trị viên";
            else if (_currentUser.NguoiDung is NhanVien) roleText = "Nhân viên";

            lblUserRole = new Label
            {
                Text = roleText.ToUpper(),
                ForeColor = ThemeHelper.Success,
                Font = new Font("Segoe UI", 7.5F, FontStyle.Bold),
                Location = new Point(15, 42),
                AutoSize = true
            };
            pnlProfile.Controls.AddRange(new Control[] { lblUserGreeting, lblUserRole });
            flowSidebar.Controls.Add(pnlProfile);

            // Menu Buttons
            btnProducts = CreateMenuButton("📦  Sản phẩm & Kho");
            btnProducts.Click += (s, e) => SwitchTab(new ucSanPham(_productUseCase, _currentUser), btnProducts, "Sản phẩm & Quản lý Kho");

            btnOrders = CreateMenuButton("📝  Đơn đặt giữ hàng");
            btnOrders.Click += (s, e) => SwitchTab(new ucDonHang(_orderUseCase, _productUseCase, _currentUser), btnOrders, "Yêu cầu Đặt giữ hàng");

            btnPOS = CreateMenuButton("💳  Thanh toán POS");
            btnPOS.Click += (s, e) => SwitchTab(new ucThanhToan(_invoiceUseCase, _orderUseCase, _productUseCase, _currentUser), btnPOS, "Điểm bán lẻ POS & Đổi trả");

            btnAccounts = CreateMenuButton("👤  Quản lý tài khoản");
            btnAccounts.Click += (s, e) => SwitchTab(new ucTaiKhoan(_accountUseCase, _currentUser), btnAccounts, "Quản trị Tài khoản");

            btnLogout = CreateMenuButton("🚪  Đăng xuất");
            btnLogout.ForeColor = ThemeHelper.Danger;
            btnLogout.Click += BtnLogout_Click;

            flowSidebar.Controls.AddRange(new Control[] { btnProducts, btnOrders, btnPOS, btnAccounts, btnLogout });

            // Active menu left border indicator
            pnlIndicator = new Panel
            {
                Width = 4,
                Height = 48,
                BackColor = Color.FromArgb(77, 142, 240),
                Location = new Point(0, 0),
                Visible = false
            };
            pnlSidebar.Controls.Add(pnlIndicator); // Placed in sidebar over flow list z-order

            // 2. RIGHT LAYOUT (Col 1)
            rightLayout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 1,
                RowCount = 2,
                Margin = new Padding(0),
                Padding = new Padding(0)
            };
            rightLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            rightLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 60F));      // Row 0: Topbar
            rightLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));      // Row 1: Content
            rootLayout.Controls.Add(rightLayout, 1, 0);

            // Topbar Panel (Row 0 of Right Side)
            pnlTopbar = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.White,
                Padding = new Padding(15, 0, 15, 0),
                Margin = new Padding(0)
            };
            pnlTopbar.Paint += (s, e) =>
            {
                using (var pen = new Pen(ThemeHelper.BorderLight, 2))
                {
                    e.Graphics.DrawLine(pen, 0, pnlTopbar.Height - 1, pnlTopbar.Width, pnlTopbar.Height - 1);
                }
            };
            rightLayout.Controls.Add(pnlTopbar, 0, 0);

            lblPageTitle = new Label
            {
                Text = "Trang chủ",
                Font = ThemeHelper.FontH2,
                ForeColor = ThemeHelper.Primary,
                Location = new Point(15, 18),
                AutoSize = true,
                Anchor = AnchorStyles.Left | AnchorStyles.Top
            };

            lblAvatar = new Label
            {
                Text = _currentUser.TenDangNhap.Substring(0, Math.Min(2, _currentUser.TenDangNhap.Length)).ToUpper(),
                Font = ThemeHelper.FontBodyBold,
                ForeColor = ThemeHelper.Primary,
                BackColor = ThemeHelper.PrimaryLight,
                Size = new Size(34, 34),
                Location = new Point(840, 13),
                TextAlign = ContentAlignment.MiddleCenter,
                Anchor = AnchorStyles.Right | AnchorStyles.Top
            };
            ThemeHelper.RoundControl(lblAvatar, 17);

            lblBell = new Label
            {
                Text = "🔔",
                Font = new Font("Segoe UI", 12F),
                ForeColor = ThemeHelper.TextSecondary,
                Size = new Size(30, 30),
                Location = new Point(800, 15),
                Cursor = Cursors.Hand,
                TextAlign = ContentAlignment.MiddleCenter,
                Anchor = AnchorStyles.Right | AnchorStyles.Top
            };

            pnlTopbar.Controls.AddRange(new Control[] { lblPageTitle, lblAvatar, lblBell });

            // Initialize Avatar and Bell Positions relative to right side
            pnlTopbar.SizeChanged += (s, e) =>
            {
                lblAvatar.Left = pnlTopbar.Width - 60;
                lblBell.Left = pnlTopbar.Width - 100;
            };

            // Content Panel (Row 1 of Right Side)
            pnlContent = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = ThemeHelper.BackgroundApp,
                Padding = new Padding(0), // Clean Padding to maximize UserControl workspace
                Margin = new Padding(0)
            };
            rightLayout.Controls.Add(pnlContent, 0, 1);

            ConfigureMenuAccess();
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

        private void SwitchTab(UserControl uc, Guna2Button activeMenuButton, string title)
        {
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
