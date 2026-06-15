using System;
using System.Drawing;
using System.Windows.Forms;
using Guna.UI2.WinForms;
using QuanLyCuaHangTapHoa.Domain;

namespace QuanLyCuaHangTapHoa.Presentation.Modals
{
    public class FormSanPhamInput : Form
    {
        public SanPham Product { get; private set; }
        private readonly bool _isEdit;

        private Guna2TextBox txtMaSP;
        private Guna2TextBox txtTenSP;
        private Guna2TextBox txtDanhMuc;
        private Guna2TextBox txtDonGia;
        private Guna2TextBox txtSoLuongTon;
        private Guna2ComboBox cbTrangThai;

        private Guna2Button btnSave;
        private Guna2Button btnCancel;

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
        }

        private void InitializeComponent()
        {
            this.Size = new Size(500, 480);
            this.BackColor = Color.White;
            this.FormBorderStyle = FormBorderStyle.None;
            this.StartPosition = FormStartPosition.CenterParent;

            // Border Panel
            Panel pnlMain = new Panel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(24),
                BorderStyle = BorderStyle.FixedSingle
            };
            this.Controls.Add(pnlMain);

            // Title
            Label lblTitle = new Label
            {
                Text = _isEdit ? "CẬP NHẬT SẢN PHẨM" : "THÊM SẢN PHẨM MỚI",
                Font = ThemeHelper.FontSubheading,
                ForeColor = ThemeHelper.Primary,
                AutoSize = true,
                Location = new Point(24, 20)
            };
            pnlMain.Controls.Add(lblTitle);

            // TableLayout for fields
            TableLayoutPanel tblFields = new TableLayoutPanel
            {
                Location = new Point(24, 60),
                Size = new Size(450, 320),
                ColumnCount = 2,
                RowCount = 6,
                BackColor = Color.Transparent
            };
            tblFields.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 140));
            tblFields.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));

            for (int i = 0; i < 6; i++)
            {
                tblFields.RowStyles.Add(new RowStyle(SizeType.Percent, 16.6F));
            }
            pnlMain.Controls.Add(tblFields);

            // Row 0: MaSP
            var lblMaSP = CreateLabel("Mã sản phẩm *");
            txtMaSP = CreateTextBox(Product.MaSP, _isEdit); // Read-only if edit
            txtMaSP.PlaceholderText = "Mã định danh duy nhất...";
            tblFields.Controls.Add(lblMaSP, 0, 0);
            tblFields.Controls.Add(txtMaSP, 1, 0);

            // Row 1: TenSP
            var lblTenSP = CreateLabel("Tên sản phẩm *");
            txtTenSP = CreateTextBox(Product.TenSP);
            txtTenSP.PlaceholderText = "Nhập tên sản phẩm...";
            tblFields.Controls.Add(lblTenSP, 0, 1);
            tblFields.Controls.Add(txtTenSP, 1, 1);

            // Row 2: DanhMuc
            var lblDanhMuc = CreateLabel("Danh mục *");
            txtDanhMuc = CreateTextBox(Product.DanhMuc);
            txtDanhMuc.PlaceholderText = "Ví dụ: Gia vị, Đồ uống...";
            tblFields.Controls.Add(lblDanhMuc, 0, 2);
            tblFields.Controls.Add(txtDanhMuc, 1, 2);

            // Row 3: DonGia
            var lblDonGia = CreateLabel("Đơn giá (VND) *");
            txtDonGia = CreateTextBox(_isEdit ? Product.DonGia.ToString("0") : "");
            txtDonGia.PlaceholderText = "Ví dụ: 15000";
            tblFields.Controls.Add(lblDonGia, 0, 3);
            tblFields.Controls.Add(txtDonGia, 1, 3);

            // Row 4: SoLuongTon
            var lblSoLuongTon = CreateLabel("Số lượng tồn *");
            txtSoLuongTon = CreateTextBox(_isEdit ? Product.SoLuongTon.ToString() : "");
            txtSoLuongTon.PlaceholderText = "Ví dụ: 100";
            tblFields.Controls.Add(lblSoLuongTon, 0, 4);
            tblFields.Controls.Add(txtSoLuongTon, 1, 4);

            // Row 5: TrangThai
            var lblTrangThai = CreateLabel("Trạng thái");
            cbTrangThai = new Guna2ComboBox
            {
                Dock = DockStyle.Fill,
                Margin = new Padding(0)
            };
            ThemeHelper.StyleComboBox(cbTrangThai);
            cbTrangThai.DataSource = EnumTranslator.TranslateTrangThaiSanPham(false);
            cbTrangThai.DisplayMember = "Text";
            cbTrangThai.ValueMember = "Value";
            cbTrangThai.SelectedValue = Product.TrangThaiSanPham;
            tblFields.Controls.Add(lblTrangThai, 0, 5);
            tblFields.Controls.Add(cbTrangThai, 1, 5);

            // FlowLayout for buttons
            FlowLayoutPanel pnlButtons = new FlowLayoutPanel
            {
                Location = new Point(24, 400),
                Size = new Size(450, 50),
                FlowDirection = FlowDirection.RightToLeft,
                BackColor = Color.Transparent
            };
            pnlMain.Controls.Add(pnlButtons);

            btnCancel = new Guna2Button
            {
                Text = "Hủy bỏ",
                Size = new Size(110, 40),
                BorderRadius = 20,
                FillColor = ThemeHelper.BorderLight,
                HoverState = { FillColor = ThemeHelper.Border },
                Font = ThemeHelper.FontBodyBold,
                ForeColor = ThemeHelper.TextSecondary,
                Cursor = Cursors.Hand
            };
            btnCancel.Click += (s, e) => this.Close();
            pnlButtons.Controls.Add(btnCancel);

            btnSave = new Guna2Button
            {
                Text = "Lưu lại",
                Size = new Size(110, 40),
                BorderRadius = 20,
                FillColor = ThemeHelper.Success,
                HoverState = { FillColor = Color.FromArgb(4, 120, 87) },
                Font = ThemeHelper.FontBodyBold,
                ForeColor = Color.White,
                Cursor = Cursors.Hand
            };
            btnSave.Click += BtnSave_Click;
            pnlButtons.Controls.Add(btnSave);
        }

        private Label CreateLabel(string text)
        {
            var lbl = new Label
            {
                Text = text,
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleRight,
                Font = ThemeHelper.FontCaptionBold,
                ForeColor = ThemeHelper.TextSecondary,
                Padding = new Padding(0, 0, 10, 0)
            };
            return lbl;
        }

        private Guna2TextBox CreateTextBox(string initialText, bool isReadOnly = false)
        {
            var txt = new Guna2TextBox
            {
                Dock = DockStyle.Fill,
                Size = new Size(290, 36),
                BorderRadius = 8,
                BorderColor = ThemeHelper.Border,
                Font = ThemeHelper.FontBody,
                ForeColor = ThemeHelper.Text,
                ReadOnly = isReadOnly,
                Text = initialText ?? ""
            };
            return txt;
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
