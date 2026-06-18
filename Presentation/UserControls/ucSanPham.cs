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
    public partial class ucSanPham : UserControl
    {
        private readonly IProductUseCase _productUseCase;
        private readonly TaiKhoan _currentUser;

        // Parameterless constructor for Visual Studio Designer
        public ucSanPham()
        {
            InitializeComponent();
            SetupEvents();
            SetupData();
        }

        public ucSanPham(IProductUseCase productUseCase, TaiKhoan currentUser)
        {
            _productUseCase = productUseCase;
            _currentUser = currentUser;

            InitializeComponent();
            SetupEvents();
            SetupData();
            LoadCategories();
            LoadData();
        }

        private void SetupEvents()
        {
            btnSearch.Click += (s, e) => LoadData();
        }

        private void SetupData()
        {
            bool isStaff = _currentUser != null && (_currentUser.NguoiDung is NhanVien || _currentUser.NguoiDung is Admin);
            btnAddNew.Visible = isStaff;

            if (isStaff)
            {
                if (!dgvProducts.Columns.Contains("colEdit"))
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
                }

                if (!dgvProducts.Columns.Contains("colDelete"))
                {
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
        }

        private void LoadCategories()
        {
            if (_productUseCase == null) return;
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
            if (_productUseCase == null) return;
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
            if (_productUseCase == null) return;
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
            if (e.RowIndex < 0 || _productUseCase == null) return;

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
