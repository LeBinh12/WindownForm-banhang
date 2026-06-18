using System.Drawing;
using System.Windows.Forms;
using Guna.UI2.WinForms;
using QuanLyCuaHangTapHoa.Domain;

namespace QuanLyCuaHangTapHoa.Presentation.UserControls
{
    partial class ucDonHang
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.Dock = DockStyle.Fill;
            this.BackColor = ThemeHelper.BackgroundApp;

            // 1. Root TableLayoutPanel (1 Column x 3 Rows, Padding = 20)
            tblRoot = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 1,
                RowCount = 3,
                Padding = new Padding(20),
                BackColor = Color.Transparent
            };
            tblRoot.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tblRoot.RowStyles.Add(new RowStyle(SizeType.AutoSize)); // Row 0: Header
            tblRoot.RowStyles.Add(new RowStyle(SizeType.AutoSize)); // Row 1: FilterBar
            tblRoot.RowStyles.Add(new RowStyle(SizeType.Percent, 100F)); // Row 2: Card Grid
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
            tblHeader.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));    // Col 1: Action Buttons Flow
            tblHeader.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tblRoot.Controls.Add(tblHeader, 0, 0);

            var lblTitle = new Label
            {
                Text = "YÊU CẦU ĐẶT GIỮ HÀNG",
                Font = ThemeHelper.FontH2,
                ForeColor = ThemeHelper.Primary,
                Anchor = AnchorStyles.Left,
                AutoSize = true,
                TextAlign = ContentAlignment.MiddleLeft
            };
            tblHeader.Controls.Add(lblTitle, 0, 0);

            // Header actions layout
            flowHeaderActions = new FlowLayoutPanel
            {
                FlowDirection = FlowDirection.RightToLeft,
                WrapContents = false,
                AutoSize = true,
                Anchor = AnchorStyles.Right,
                BackColor = Color.Transparent,
                Margin = new Padding(0)
            };
            tblHeader.Controls.Add(flowHeaderActions, 1, 0);

            btnCleanExpired = new Guna2Button
            {
                Text = "Thu hồi đơn hết hạn",
                AutoSize = true,
                Padding = new Padding(12, 6, 12, 6),
                BorderRadius = 20,
                FillColor = ThemeHelper.Warning,
                Font = ThemeHelper.FontBodyBold,
                ForeColor = Color.White,
                Cursor = Cursors.Hand,
                Margin = new Padding(8, 0, 0, 0)
            };
            btnCleanExpired.HoverState.FillColor = Color.FromArgb(217, 119, 6);
            btnCleanExpired.Click += BtnCleanExpired_Click;

            btnAddNew = new Guna2Button
            {
                Text = "+ Tạo yêu cầu đặt giữ",
                AutoSize = true,
                Padding = new Padding(12, 6, 12, 6),
                BorderRadius = 20,
                FillColor = ThemeHelper.Success,
                Font = ThemeHelper.FontBodyBold,
                ForeColor = Color.White,
                Cursor = Cursors.Hand,
                Margin = new Padding(8, 0, 0, 0)
            };
            btnAddNew.HoverState.FillColor = Color.FromArgb(4, 120, 87);
            btnAddNew.Click += BtnAddNew_Click;

            // ==================== ROW 1: FILTERBAR (TableLayoutPanel 4 Cols) ====================
            tblFilter = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 4,
                RowCount = 1,
                Margin = new Padding(0, 0, 0, 16),
                Padding = new Padding(0),
                Height = 44
            };
            tblFilter.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 280F)); // Col 0: Search Input
            tblFilter.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));       // Col 1: Search Button
            tblFilter.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));       // Col 2: Reset Button
            tblFilter.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));  // Col 3: Spacer
            tblFilter.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tblRoot.Controls.Add(tblFilter, 0, 1);

            txtSearch = new Guna2TextBox
            {
                Dock = DockStyle.Fill,
                BorderRadius = 8,
                BorderColor = ThemeHelper.Border,
                Font = ThemeHelper.FontBody,
                ForeColor = ThemeHelper.Text,
                PlaceholderText = "Mã đơn hoặc tên khách...",
                Margin = new Padding(0, 0, 12, 0)
            };
            tblFilter.Controls.Add(txtSearch, 0, 0);

            btnSearch = new Guna2Button
            {
                Text = "Tìm lọc",
                Size = new Size(110, 36),
                BorderRadius = 18,
                FillColor = ThemeHelper.Primary,
                Font = ThemeHelper.FontBodyBold,
                ForeColor = Color.White,
                Cursor = Cursors.Hand,
                Margin = new Padding(0, 0, 12, 0)
            };
            btnSearch.HoverState.FillColor = ThemeHelper.PrimaryHover;
            tblFilter.Controls.Add(btnSearch, 1, 0);

            btnReset = new Guna2Button
            {
                Text = "Xóa lọc",
                Size = new Size(110, 36),
                BorderRadius = 18,
                FillColor = ThemeHelper.BorderLight,
                Font = ThemeHelper.FontBodyBold,
                ForeColor = ThemeHelper.TextSecondary,
                Cursor = Cursors.Hand,
                Margin = new Padding(0, 0, 0, 0)
            };
            btnReset.HoverState.FillColor = ThemeHelper.Border;
            btnReset.Click += BtnReset_Click;
            tblFilter.Controls.Add(btnReset, 2, 0);

            // ==================== ROW 2: GRID CARD ====================
            cardGrid = new Guna2Panel
            {
                Dock = DockStyle.Fill,
                BorderRadius = 12,
                FillColor = Color.White,
                Padding = new Padding(16),
                Margin = new Padding(0)
            };
            cardGrid.ShadowDecoration.Enabled = true;
            tblRoot.Controls.Add(cardGrid, 0, 2);

            dgvOrders = new DataGridView
            {
                Dock = DockStyle.Fill,
                AutoGenerateColumns = false,
                RowHeadersVisible = false,
                AllowUserToResizeRows = false,
                AllowUserToAddRows = false
            };
            ThemeHelper.StyleFlatDataGrid(dgvOrders);
            dgvOrders.CellClick += DgvOrders_CellClick;
            cardGrid.Controls.Add(dgvOrders);

            // Setup explicit grid columns
            dgvOrders.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Mã Đơn", DataPropertyName = "MaDon", Width = 110, ReadOnly = true });
            dgvOrders.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Khách hàng", DataPropertyName = "KhachHang", AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill, FillWeight = 35, ReadOnly = true });
            dgvOrders.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Ngày đặt", DataPropertyName = "NgayDat", Width = 140, ReadOnly = true });
            dgvOrders.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Ngày duyệt", DataPropertyName = "NgayDuyet", Width = 140, ReadOnly = true });
            dgvOrders.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Trạng thái", DataPropertyName = "TrangThaiText", Width = 145, ReadOnly = true });

            // Action columns
            var colView = new DataGridViewButtonColumn
            {
                Name = "colView",
                HeaderText = "Chi tiết",
                Text = "Xem",
                UseColumnTextForButtonValue = true,
                Width = 70,
                FlatStyle = FlatStyle.Flat,
                Resizable = DataGridViewTriState.False
            };
            colView.DefaultCellStyle.Font = new Font("Segoe UI", 9f);
            dgvOrders.Columns.Add(colView);


        }

        #endregion

        // UI Controls
        private TableLayoutPanel tblRoot;
        private TableLayoutPanel tblHeader;
        private FlowLayoutPanel flowHeaderActions;
        private TableLayoutPanel tblFilter;
        private Guna2Panel cardGrid;

        private DataGridView dgvOrders;
        private Guna2TextBox txtSearch;
        private Guna2Button btnSearch;
        private Guna2Button btnReset;
        private Guna2Button btnAddNew;
        private Guna2Button btnCleanExpired;
    }
}
