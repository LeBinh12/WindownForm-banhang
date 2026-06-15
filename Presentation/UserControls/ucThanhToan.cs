using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Guna.UI2.WinForms;
using QuanLyCuaHangTapHoa.Application.Interfaces;
using QuanLyCuaHangTapHoa.Domain;
using QuanLyCuaHangTapHoa.Presentation.Modals;

namespace QuanLyCuaHangTapHoa.Presentation.UserControls
{
    public class ucThanhToan : UserControl
    {
        private readonly IInvoiceUseCase _invoiceUseCase;
        private readonly IOrderUseCase _orderUseCase;
        private readonly IProductUseCase _productUseCase;
        private readonly TaiKhoan _currentUser;

        // POS cart
        private List<(SanPham product, int quantity)> _posCart = new List<(SanPham, int)>();

        // Controls
        private TableLayoutPanel tblRoot;
        private TableLayoutPanel tblHeader;
        private Guna2TabControl tabThanhToan;
        private TabPage tabPos;
        private TabPage tabReturn;

        // POS Elements
        private TableLayoutPanel posSplitLayout;
        private Guna2Panel cardPosLeft;
        private Guna2Panel cardPosRight;
        
        private Guna2ComboBox cbPosProducts;
        private NumericUpDown numPosQty;
        private Guna2Button btnPosAdd;
        private DataGridView dgvPosCart;
        private Label lblPosTotal;
        private Guna2Button btnPosCheckout;
        private Guna2Button btnPosClear;

        // Pay Reserved Order elements
        private Guna2ComboBox cbReservedOrders;
        private Label lblReservedDetails;
        private Guna2Button btnPayReserved;

        // Return elements
        private TableLayoutPanel returnSplitLayout;
        private Guna2Panel cardReturnSearch;
        private Guna2Panel cardReturnDetails;
        
        private Guna2TextBox txtSearchInvoiceId;
        private Guna2Button btnSearchInvoice;
        private Label lblInvoiceDate;
        private Label lblInvoiceTotal;
        private DataGridView dgvInvoiceDetails;

        // Invoice History elements
        private TabPage tabHistory;
        private TableLayoutPanel historySplitLayout;
        private Guna2Panel cardHistoryFilter;
        private Guna2Panel cardHistoryGrid;
        private Guna2TextBox txtSearchHistory;
        private Guna2Button btnSearchHistory;
        private DataGridView dgvHistoryInvoices;

        public ucThanhToan(
            IInvoiceUseCase invoiceUseCase,
            IOrderUseCase orderUseCase,
            IProductUseCase productUseCase,
            TaiKhoan currentUser)
        {
            _invoiceUseCase = invoiceUseCase;
            _orderUseCase = orderUseCase;
            _productUseCase = productUseCase;
            _currentUser = currentUser;

            InitializeComponent();
            LoadPOSProducts();
            LoadApprovedOrders();
        }

        private void InitializeComponent()
        {
            this.Dock = DockStyle.Fill;
            this.BackColor = ThemeHelper.BackgroundApp;

            // 1. Root TableLayoutPanel (1 Column x 2 Rows, Padding = 20)
            tblRoot = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 1,
                RowCount = 2,
                Padding = new Padding(20),
                BackColor = Color.Transparent
            };
            tblRoot.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tblRoot.RowStyles.Add(new RowStyle(SizeType.AutoSize)); // Row 0: Header
            tblRoot.RowStyles.Add(new RowStyle(SizeType.Percent, 100F)); // Row 1: Content Tabs
            this.Controls.Add(tblRoot);

            // ==================== ROW 0: HEADER (TableLayoutPanel 2 Cols) ====================
            tblHeader = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 2,
                RowCount = 1,
                Margin = new Padding(0, 0, 0, 12),
                Padding = new Padding(0)
            };
            tblHeader.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F)); // Col 0: Title
            tblHeader.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));    // Col 1: Empty
            tblHeader.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tblRoot.Controls.Add(tblHeader, 0, 0);

            var lblTitle = new Label
            {
                Text = "QUẢN LÝ BÁN HÀNG & ĐỔI TRẢ",
                Font = ThemeHelper.FontH2,
                ForeColor = ThemeHelper.Primary,
                Anchor = AnchorStyles.Left,
                AutoSize = true,
                TextAlign = ContentAlignment.MiddleLeft
            };
            tblHeader.Controls.Add(lblTitle, 0, 0);

            // ==================== ROW 1: TABS ====================
            tabThanhToan = new Guna2TabControl
            {
                Dock = DockStyle.Fill,
                TabButtonHoverState = { FillColor = ThemeHelper.SidebarActiveBg, InnerColor = ThemeHelper.Primary, ForeColor = Color.White },
                TabButtonSelectedState = { FillColor = ThemeHelper.SidebarBg, InnerColor = Color.FromArgb(77, 142, 240), ForeColor = Color.White },
                TabButtonIdleState = { FillColor = Color.FromArgb(240, 243, 250), ForeColor = ThemeHelper.TextSecondary },
                TabButtonSize = new Size(240, 40),
                Alignment = TabAlignment.Top
            };
            tblRoot.Controls.Add(tabThanhToan, 0, 1);

            tabPos = new TabPage { Text = "💳  Bán lẻ & Nhận Đơn đặt giữ", BackColor = ThemeHelper.BackgroundApp };
            tabReturn = new TabPage { Text = "🔄  Tiếp nhận đổi trả hàng lỗi", BackColor = ThemeHelper.BackgroundApp };
            tabHistory = new TabPage { Text = "📜  Lịch sử hóa đơn đã thanh toán", BackColor = ThemeHelper.BackgroundApp };
            tabThanhToan.TabPages.AddRange(new TabPage[] { tabPos, tabReturn, tabHistory });

            // -------------------- TAB 1: POS --------------------
            posSplitLayout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 2,
                RowCount = 1,
                Padding = new Padding(8)
            };
            posSplitLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 60F)); // Retail POS
            posSplitLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 40F)); // Reserved Orders
            posSplitLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tabPos.Controls.Add(posSplitLayout);

            // Card POS Left
            cardPosLeft = new Guna2Panel { Dock = DockStyle.Fill, BorderRadius = 12, FillColor = Color.White, Padding = new Padding(16) };
            cardPosLeft.ShadowDecoration.Enabled = true;
            posSplitLayout.Controls.Add(cardPosLeft, 0, 0);

            var tblPosLeftLayout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 1,
                RowCount = 4,
                BackColor = Color.Transparent
            };
            tblPosLeftLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tblPosLeftLayout.RowStyles.Add(new RowStyle(SizeType.AutoSize)); // Row 0: Title
            tblPosLeftLayout.RowStyles.Add(new RowStyle(SizeType.AutoSize)); // Row 1: Inputs
            tblPosLeftLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F)); // Row 2: Grid Cart
            tblPosLeftLayout.RowStyles.Add(new RowStyle(SizeType.AutoSize)); // Row 3: Bottom Actions
            cardPosLeft.Controls.Add(tblPosLeftLayout);

            var lblPosTitle = new Label { Text = "BÁN LẺ TRỰC TIẾP TẠI QUẦY (POS)", Font = ThemeHelper.FontSubheading, ForeColor = ThemeHelper.Primary, Dock = DockStyle.Top, Height = 25, BackColor = Color.Transparent };
            tblPosLeftLayout.Controls.Add(lblPosTitle, 0, 0);

            // POS Inputs Horizontal TableLayout
            var tblPosFilter = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 4,
                RowCount = 1,
                Margin = new Padding(0, 8, 0, 12),
                Padding = new Padding(0),
                Height = 44
            };
            tblPosFilter.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 180F)); // Col 0: Product Select
            tblPosFilter.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 80F));  // Col 1: Qty Input
            tblPosFilter.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));       // Col 2: Add Button
            tblPosFilter.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));  // Col 3: Spacer
            tblPosFilter.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tblPosLeftLayout.Controls.Add(tblPosFilter, 0, 1);

            cbPosProducts = new Guna2ComboBox { Dock = DockStyle.Fill, Margin = new Padding(0, 0, 12, 0) };
            ThemeHelper.StyleComboBox(cbPosProducts);
            tblPosFilter.Controls.Add(cbPosProducts, 0, 0);

            numPosQty = new NumericUpDown { Dock = DockStyle.Fill, Minimum = 1, Maximum = 100, Font = ThemeHelper.FontBody, Margin = new Padding(0, 4, 12, 0) };
            tblPosFilter.Controls.Add(numPosQty, 1, 0);

            btnPosAdd = new Guna2Button
            {
                Text = "Thêm hàng",
                Size = new Size(110, 36),
                BorderRadius = 18,
                FillColor = ThemeHelper.Primary,
                Font = ThemeHelper.FontCaptionBold,
                ForeColor = Color.White,
                Cursor = Cursors.Hand,
                Margin = new Padding(0, 0, 0, 0)
            };
            btnPosAdd.Click += BtnPosAdd_Click;
            tblPosFilter.Controls.Add(btnPosAdd, 2, 0);

            // POS Cart Grid
            dgvPosCart = new DataGridView
            {
                Dock = DockStyle.Fill,
                AutoGenerateColumns = false,
                RowHeadersVisible = false,
                AllowUserToResizeRows = false,
                AllowUserToAddRows = false
            };
            ThemeHelper.StyleFlatDataGrid(dgvPosCart);
            dgvPosCart.ColumnHeadersHeight = 44;
            dgvPosCart.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9.75f, FontStyle.Bold);
            dgvPosCart.ColumnHeadersDefaultCellStyle.WrapMode = DataGridViewTriState.False;
            dgvPosCart.EnableHeadersVisualStyles = false;
            dgvPosCart.DefaultCellStyle.Font = new Font("Segoe UI", 9f);
            dgvPosCart.DefaultCellStyle.WrapMode = DataGridViewTriState.False;

            dgvPosCart.DataError += (s, e) => { e.ThrowException = false; };
            dgvPosCart.CellClick += DgvPosCart_CellClick;
            tblPosLeftLayout.Controls.Add(dgvPosCart, 0, 2);

            dgvPosCart.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Mã SP", DataPropertyName = "MaSP", Width = 90, ReadOnly = true });
            dgvPosCart.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Sản phẩm", DataPropertyName = "TenSP", AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill, FillWeight = 40, ReadOnly = true });
            
            var colPosQty = new DataGridViewTextBoxColumn
            {
                HeaderText = "SL",
                DataPropertyName = "SoLuong",
                Width = 50,
                ReadOnly = true
            };
            colPosQty.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvPosCart.Columns.Add(colPosQty);

            var colPosPrice = new DataGridViewTextBoxColumn
            {
                HeaderText = "Đơn giá",
                DataPropertyName = "DonGiaText",
                Width = 95,
                ReadOnly = true
            };
            colPosPrice.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvPosCart.Columns.Add(colPosPrice);

            var colPosTotalVal = new DataGridViewTextBoxColumn
            {
                HeaderText = "Thành tiền",
                DataPropertyName = "ThanhTienText",
                Width = 95,
                ReadOnly = true
            };
            colPosTotalVal.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvPosCart.Columns.Add(colPosTotalVal);
            
            var colPosRemove = new DataGridViewButtonColumn
            {
                Name = "colPosRemove",
                HeaderText = "Xóa",
                Text = "Xóa",
                UseColumnTextForButtonValue = true,
                Width = 60,
                FlatStyle = FlatStyle.Flat,
                Resizable = DataGridViewTriState.False
            };
            colPosRemove.DefaultCellStyle.Font = new Font("Segoe UI", 9f);
            dgvPosCart.Columns.Add(colPosRemove);

            // Bottom Panel TableLayout
            Panel pnlPosBottom = new Panel { Dock = DockStyle.Fill, Height = 95, BackColor = Color.Transparent, Margin = new Padding(0, 8, 0, 0) };
            lblPosTotal = new Label { Text = "TỔNG THANH TOÁN: 0 đ", Font = ThemeHelper.FontH2, ForeColor = ThemeHelper.Danger, Location = new Point(0, 10), AutoSize = true };
            
            btnPosCheckout = new Guna2Button
            {
                Text = "Thanh toán",
                Location = new Point(0, 45),
                Size = new Size(130, 38),
                BorderRadius = 19,
                FillColor = ThemeHelper.Success,
                Font = ThemeHelper.FontBodyBold,
                ForeColor = Color.White,
                Cursor = Cursors.Hand
            };
            btnPosCheckout.Click += BtnPosCheckout_Click;

            btnPosClear = new Guna2Button
            {
                Text = "Xóa giỏ",
                Location = new Point(140, 45),
                Size = new Size(100, 38),
                BorderRadius = 19,
                FillColor = ThemeHelper.BorderLight,
                HoverState = { FillColor = ThemeHelper.Border },
                Font = ThemeHelper.FontBodyBold,
                ForeColor = ThemeHelper.TextSecondary,
                Cursor = Cursors.Hand
            };
            btnPosClear.Click += BtnPosClear_Click;
            pnlPosBottom.Controls.AddRange(new Control[] { lblPosTotal, btnPosCheckout, btnPosClear });
            tblPosLeftLayout.Controls.Add(pnlPosBottom, 0, 3);

            // Card POS Right (Pay Reserved Orders)
            cardPosRight = new Guna2Panel { Dock = DockStyle.Fill, BorderRadius = 12, FillColor = Color.White, Padding = new Padding(20), Margin = new Padding(12, 0, 0, 0) };
            cardPosRight.ShadowDecoration.Enabled = true;
            posSplitLayout.Controls.Add(cardPosRight, 1, 0);

            var tblPosRightLayout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 1,
                RowCount = 5,
                BackColor = Color.Transparent
            };
            tblPosRightLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tblPosRightLayout.RowStyles.Add(new RowStyle(SizeType.AutoSize)); // Title
            tblPosRightLayout.RowStyles.Add(new RowStyle(SizeType.AutoSize)); // Dropdown Select
            tblPosRightLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F)); // Detail Box
            tblPosRightLayout.RowStyles.Add(new RowStyle(SizeType.AutoSize)); // Button Pay
            cardPosRight.Controls.Add(tblPosRightLayout);

            var lblReservedTitle = new Label { Text = "THANH TOÁN ĐƠN ĐÃ DUYỆT", Font = ThemeHelper.FontSubheading, ForeColor = ThemeHelper.Primary, AutoSize = true, BackColor = Color.Transparent, Margin = new Padding(0, 0, 0, 12) };
            tblPosRightLayout.Controls.Add(lblReservedTitle, 0, 0);

            var pnlOrderSelect = new Panel { Dock = DockStyle.Fill, Height = 65, Margin = new Padding(0, 0, 0, 12) };
            var lblSelectOrder = new Label { Text = "Chọn mã đơn đặt hàng:", Font = ThemeHelper.FontCaptionBold, ForeColor = ThemeHelper.TextSecondary, Location = new Point(0, 0), AutoSize = true, BackColor = Color.Transparent };
            cbReservedOrders = new Guna2ComboBox { Location = new Point(0, 20), Width = 310, Height = 36 };
            ThemeHelper.StyleComboBox(cbReservedOrders);
            cbReservedOrders.SelectedIndexChanged += CbReservedOrders_SelectedIndexChanged;
            pnlOrderSelect.Controls.AddRange(new Control[] { lblSelectOrder, cbReservedOrders });
            tblPosRightLayout.Controls.Add(pnlOrderSelect, 0, 1);

            lblReservedDetails = new Label
            {
                Dock = DockStyle.Fill,
                Text = "Chi tiết đơn hàng:\nChưa chọn đơn.",
                BackColor = ThemeHelper.BackgroundApp,
                Font = ThemeHelper.FontBody,
                ForeColor = ThemeHelper.Text,
                Padding = new Padding(12),
                Margin = new Padding(0, 0, 0, 16)
            };
            ThemeHelper.RoundControl(lblReservedDetails, 8);
            tblPosRightLayout.Controls.Add(lblReservedDetails, 0, 2);

            btnPayReserved = new Guna2Button
            {
                Text = "Xác nhận & Xuất HĐ",
                Dock = DockStyle.Fill,
                Height = 42,
                BorderRadius = 21,
                FillColor = ThemeHelper.Success,
                Font = ThemeHelper.FontBodyBold,
                ForeColor = Color.White,
                Cursor = Cursors.Hand,
                Margin = new Padding(0)
            };
            btnPayReserved.Click += BtnPayReserved_Click;
            tblPosRightLayout.Controls.Add(btnPayReserved, 0, 3);

            // -------------------- TAB 2: RETURNS --------------------
            returnSplitLayout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 1,
                RowCount = 2,
                Padding = new Padding(8)
            };
            returnSplitLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            returnSplitLayout.RowStyles.Add(new RowStyle(SizeType.AutoSize));     // Row 0: Invoice Filter
            returnSplitLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F)); // Row 1: Invoice Details Grid
            tabReturn.Controls.Add(returnSplitLayout);

            // Search Header Panel
            cardReturnSearch = new Guna2Panel { Dock = DockStyle.Fill, BorderRadius = 12, FillColor = Color.White, Padding = new Padding(16), Margin = new Padding(0, 0, 0, 16) };
            cardReturnSearch.ShadowDecoration.Enabled = true;
            returnSplitLayout.Controls.Add(cardReturnSearch, 0, 0);

            var tblReturnFilter = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 5,
                RowCount = 1,
                Height = 65,
                BackColor = Color.Transparent
            };
            tblReturnFilter.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 220F)); // Col 0: Inv input
            tblReturnFilter.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));       // Col 1: Search Btn
            tblReturnFilter.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));       // Col 2: Date Info
            tblReturnFilter.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));       // Col 3: Total Info
            tblReturnFilter.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));  // Col 4: Spacer
            tblReturnFilter.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            cardReturnSearch.Controls.Add(tblReturnFilter);

            txtSearchInvoiceId = new Guna2TextBox { Dock = DockStyle.Fill, BorderRadius = 6, BorderColor = ThemeHelper.Border, Font = ThemeHelper.FontBody, ForeColor = ThemeHelper.Text, PlaceholderText = "Nhập mã hóa đơn...", Margin = new Padding(0, 4, 12, 0) };
            tblReturnFilter.Controls.Add(txtSearchInvoiceId, 0, 0);

            btnSearchInvoice = new Guna2Button
            {
                Text = "Tìm hóa đơn",
                Size = new Size(130, 36),
                BorderRadius = 18,
                FillColor = ThemeHelper.Primary,
                Font = ThemeHelper.FontCaptionBold,
                ForeColor = Color.White,
                Cursor = Cursors.Hand,
                Margin = new Padding(0, 0, 16, 0)
            };
            btnSearchInvoice.Click += BtnSearchInvoice_Click;
            tblReturnFilter.Controls.Add(btnSearchInvoice, 1, 0);

            lblInvoiceDate = new Label { Text = "Ngày lập: ---", Font = ThemeHelper.FontBodyBold, ForeColor = ThemeHelper.TextSecondary, Anchor = AnchorStyles.Left, AutoSize = true, Margin = new Padding(0, 0, 20, 0) };
            tblReturnFilter.Controls.Add(lblInvoiceDate, 2, 0);

            lblInvoiceTotal = new Label { Text = "Tổng tiền: ---", Font = ThemeHelper.FontBodyBold, ForeColor = ThemeHelper.Danger, Anchor = AnchorStyles.Left, AutoSize = true };
            tblReturnFilter.Controls.Add(lblInvoiceTotal, 3, 0);

            // Items Grid Panel
            cardReturnDetails = new Guna2Panel { Dock = DockStyle.Fill, BorderRadius = 12, FillColor = Color.White, Padding = new Padding(16) };
            cardReturnDetails.ShadowDecoration.Enabled = true;
            returnSplitLayout.Controls.Add(cardReturnDetails, 0, 1);

            dgvInvoiceDetails = new DataGridView
            {
                Dock = DockStyle.Fill,
                AutoGenerateColumns = false,
                RowHeadersVisible = false,
                AllowUserToResizeRows = false,
                AllowUserToAddRows = false
            };
            ThemeHelper.StyleFlatDataGrid(dgvInvoiceDetails);
            dgvInvoiceDetails.ColumnHeadersHeight = 44;
            dgvInvoiceDetails.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9.75f, FontStyle.Bold);
            dgvInvoiceDetails.ColumnHeadersDefaultCellStyle.WrapMode = DataGridViewTriState.False;
            dgvInvoiceDetails.EnableHeadersVisualStyles = false;
            dgvInvoiceDetails.DefaultCellStyle.Font = new Font("Segoe UI", 9f);
            dgvInvoiceDetails.DefaultCellStyle.WrapMode = DataGridViewTriState.False;

            dgvInvoiceDetails.DataError += (s, e) => { e.ThrowException = false; };
            dgvInvoiceDetails.CellClick += DgvInvoiceDetails_CellClick;
            cardReturnDetails.Controls.Add(dgvInvoiceDetails);

            dgvInvoiceDetails.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Mã SP", DataPropertyName = "MaSP", Width = 100, ReadOnly = true });
            dgvInvoiceDetails.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Tên Sản Phẩm", DataPropertyName = "SanPhamName", AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill, FillWeight = 40, ReadOnly = true });
            
            var colInvoiceQty = new DataGridViewTextBoxColumn
            {
                HeaderText = "SL Đã Mua",
                DataPropertyName = "SoLuong",
                Width = 100,
                ReadOnly = true
            };
            colInvoiceQty.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvInvoiceDetails.Columns.Add(colInvoiceQty);

            var colInvoicePrice = new DataGridViewTextBoxColumn
            {
                HeaderText = "Đơn Giá",
                DataPropertyName = "DonGiaText",
                Width = 110,
                ReadOnly = true
            };
            colInvoicePrice.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvInvoiceDetails.Columns.Add(colInvoicePrice);

            var colInvoiceTotalVal = new DataGridViewTextBoxColumn
            {
                HeaderText = "Thành Tiền",
                DataPropertyName = "ThanhTienText",
                Width = 110,
                ReadOnly = true
            };
            colInvoiceTotalVal.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvInvoiceDetails.Columns.Add(colInvoiceTotalVal);

            var colReturn = new DataGridViewButtonColumn
            {
                Name = "colReturn",
                HeaderText = "Trả Lỗi",
                Text = "Trả hàng",
                UseColumnTextForButtonValue = true,
                Width = 90,
                FlatStyle = FlatStyle.Flat,
                Resizable = DataGridViewTriState.False
            };
            colReturn.DefaultCellStyle.Font = new Font("Segoe UI", 9f);
            dgvInvoiceDetails.Columns.Add(colReturn);

            // -------------------- TAB 3: HISTORY --------------------
            historySplitLayout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 1,
                RowCount = 2,
                Padding = new Padding(8)
            };
            historySplitLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            historySplitLayout.RowStyles.Add(new RowStyle(SizeType.AutoSize));     // Row 0: Filter
            historySplitLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F)); // Row 1: Grid
            tabHistory.Controls.Add(historySplitLayout);

            // Filter Panel
            cardHistoryFilter = new Guna2Panel { Dock = DockStyle.Fill, BorderRadius = 12, FillColor = Color.White, Padding = new Padding(16), Margin = new Padding(0, 0, 0, 16) };
            cardHistoryFilter.ShadowDecoration.Enabled = true;
            historySplitLayout.Controls.Add(cardHistoryFilter, 0, 0);

            var tblHistoryFilter = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 3,
                RowCount = 1,
                Height = 65,
                BackColor = Color.Transparent
            };
            tblHistoryFilter.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 240F)); // Col 0: Search input
            tblHistoryFilter.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));       // Col 1: Search Btn
            tblHistoryFilter.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));  // Col 2: Spacer
            tblHistoryFilter.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            cardHistoryFilter.Controls.Add(tblHistoryFilter);

            txtSearchHistory = new Guna2TextBox { Dock = DockStyle.Fill, BorderRadius = 6, BorderColor = ThemeHelper.Border, Font = ThemeHelper.FontBody, ForeColor = ThemeHelper.Text, PlaceholderText = "Tìm theo mã hóa đơn, mã đơn...", Margin = new Padding(0, 4, 12, 0) };
            tblHistoryFilter.Controls.Add(txtSearchHistory, 0, 0);

            btnSearchHistory = new Guna2Button
            {
                Text = "Tìm kiếm",
                Size = new Size(110, 36),
                BorderRadius = 18,
                FillColor = ThemeHelper.Primary,
                Font = ThemeHelper.FontCaptionBold,
                ForeColor = Color.White,
                Cursor = Cursors.Hand,
                Margin = new Padding(0)
            };
            btnSearchHistory.Click += (s, e) => LoadInvoiceHistory();
            tblHistoryFilter.Controls.Add(btnSearchHistory, 1, 0);

            // Grid Panel
            cardHistoryGrid = new Guna2Panel { Dock = DockStyle.Fill, BorderRadius = 12, FillColor = Color.White, Padding = new Padding(16) };
            cardHistoryGrid.ShadowDecoration.Enabled = true;
            historySplitLayout.Controls.Add(cardHistoryGrid, 0, 1);

            dgvHistoryInvoices = new DataGridView
            {
                Dock = DockStyle.Fill,
                AutoGenerateColumns = false,
                RowHeadersVisible = false,
                AllowUserToResizeRows = false,
                AllowUserToAddRows = false
            };
            ThemeHelper.StyleFlatDataGrid(dgvHistoryInvoices);
            dgvHistoryInvoices.ColumnHeadersHeight = 44;
            dgvHistoryInvoices.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9.75f, FontStyle.Bold);
            dgvHistoryInvoices.ColumnHeadersDefaultCellStyle.WrapMode = DataGridViewTriState.False;
            dgvHistoryInvoices.EnableHeadersVisualStyles = false;
            dgvHistoryInvoices.DefaultCellStyle.Font = new Font("Segoe UI", 9f);
            dgvHistoryInvoices.DefaultCellStyle.WrapMode = DataGridViewTriState.False;

            dgvHistoryInvoices.DataError += (s, e) => { e.ThrowException = false; };
            cardHistoryGrid.Controls.Add(dgvHistoryInvoices);

            dgvHistoryInvoices.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Mã Hóa Đơn", DataPropertyName = "MaHoaDon", Width = 160, ReadOnly = true });
            dgvHistoryInvoices.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Ngày Lập", DataPropertyName = "NgayLap", Width = 160, ReadOnly = true });
            
            var colHistTotal = new DataGridViewTextBoxColumn
            {
                HeaderText = "Tổng Tiền",
                DataPropertyName = "TongTienText",
                Width = 120,
                ReadOnly = true
            };
            colHistTotal.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvHistoryInvoices.Columns.Add(colHistTotal);

            dgvHistoryInvoices.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Mã Đơn Gốc", DataPropertyName = "MaDon", Width = 130, ReadOnly = true });
            dgvHistoryInvoices.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Nhân Viên Lập", DataPropertyName = "NhanVien", AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill, FillWeight = 30, ReadOnly = true });

            // Tab selection changed
            tabThanhToan.SelectedIndexChanged += (s, e) =>
            {
                if (tabThanhToan.SelectedTab == tabHistory)
                {
                    LoadInvoiceHistory();
                }
            };
        }

        private void LoadPOSProducts()
        {
            try
            {
                var list = _productUseCase.SearchProducts("", "Tất cả")
                    .Where(p => p.TrangThaiSanPham == TrangThaiSanPham.SanSang && p.SoLuongTon > 0)
                    .ToList();
                cbPosProducts.DataSource = null;
                cbPosProducts.DataSource = list;
                cbPosProducts.DisplayMember = "TenSP";
                cbPosProducts.ValueMember = "MaSP";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải sản phẩm POS: " + ex.Message);
            }
        }

        private void LoadApprovedOrders()
        {
            try
            {
                var list = _orderUseCase.GetAllOrders()
                    .Where(o => o.TrangThaiDonHang == TrangThaiDonHang.DaDuyet)
                    .ToList();
                cbReservedOrders.DataSource = null;
                cbReservedOrders.DataSource = list;
                cbReservedOrders.DisplayMember = "MaDon";
                cbReservedOrders.ValueMember = "MaDon";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải đơn hàng: " + ex.Message);
            }
        }

        private void LoadInvoiceHistory()
        {
            try
            {
                var list = _invoiceUseCase.GetAllInvoices();
                string q = txtSearchHistory.Text.Trim().ToLower();
                if (!string.IsNullOrEmpty(q))
                {
                    list = list.Where(h => h.MaHoaDon.ToLower().Contains(q) || h.MaDon.ToLower().Contains(q)).ToList();
                }

                var display = list.Select(h => new {
                    MaHoaDon = h.MaHoaDon,
                    NgayLap = h.NgayLap.ToString("dd/MM/yyyy HH:mm"),
                    TongTienText = h.TongTien.ToString("#,##0") + " đ",
                    MaDon = h.MaDon,
                    NhanVien = h.NhanVien?.HoTen ?? h.MaNguoiDungNhanVien
                }).ToList();

                dgvHistoryInvoices.DataSource = null;
                dgvHistoryInvoices.DataSource = display;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải lịch sử hóa đơn: " + ex.Message);
            }
        }

        private void BtnPosAdd_Click(object sender, EventArgs e)
        {
            var selected = cbPosProducts.SelectedItem as SanPham;
            if (selected == null) return;

            int qty = (int)numPosQty.Value;

            var existingIndex = _posCart.FindIndex(x => x.product.MaSP == selected.MaSP);
            if (existingIndex >= 0)
            {
                var current = _posCart[existingIndex];
                if (current.product.SoLuongTon < current.quantity + qty)
                {
                    Toast.Show("Hàng trong kho không đủ để bán!", "warning");
                    return;
                }
                _posCart[existingIndex] = (current.product, current.quantity + qty);
            }
            else
            {
                if (selected.SoLuongTon < qty)
                {
                    Toast.Show("Hàng trong kho không đủ để bán!", "warning");
                    return;
                }
                _posCart.Add((selected, qty));
            }

            UpdatePosCartUI();
        }

        private void DgvPosCart_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            if (dgvPosCart.Columns[e.ColumnIndex].Name == "colPosRemove")
            {
                _posCart.RemoveAt(e.RowIndex);
                UpdatePosCartUI();
            }
        }

        private void UpdatePosCartUI()
        {
            var display = _posCart.Select(x => new
            {
                MaSP = x.product.MaSP,
                TenSP = x.product.TenSP,
                SoLuong = x.quantity,
                DonGiaText = x.product.DonGia.ToString("#,##0") + " đ",
                ThanhTienText = (x.quantity * x.product.DonGia).ToString("#,##0") + " đ"
            }).ToList();

            dgvPosCart.DataSource = null;
            dgvPosCart.DataSource = display;

            decimal total = _posCart.Sum(x => x.quantity * x.product.DonGia);
            lblPosTotal.Text = $"TỔNG THANH TOÁN: {total:#,##0} VND";
        }

        private void BtnPosClear_Click(object sender, EventArgs e)
        {
            _posCart.Clear();
            UpdatePosCartUI();
        }

        private void BtnPosCheckout_Click(object sender, EventArgs e)
        {
            try
            {
                if (!_posCart.Any())
                {
                    Toast.Show("Giỏ hàng POS trống!", "warning");
                    return;
                }

                var items = _posCart.Select(x => (x.product.MaSP, x.quantity)).ToList();
                var invoice = _invoiceUseCase.CreatePOSInvoice(_currentUser.MaNguoiDung, items);

                Toast.Show($"Thanh toán thành công! Mã HĐ: {invoice.MaHoaDon}", "success");
                
                _posCart.Clear();
                UpdatePosCartUI();
                LoadPOSProducts();
            }
            catch (Exception ex)
            {
                Toast.Show(ex.Message, "danger");
            }
        }

        private void CbReservedOrders_SelectedIndexChanged(object sender, EventArgs e)
        {
            var selectedOrder = cbReservedOrders.SelectedItem as DonDatHang;
            if (selectedOrder == null)
            {
                lblReservedDetails.Text = "Chi tiết đơn hàng:\nChưa chọn đơn.";
                return;
            }

            decimal total = selectedOrder.ChiTietDonHangs.Sum(x => x.SoLuong * x.DonGia);
            string details = $"Mã Đơn: {selectedOrder.MaDon}\n" +
                             $"Khách hàng: {selectedOrder.KhachHang?.HoTen ?? "Khách lẻ"}\n" +
                             $"Ngày đặt: {selectedOrder.NgayDat:dd/MM/yyyy HH:mm}\n" +
                             $"Mặt hàng: {selectedOrder.ChiTietDonHangs.Count}\n" +
                             $"Cần thu: {total:#,##0} VND";

            lblReservedDetails.Text = details;
        }

        private void BtnPayReserved_Click(object sender, EventArgs e)
        {
            try
            {
                var selectedOrder = cbReservedOrders.SelectedItem as DonDatHang;
                if (selectedOrder == null)
                {
                    Toast.Show("Vui lòng chọn đơn hàng!", "warning");
                    return;
                }

                var invoice = _invoiceUseCase.CreateInvoice(selectedOrder.MaDon, _currentUser.MaNguoiDung);
                Toast.Show($"Thanh toán xuất hóa đơn {invoice.MaHoaDon} thành công!", "success");
                
                LoadApprovedOrders();
                LoadPOSProducts();
            }
            catch (Exception ex)
            {
                Toast.Show("Thanh toán đơn thất bại: " + ex.Message, "danger");
            }
        }

        private void BtnSearchInvoice_Click(object sender, EventArgs e)
        {
            try
            {
                string invId = txtSearchInvoiceId.Text.Trim();
                if (string.IsNullOrEmpty(invId)) return;

                var invoice = _invoiceUseCase.GetInvoiceById(invId);
                if (invoice == null)
                {
                    Toast.Show("Không tìm thấy hóa đơn này!", "warning");
                    lblInvoiceDate.Text = "Ngày lập: ---";
                    lblInvoiceTotal.Text = "Tổng tiền: ---";
                    dgvInvoiceDetails.DataSource = null;
                    return;
                }

                lblInvoiceDate.Text = $"Ngày lập: {invoice.NgayLap:dd/MM/yyyy HH:mm}";
                lblInvoiceTotal.Text = $"Tổng tiền: {invoice.TongTien:#,##0} VND";

                var details = invoice.DonDatHang.ChiTietDonHangs.Select(ct => new
                {
                    ct.MaSP,
                    SanPhamName = ct.SanPham?.TenSP ?? ct.MaSP,
                    ct.SoLuong,
                    DonGiaText = ct.DonGia.ToString("#,##0") + " đ",
                    ThanhTienText = (ct.SoLuong * ct.DonGia).ToString("#,##0") + " đ",
                    RawDetail = ct
                }).ToList();

                dgvInvoiceDetails.DataSource = null;
                dgvInvoiceDetails.DataSource = details;
            }
            catch (Exception ex)
            {
                Toast.Show("Tìm hóa đơn lỗi: " + ex.Message, "danger");
            }
        }

        private void DgvInvoiceDetails_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            if (dgvInvoiceDetails.Columns[e.ColumnIndex].Name == "colReturn")
            {
                string invId = txtSearchInvoiceId.Text.Trim();
                if (string.IsNullOrEmpty(invId)) return;

                var invoice = _invoiceUseCase.GetInvoiceById(invId);
                if (invoice == null) return;

                var rowData = dgvInvoiceDetails.Rows[e.RowIndex].DataBoundItem;
                if (rowData == null) return;

                var propMaSP = rowData.GetType().GetProperty("MaSP");
                var propName = rowData.GetType().GetProperty("SanPhamName");
                var propQty = rowData.GetType().GetProperty("SoLuong");

                string maSp = propMaSP?.GetValue(rowData)?.ToString();
                string name = propName?.GetValue(rowData)?.ToString() ?? maSp;
                int maxQty = (int)(propQty?.GetValue(rowData) ?? 0);

                Form parent = this.FindForm();
                using (var form = new FormReturnInput(name, maxQty))
                {
                    var result = ThemeHelper.ShowCustomDialog(parent, form);
                    if (result == DialogResult.OK)
                    {
                        try
                        {
                            _invoiceUseCase.ReturnProduct(invId, maSp, form.ReturnQuantity, form.ReturnReason);
                            Toast.Show("Đổi trả và hoàn tiền hóa đơn thành công!", "success");
                            
                            BtnSearchInvoice_Click(null, null);
                            LoadPOSProducts();
                        }
                        catch (Exception ex)
                        {
                            Toast.Show(ex.Message, "danger");
                        }
                    }
                }
            }
        }
    }
}
