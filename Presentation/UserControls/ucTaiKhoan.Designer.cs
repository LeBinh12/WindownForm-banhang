using System.Drawing;
using System.Windows.Forms;
using Guna.UI2.WinForms;
using QuanLyCuaHangTapHoa.Domain;

namespace QuanLyCuaHangTapHoa.Presentation.UserControls
{
    partial class ucTaiKhoan
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
            tblHeader.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));    // Col 1: Add Button
            tblHeader.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tblRoot.Controls.Add(tblHeader, 0, 0);

            var lblTitle = new Label
            {
                Text = "QUẢN TRỊ TÀI KHOẢN",
                Font = ThemeHelper.FontH2,
                ForeColor = ThemeHelper.Primary,
                Anchor = AnchorStyles.Left,
                AutoSize = true,
                TextAlign = ContentAlignment.MiddleLeft
            };
            tblHeader.Controls.Add(lblTitle, 0, 0);

            btnAddNew = new Guna2Button
            {
                Text = "+ Tạo tài khoản mới",
                Anchor = AnchorStyles.Right,
                AutoSize = true,
                Padding = new Padding(12, 6, 12, 6),
                BorderRadius = 20,
                FillColor = ThemeHelper.Success,
                Font = ThemeHelper.FontBodyBold,
                ForeColor = Color.White,
                Cursor = Cursors.Hand
            };
            btnAddNew.HoverState.FillColor = Color.FromArgb(4, 120, 87);
            btnAddNew.Click += BtnAddNew_Click;
            tblHeader.Controls.Add(btnAddNew, 1, 0);

            // ==================== ROW 1: FILTERBAR (TableLayoutPanel 5 Cols) ====================
            tblFilter = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 5,
                RowCount = 1,
                Margin = new Padding(0, 0, 0, 16),
                Padding = new Padding(0),
                Height = 44
            };
            tblFilter.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 240F)); // Col 0: Search Input
            tblFilter.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 180F)); // Col 1: Role Dropdown
            tblFilter.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));       // Col 2: Search Button
            tblFilter.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));       // Col 3: Reset Button
            tblFilter.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));  // Col 4: Spacer
            tblFilter.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tblRoot.Controls.Add(tblFilter, 0, 1);

            txtSearch = new Guna2TextBox
            {
                Dock = DockStyle.Fill,
                BorderRadius = 8,
                BorderColor = ThemeHelper.Border,
                Font = ThemeHelper.FontBody,
                ForeColor = ThemeHelper.Text,
                PlaceholderText = "Tên tài khoản hoặc họ tên...",
                Margin = new Padding(0, 0, 12, 0)
            };
            tblFilter.Controls.Add(txtSearch, 0, 0);

            cbRoleSearch = new Guna2ComboBox
            {
                Dock = DockStyle.Fill,
                Margin = new Padding(0, 0, 12, 0)
            };
            ThemeHelper.StyleComboBox(cbRoleSearch);
            cbRoleSearch.DataSource = EnumTranslator.TranslateVaiTro(true);
            cbRoleSearch.DisplayMember = "Text";
            cbRoleSearch.ValueMember = "Value";
            if (cbRoleSearch.Items.Count > 0) cbRoleSearch.SelectedIndex = 0;
            tblFilter.Controls.Add(cbRoleSearch, 1, 0);

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
            tblFilter.Controls.Add(btnSearch, 2, 0);

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
            tblFilter.Controls.Add(btnReset, 3, 0);

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

            dgvAccounts = new DataGridView
            {
                Dock = DockStyle.Fill,
                AutoGenerateColumns = false,
                RowHeadersVisible = false,
                AllowUserToResizeRows = false,
                AllowUserToAddRows = false
            };
            ThemeHelper.StyleFlatDataGrid(dgvAccounts);
            dgvAccounts.CellClick += DgvAccounts_CellClick;
            cardGrid.Controls.Add(dgvAccounts);

            // Setup explicit grid columns
            dgvAccounts.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Mã ND", DataPropertyName = "MaNguoiDung", Width = 90, ReadOnly = true });
            dgvAccounts.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Tên đăng nhập", DataPropertyName = "TenDangNhap", Width = 120, ReadOnly = true });
            dgvAccounts.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Họ tên", DataPropertyName = "HoTen", AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill, FillWeight = 30, ReadOnly = true });
            dgvAccounts.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Email", DataPropertyName = "Email", Width = 160, ReadOnly = true });
            dgvAccounts.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "SĐT", DataPropertyName = "SoDienThoai", Width = 105, ReadOnly = true });
            dgvAccounts.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Vai trò", DataPropertyName = "VaiTro", Width = 100, ReadOnly = true });
            dgvAccounts.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Trạng thái", DataPropertyName = "TrangThai", Width = 110, ReadOnly = true });

            var colEdit = new DataGridViewButtonColumn
            {
                Name = "colEdit",
                HeaderText = "Sửa",
                Text = "Sửa",
                UseColumnTextForButtonValue = true,
                Width = 70,
                FlatStyle = FlatStyle.Flat,
                Resizable = DataGridViewTriState.False
            };
            colEdit.DefaultCellStyle.Font = new Font("Segoe UI", 9f);
            dgvAccounts.Columns.Add(colEdit);

            var colLock = new DataGridViewButtonColumn
            {
                Name = "colLock",
                HeaderText = "Khóa/Mở",
                Text = "Khóa/Mở",
                UseColumnTextForButtonValue = true,
                Width = 110,
                FlatStyle = FlatStyle.Flat,
                Resizable = DataGridViewTriState.False
            };
            colLock.DefaultCellStyle.Font = new Font("Segoe UI", 9f);
            dgvAccounts.Columns.Add(colLock);
        }

        #endregion

        // UI Controls
        private TableLayoutPanel tblRoot;
        private TableLayoutPanel tblHeader;
        private TableLayoutPanel tblFilter;
        private Guna2Panel cardGrid;

        private DataGridView dgvAccounts;
        private Guna2TextBox txtSearch;
        private Guna2ComboBox cbRoleSearch;
        private Guna2Button btnSearch;
        private Guna2Button btnReset;
        private Guna2Button btnAddNew;
    }
}
