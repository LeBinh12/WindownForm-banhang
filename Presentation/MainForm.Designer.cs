using System;
using System.Drawing;
using System.Windows.Forms;
using Guna.UI2.WinForms;
using QuanLyCuaHangTapHoa.Domain;
using QuanLyCuaHangTapHoa.Presentation.UserControls;

namespace QuanLyCuaHangTapHoa.Presentation
{
    partial class MainForm
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
            this.rootLayout = new System.Windows.Forms.TableLayoutPanel();
            this.pnlSidebar = new System.Windows.Forms.Panel();
            this.flowSidebar = new System.Windows.Forms.FlowLayoutPanel();
            this.pnlSidebarHeader = new System.Windows.Forms.Panel();
            this.lblAppTitle = new System.Windows.Forms.Label();
            this.btnToggleSidebar = new System.Windows.Forms.Button();
            this.pnlProfile = new System.Windows.Forms.Panel();
            this.lblUserGreeting = new System.Windows.Forms.Label();
            this.lblUserRole = new System.Windows.Forms.Label();
            this.btnProducts = new Guna.UI2.WinForms.Guna2Button();
            this.btnOrders = new Guna.UI2.WinForms.Guna2Button();
            this.btnPOS = new Guna.UI2.WinForms.Guna2Button();
            this.btnAccounts = new Guna.UI2.WinForms.Guna2Button();
            this.btnLogout = new Guna.UI2.WinForms.Guna2Button();
            this.pnlIndicator = new System.Windows.Forms.Panel();
            this.rightLayout = new System.Windows.Forms.TableLayoutPanel();
            this.pnlTopbar = new System.Windows.Forms.Panel();
            this.lblPageTitle = new System.Windows.Forms.Label();
            this.lblAvatar = new System.Windows.Forms.Label();
            this.lblBell = new System.Windows.Forms.Label();
            this.pnlContent = new System.Windows.Forms.Panel();

            // 
            // rootLayout
            // 
            this.rootLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rootLayout.ColumnCount = 2;
            this.rootLayout.RowCount = 1;
            this.rootLayout.Margin = new System.Windows.Forms.Padding(0);
            this.rootLayout.Padding = new System.Windows.Forms.Padding(0);
            this.rootLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 230F));
            this.rootLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.rootLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.Controls.Add(this.rootLayout);

            // 
            // pnlSidebar
            // 
            this.pnlSidebar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlSidebar.BackColor = ThemeHelper.SidebarBg;
            this.pnlSidebar.Margin = new System.Windows.Forms.Padding(0);
            this.rootLayout.Controls.Add(this.pnlSidebar, 0, 0);

            // 
            // flowSidebar
            // 
            this.flowSidebar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowSidebar.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowSidebar.WrapContents = false;
            this.flowSidebar.BackColor = System.Drawing.Color.Transparent;
            this.flowSidebar.Margin = new System.Windows.Forms.Padding(0);
            this.pnlSidebar.Controls.Add(this.flowSidebar);

            // 
            // pnlSidebarHeader
            // 
            this.pnlSidebarHeader.Width = 230;
            this.pnlSidebarHeader.Height = 65;
            this.pnlSidebarHeader.BackColor = System.Drawing.Color.Transparent;
            this.pnlSidebarHeader.Margin = new System.Windows.Forms.Padding(0);

            // 
            // lblAppTitle
            // 
            this.lblAppTitle.Text = "🏪 MART SYSTEM";
            this.lblAppTitle.ForeColor = System.Drawing.Color.White;
            this.lblAppTitle.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Bold);
            this.lblAppTitle.Location = new System.Drawing.Point(15, 18);
            this.lblAppTitle.Size = new System.Drawing.Size(160, 30);
            this.lblAppTitle.BackColor = System.Drawing.Color.Transparent;

            // 
            // btnToggleSidebar
            // 
            this.btnToggleSidebar.Text = "◀";
            this.btnToggleSidebar.Size = new System.Drawing.Size(25, 25);
            this.btnToggleSidebar.Location = new System.Drawing.Point(190, 20);
            this.btnToggleSidebar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnToggleSidebar.ForeColor = System.Drawing.Color.White;
            this.btnToggleSidebar.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.btnToggleSidebar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnToggleSidebar.BackColor = System.Drawing.Color.Transparent;
            this.btnToggleSidebar.FlatAppearance.BorderSize = 0;
            this.btnToggleSidebar.Click += new System.EventHandler(this.BtnToggleSidebar_Click);

            this.pnlSidebarHeader.Controls.Add(this.lblAppTitle);
            this.pnlSidebarHeader.Controls.Add(this.btnToggleSidebar);
            this.flowSidebar.Controls.Add(this.pnlSidebarHeader);

            // 
            // pnlProfile
            // 
            this.pnlProfile.Width = 230;
            this.pnlProfile.Height = 85;
            this.pnlProfile.BackColor = System.Drawing.Color.FromArgb(35, 50, 85);
            this.pnlProfile.Margin = new System.Windows.Forms.Padding(0);

            // 
            // lblUserGreeting
            // 
            this.lblUserGreeting.Text = "User";
            this.lblUserGreeting.ForeColor = System.Drawing.Color.White;
            this.lblUserGreeting.Font = ThemeHelper.FontBodyBold;
            this.lblUserGreeting.Location = new System.Drawing.Point(15, 18);
            this.lblUserGreeting.Width = 200;
            this.lblUserGreeting.AutoEllipsis = true;

            // 
            // lblUserRole
            // 
            this.lblUserRole.Text = "ROLE";
            this.lblUserRole.ForeColor = ThemeHelper.Success;
            this.lblUserRole.Font = new System.Drawing.Font("Segoe UI", 7.5F, System.Drawing.FontStyle.Bold);
            this.lblUserRole.Location = new System.Drawing.Point(15, 42);
            this.lblUserRole.AutoSize = true;

            this.pnlProfile.Controls.Add(this.lblUserGreeting);
            this.pnlProfile.Controls.Add(this.lblUserRole);
            this.flowSidebar.Controls.Add(this.pnlProfile);

            // 
            // btnProducts
            // 
            this.btnProducts.Text = "📦  Sản phẩm & Kho";
            this.btnProducts.Size = new System.Drawing.Size(230, 48);
            this.btnProducts.FillColor = ThemeHelper.SidebarBg;
            this.btnProducts.ForeColor = ThemeHelper.TextMuted;
            this.btnProducts.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnProducts.TextAlign = HorizontalAlignment.Left;
            this.btnProducts.TextOffset = new System.Drawing.Point(10, 0);
            this.btnProducts.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnProducts.BorderRadius = 0;
            this.btnProducts.Margin = new System.Windows.Forms.Padding(0);
            this.btnProducts.HoverState.FillColor = ThemeHelper.SidebarActiveBg;
            this.btnProducts.HoverState.ForeColor = System.Drawing.Color.White;

            // 
            // btnOrders
            // 
            this.btnOrders.Text = "📝  Đơn đặt giữ hàng";
            this.btnOrders.Size = new System.Drawing.Size(230, 48);
            this.btnOrders.FillColor = ThemeHelper.SidebarBg;
            this.btnOrders.ForeColor = ThemeHelper.TextMuted;
            this.btnOrders.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnOrders.TextAlign = HorizontalAlignment.Left;
            this.btnOrders.TextOffset = new System.Drawing.Point(10, 0);
            this.btnOrders.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnOrders.BorderRadius = 0;
            this.btnOrders.Margin = new System.Windows.Forms.Padding(0);
            this.btnOrders.HoverState.FillColor = ThemeHelper.SidebarActiveBg;
            this.btnOrders.HoverState.ForeColor = System.Drawing.Color.White;

            // 
            // btnPOS
            // 
            this.btnPOS.Text = "💳  Thanh toán POS";
            this.btnPOS.Size = new System.Drawing.Size(230, 48);
            this.btnPOS.FillColor = ThemeHelper.SidebarBg;
            this.btnPOS.ForeColor = ThemeHelper.TextMuted;
            this.btnPOS.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnPOS.TextAlign = HorizontalAlignment.Left;
            this.btnPOS.TextOffset = new System.Drawing.Point(10, 0);
            this.btnPOS.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPOS.BorderRadius = 0;
            this.btnPOS.Margin = new System.Windows.Forms.Padding(0);
            this.btnPOS.HoverState.FillColor = ThemeHelper.SidebarActiveBg;
            this.btnPOS.HoverState.ForeColor = System.Drawing.Color.White;

            // 
            // btnAccounts
            // 
            this.btnAccounts.Text = "👤  Quản lý tài khoản";
            this.btnAccounts.Size = new System.Drawing.Size(230, 48);
            this.btnAccounts.FillColor = ThemeHelper.SidebarBg;
            this.btnAccounts.ForeColor = ThemeHelper.TextMuted;
            this.btnAccounts.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnAccounts.TextAlign = HorizontalAlignment.Left;
            this.btnAccounts.TextOffset = new System.Drawing.Point(10, 0);
            this.btnAccounts.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAccounts.BorderRadius = 0;
            this.btnAccounts.Margin = new System.Windows.Forms.Padding(0);
            this.btnAccounts.HoverState.FillColor = ThemeHelper.SidebarActiveBg;
            this.btnAccounts.HoverState.ForeColor = System.Drawing.Color.White;

            // 
            // btnLogout
            // 
            this.btnLogout.Text = "🚪  Đăng xuất";
            this.btnLogout.Size = new System.Drawing.Size(230, 48);
            this.btnLogout.FillColor = ThemeHelper.SidebarBg;
            this.btnLogout.ForeColor = ThemeHelper.Danger;
            this.btnLogout.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnLogout.TextAlign = HorizontalAlignment.Left;
            this.btnLogout.TextOffset = new System.Drawing.Point(10, 0);
            this.btnLogout.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLogout.BorderRadius = 0;
            this.btnLogout.Margin = new System.Windows.Forms.Padding(0);
            this.btnLogout.HoverState.FillColor = ThemeHelper.SidebarActiveBg;
            this.btnLogout.HoverState.ForeColor = System.Drawing.Color.White;
            this.btnLogout.Click += new System.EventHandler(this.BtnLogout_Click);

            this.flowSidebar.Controls.Add(this.btnProducts);
            this.flowSidebar.Controls.Add(this.btnOrders);
            this.flowSidebar.Controls.Add(this.btnPOS);
            this.flowSidebar.Controls.Add(this.btnAccounts);
            this.flowSidebar.Controls.Add(this.btnLogout);

            // 
            // pnlIndicator
            // 
            this.pnlIndicator.Width = 4;
            this.pnlIndicator.Height = 48;
            this.pnlIndicator.BackColor = System.Drawing.Color.FromArgb(77, 142, 240);
            this.pnlIndicator.Location = new System.Drawing.Point(0, 0);
            this.pnlIndicator.Visible = false;
            this.pnlSidebar.Controls.Add(this.pnlIndicator);

            // 
            // rightLayout
            // 
            this.rightLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rightLayout.ColumnCount = 1;
            this.rightLayout.RowCount = 2;
            this.rightLayout.Margin = new System.Windows.Forms.Padding(0);
            this.rightLayout.Padding = new System.Windows.Forms.Padding(0);
            this.rightLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.rightLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.rightLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.rootLayout.Controls.Add(this.rightLayout, 1, 0);

            // 
            // pnlTopbar
            // 
            this.pnlTopbar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlTopbar.BackColor = System.Drawing.Color.White;
            this.pnlTopbar.Padding = new System.Windows.Forms.Padding(15, 0, 15, 0);
            this.pnlTopbar.Margin = new System.Windows.Forms.Padding(0);
            this.rightLayout.Controls.Add(this.pnlTopbar, 0, 0);

            // 
            // lblPageTitle
            // 
            this.lblPageTitle.Text = "Trang chủ";
            this.lblPageTitle.Font = ThemeHelper.FontH2;
            this.lblPageTitle.ForeColor = ThemeHelper.Primary;
            this.lblPageTitle.Location = new System.Drawing.Point(15, 18);
            this.lblPageTitle.AutoSize = true;
            this.lblPageTitle.Anchor = System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Top;

            // 
            // lblAvatar
            // 
            this.lblAvatar.Text = "US";
            this.lblAvatar.Font = ThemeHelper.FontBodyBold;
            this.lblAvatar.ForeColor = ThemeHelper.Primary;
            this.lblAvatar.BackColor = ThemeHelper.PrimaryLight;
            this.lblAvatar.Size = new System.Drawing.Size(34, 34);
            this.lblAvatar.Location = new System.Drawing.Point(840, 13);
            this.lblAvatar.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblAvatar.Anchor = System.Windows.Forms.AnchorStyles.Right | System.Windows.Forms.AnchorStyles.Top;

            // 
            // lblBell
            // 
            this.lblBell.Text = "🔔";
            this.lblBell.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.lblBell.ForeColor = ThemeHelper.TextSecondary;
            this.lblBell.Size = new System.Drawing.Size(30, 30);
            this.lblBell.Location = new System.Drawing.Point(800, 15);
            this.lblBell.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblBell.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblBell.Anchor = System.Windows.Forms.AnchorStyles.Right | System.Windows.Forms.AnchorStyles.Top;

            this.pnlTopbar.Controls.Add(this.lblPageTitle);
            this.pnlTopbar.Controls.Add(this.lblAvatar);
            this.pnlTopbar.Controls.Add(this.lblBell);

            // 
            // pnlContent
            // 
            this.pnlContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlContent.BackColor = ThemeHelper.BackgroundApp;
            this.pnlContent.Padding = new System.Windows.Forms.Padding(0);
            this.pnlContent.Margin = new System.Windows.Forms.Padding(0);
            this.rightLayout.Controls.Add(this.pnlContent, 0, 1);

            // FORM properties
            this.Size = new System.Drawing.Size(1150, 720);
            this.MinimumSize = new System.Drawing.Size(1100, 650);
            this.Text = "Hệ thống Quản Lý Cửa Hàng Tạp Hóa";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
        }

        #endregion

        // UI Panels & Layouts
        private System.Windows.Forms.TableLayoutPanel rootLayout;
        private System.Windows.Forms.TableLayoutPanel rightLayout;
        private System.Windows.Forms.Panel pnlSidebar;
        private System.Windows.Forms.Panel pnlTopbar;
        private System.Windows.Forms.Panel pnlContent;
        private System.Windows.Forms.Panel pnlProfile;
        private System.Windows.Forms.Panel pnlIndicator;

        // Header controls
        private System.Windows.Forms.Label lblAppTitle;
        private System.Windows.Forms.Label lblPageTitle;
        private System.Windows.Forms.Label lblUserGreeting;
        private System.Windows.Forms.Label lblUserRole;
        private System.Windows.Forms.Label lblBell;
        private System.Windows.Forms.Label lblAvatar;

        // Menu FlowLayout & Buttons
        private System.Windows.Forms.FlowLayoutPanel flowSidebar;
        private System.Windows.Forms.Panel pnlSidebarHeader;
        private Guna2Button btnProducts;
        private Guna2Button btnOrders;
        private Guna2Button btnPOS;
        private Guna2Button btnAccounts;
        private Guna2Button btnLogout;
        private System.Windows.Forms.Button btnToggleSidebar;
    }
}
