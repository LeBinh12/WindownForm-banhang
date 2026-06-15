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
    public class ucSanPham : UserControl
    {
        private readonly IProductUseCase _productUseCase;
        private readonly TaiKhoan _currentUser;

        // UI Controls
        private TableLayoutPanel tblRoot;
        private TableLayoutPanel tblHeader;
        private TableLayoutPanel tblFilter;
        private Guna2Panel cardGrid;

        private DataGridView dgvProducts;
        private Guna2TextBox txtSearch;
        private Guna2ComboBox cbCategorySearch;
        private Guna2Button btnSearch;
        private Guna2Button btnReset;
        private Guna2Button btnAddNew;

        public ucSanPham(IProductUseCase productUseCase, TaiKhoan currentUser)
        {
            _productUseCase = productUseCase;
            _currentUser = currentUser;

            InitializeComponent();
            LoadCategories();
            LoadData();
        }

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
                Text = "DANH MỤC SẢN PHẨM",
                Font = ThemeHelper.FontH2,
                ForeColor = ThemeHelper.Primary,
                Anchor = AnchorStyles.Left,
                AutoSize = true,
                TextAlign = ContentAlignment.MiddleLeft
            };
            tblHeader.Controls.Add(lblTitle, 0, 0);

            btnAddNew = new Guna2Button
            {
                Text = "+ Thêm sản phẩm mới",
                Anchor = AnchorStyles.Right,
                AutoSize = true,
                Padding = new Padding(12, 6, 12, 6),
                BorderRadius = 20,
                FillColor = ThemeHelper.Success,
                HoverState = { FillColor = Color.FromArgb(4, 120, 87) },
                Font = ThemeHelper.FontBodyBold,
                ForeColor = Color.White,
                Cursor = Cursors.Hand,
                Visible = (_currentUser.NguoiDung is NhanVien || _currentUser.NguoiDung is Admin)
            };
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
            tblFilter.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 180F)); // Col 1: Category Dropdown
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
                PlaceholderText = "Nhập tên hoặc mã sản phẩm...",
                Margin = new Padding(0, 0, 12, 0)
            };
            tblFilter.Controls.Add(txtSearch, 0, 0);

            cbCategorySearch = new Guna2ComboBox
            {
                Dock = DockStyle.Fill,
                Margin = new Padding(0, 0, 12, 0)
            };
            ThemeHelper.StyleComboBox(cbCategorySearch);
            tblFilter.Controls.Add(cbCategorySearch, 1, 0);

            btnSearch = new Guna2Button
            {
                Text = "Tìm lọc",
                Size = new Size(110, 36),
                BorderRadius = 18,
                FillColor = ThemeHelper.Primary,
                HoverState = { FillColor = ThemeHelper.PrimaryHover },
                Font = ThemeHelper.FontBodyBold,
                ForeColor = Color.White,
                Cursor = Cursors.Hand,
                Margin = new Padding(0, 0, 12, 0)
            };
            btnSearch.Click += (s, e) => LoadData();
            tblFilter.Controls.Add(btnSearch, 2, 0);

            btnReset = new Guna2Button
            {
                Text = "Xóa lọc",
                Size = new Size(110, 36),
                BorderRadius = 18,
                FillColor = ThemeHelper.BorderLight,
                HoverState = { FillColor = ThemeHelper.Border },
                Font = ThemeHelper.FontBodyBold,
                ForeColor = ThemeHelper.TextSecondary,
                Cursor = Cursors.Hand,
                Margin = new Padding(0, 0, 0, 0)
            };
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

            dgvProducts = new DataGridView
            {
                Dock = DockStyle.Fill,
                AutoGenerateColumns = false,
                RowHeadersVisible = false,
                AllowUserToResizeRows = false,
                AllowUserToAddRows = false
            };
            ThemeHelper.StyleFlatDataGrid(dgvProducts);
            dgvProducts.CellClick += DgvProducts_CellClick;
            cardGrid.Controls.Add(dgvProducts);

            // Setup explicit grid columns
            dgvProducts.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Mã SP", DataPropertyName = "MaSP", Width = 90, ReadOnly = true });
            dgvProducts.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Tên sản phẩm", DataPropertyName = "TenSP", AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill, FillWeight = 40, ReadOnly = true });
            dgvProducts.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Danh mục", DataPropertyName = "DanhMuc", Width = 130, ReadOnly = true });
            
            var colPrice = new DataGridViewTextBoxColumn
            {
                HeaderText = "Đơn giá",
                DataPropertyName = "DonGiaText",
                Width = 110,
                ReadOnly = true
            };
            colPrice.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            colPrice.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvProducts.Columns.Add(colPrice);

            var colStock = new DataGridViewTextBoxColumn
            {
                HeaderText = "Tồn kho",
                DataPropertyName = "SoLuongTon",
                Width = 80,
                ReadOnly = true
            };
            colStock.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            colStock.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvProducts.Columns.Add(colStock);

            dgvProducts.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Trạng Thái", DataPropertyName = "TrangThai", Width = 150, ReadOnly = true });

            bool isStaff = (_currentUser.NguoiDung is NhanVien || _currentUser.NguoiDung is Admin);
            if (isStaff)
            {
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
                dgvProducts.Columns.Add(colEdit);

                var colDelete = new DataGridViewButtonColumn
                {
                    Name = "colDelete",
                    HeaderText = "Ngừng KD",
                    Text = "Ngừng KD",
                    UseColumnTextForButtonValue = true,
                    Width = 110,
                    FlatStyle = FlatStyle.Flat,
                    Resizable = DataGridViewTriState.False
                };
                colDelete.DefaultCellStyle.Font = new Font("Segoe UI", 9f);
                dgvProducts.Columns.Add(colDelete);
            }
        }

        private void LoadCategories()
        {
            try
            {
                var list = new List<ComboItem>();
                list.Add(new ComboItem { Text = "Tất cả danh mục", Value = "Tất cả" });

                var products = _productUseCase.GetAllProducts();
                var categories = new HashSet<string>();
                foreach (var p in products)
                {
                    if (!string.IsNullOrWhiteSpace(p.DanhMuc))
                        categories.Add(p.DanhMuc);
                }

                foreach (var cat in categories)
                {
                    list.Add(new ComboItem { Text = cat, Value = cat });
                }

                cbCategorySearch.DataSource = null;
                cbCategorySearch.DataSource = list;
                cbCategorySearch.DisplayMember = "Text";
                cbCategorySearch.ValueMember = "Value";
                if (cbCategorySearch.Items.Count > 0) cbCategorySearch.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải danh mục: " + ex.Message);
            }
        }

        private void LoadData()
        {
            try
            {
                string selectedCategory = cbCategorySearch.SelectedValue?.ToString() ?? "Tất cả";
                var list = _productUseCase.SearchProducts(txtSearch.Text.Trim(), selectedCategory);
                
                var viewList = list.Select(p => new SanPhamViewModel
                {
                    MaSP = p.MaSP,
                    TenSP = p.TenSP,
                    DanhMuc = p.DanhMuc,
                    DonGia = p.DonGia,
                    SoLuongTon = p.SoLuongTon,
                    TrangThai = MapTrangThaiText(p.TrangThaiSanPham),
                    RawProduct = p
                }).ToList();

                dgvProducts.DataSource = null;
                dgvProducts.DataSource = viewList;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải dữ liệu sản phẩm: " + ex.Message);
            }
        }

        private string MapTrangThaiText(TrangThaiSanPham status)
        {
            return status switch
            {
                TrangThaiSanPham.SanSang => "🟢 Sẵn sàng",
                TrangThaiSanPham.ChoXuatKho => "🟡 Chờ xuất",
                TrangThaiSanPham.DaBan => "🔵 Đã bán",
                TrangThaiSanPham.Hong => "🔴 Hỏng",
                TrangThaiSanPham.NgungKinhDoanh => "⚫ Ngừng kinh doanh",
                _ => status.ToString()
            };
        }

        private void BtnReset_Click(object sender, EventArgs e)
        {
            txtSearch.Clear();
            if (cbCategorySearch.Items.Count > 0) cbCategorySearch.SelectedIndex = 0;
            LoadData();
        }

        private void BtnAddNew_Click(object sender, EventArgs e)
        {
            Form parent = this.FindForm();
            using (var form = new FormSanPhamInput())
            {
                var result = ThemeHelper.ShowCustomDialog(parent, form);
                if (result == DialogResult.OK)
                {
                    try
                    {
                        _productUseCase.AddProduct(form.Product);
                        Toast.Show("Thêm sản phẩm mới thành công!", "success");
                        LoadData();
                        LoadCategories();
                    }
                    catch (Exception ex)
                    {
                        Toast.Show(ex.Message, "danger");
                    }
                }
            }
        }

        private void DgvProducts_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            var selectedItem = dgvProducts.Rows[e.RowIndex].DataBoundItem as SanPhamViewModel;
            if (selectedItem == null) return;

            // EDIT ACTION
            if (dgvProducts.Columns[e.ColumnIndex].Name == "colEdit")
            {
                Form parent = this.FindForm();
                using (var form = new FormSanPhamInput(selectedItem.RawProduct))
                {
                    var result = ThemeHelper.ShowCustomDialog(parent, form);
                    if (result == DialogResult.OK)
                    {
                        try
                        {
                            _productUseCase.UpdateProduct(form.Product);
                            Toast.Show("Cập nhật thông tin sản phẩm thành công!", "success");
                            LoadData();
                            LoadCategories();
                        }
                        catch (Exception ex)
                        {
                            Toast.Show(ex.Message, "danger");
                        }
                    }
                }
            }

            // DELETE ACTION
            if (dgvProducts.Columns[e.ColumnIndex].Name == "colDelete")
            {
                Form parent = this.FindForm();
                bool confirm = ThemeHelper.ShowConfirmDialog(parent, "Ngừng kinh doanh", $"Bạn có chắc muốn ngừng bán sản phẩm {selectedItem.MaSP}?");
                if (confirm)
                {
                    try
                    {
                        _productUseCase.DeleteProduct(selectedItem.MaSP);
                        Toast.Show("Đã cập nhật ngừng kinh doanh sản phẩm này!", "warning");
                        LoadData();
                    }
                    catch (Exception ex)
                    {
                        Toast.Show(ex.Message, "danger");
                    }
                }
            }
        }

        private class SanPhamViewModel
        {
            public string MaSP { get; set; }
            public string TenSP { get; set; }
            public string DanhMuc { get; set; }
            public decimal DonGia { get; set; }
            public string DonGiaText => DonGia.ToString("#,##0") + " đ";
            public int SoLuongTon { get; set; }
            public string TrangThai { get; set; }
            public SanPham RawProduct { get; set; }
        }
    }
}
