using System;
using System.Drawing;
using System.Windows.Forms;
using Guna.UI2.WinForms;
using QuanLyCuaHangTapHoa.Domain;

namespace QuanLyCuaHangTapHoa.Presentation.Modals
{
    public partial class FormSanPhamInput : Form
    {
        public SanPham Product { get; private set; }
        private readonly bool _isEdit;

        // Parameterless constructor for Visual Studio Designer
        public FormSanPhamInput()
        {
            Product = new SanPham();
            InitializeComponent();
            SetupData();
            SetupEvents();
        }

        public FormSanPhamInput(SanPham existingProduct = null)
        {
            _isEdit = existingProduct != null;
            if (_isEdit)
            {
                Product = new SanPham
                {
                    MaSP = existingProduct.MaSP,
                    TenSP = existingProduct.TenSP,
                    DanhMuc = existingProduct.DanhMuc,
                    DonGia = existingProduct.DonGia,
                    SoLuongTon = existingProduct.SoLuongTon,
                    TrangThaiSanPham = existingProduct.TrangThaiSanPham
                };
            }
            else
            {
                Product = new SanPham();
            }

            InitializeComponent();
            SetupData();
            SetupEvents();
        }

        private void SetupData()
        {
            lblTitle.Text = _isEdit ? "CẬP NHẬT SẢN PHẨM" : "THÊM SẢN PHẨM MỚI";

            txtMaSP.Text = Product.MaSP ?? "";
            txtMaSP.ReadOnly = _isEdit;

            txtTenSP.Text = Product.TenSP ?? "";
            txtDanhMuc.Text = Product.DanhMuc ?? "";
            txtDonGia.Text = _isEdit ? Product.DonGia.ToString("0") : "";
            txtSoLuongTon.Text = _isEdit ? Product.SoLuongTon.ToString() : "";

            cbTrangThai.DataSource = EnumTranslator.TranslateTrangThaiSanPham(false);
            cbTrangThai.DisplayMember = "Text";
            cbTrangThai.ValueMember = "Value";
            cbTrangThai.SelectedValue = Product.TrangThaiSanPham;
        }

        private void SetupEvents()
        {
            btnCancel.Click += (s, e) => this.Close();
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                string ma = txtMaSP.Text.Trim();
                string ten = txtTenSP.Text.Trim();
                string danhmuc = txtDanhMuc.Text.Trim();

                if (string.IsNullOrEmpty(ma)) throw new ArgumentException("Mã sản phẩm không được để trống.");
                if (string.IsNullOrEmpty(ten)) throw new ArgumentException("Tên sản phẩm không được để trống.");
                if (string.IsNullOrEmpty(danhmuc)) throw new ArgumentException("Danh mục không được để trống.");

                if (!decimal.TryParse(txtDonGia.Text.Trim(), out decimal gia) || gia <= 0)
                    throw new ArgumentException("Đơn giá phải là số lớn hơn 0.");
                if (!int.TryParse(txtSoLuongTon.Text.Trim(), out int ton) || ton < 0)
                    throw new ArgumentException("Số lượng tồn phải là số không âm.");

                Product.MaSP = ma;
                Product.TenSP = ten;
                Product.DanhMuc = danhmuc;
                Product.DonGia = gia;
                Product.SoLuongTon = ton;

                if (cbTrangThai.SelectedValue is TrangThaiSanPham tt)
                {
                    Product.TrangThaiSanPham = tt;
                }

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                Toast.Show(ex.Message, "danger");
            }
        }
    }
}
