using System.Drawing;
using System.Windows.Forms;
using Guna.UI2.WinForms;
using QuanLyCuaHangTapHoa.Domain;

namespace QuanLyCuaHangTapHoa.Presentation.Modals
{
    partial class FormSanPhamInput
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
            lblTitle = new Label
            {
                Text = "SẢN PHẨM",
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

            tblFields.RowStyles.Add(new RowStyle(SizeType.Percent, 16.6F));
            tblFields.RowStyles.Add(new RowStyle(SizeType.Percent, 16.6F));
            tblFields.RowStyles.Add(new RowStyle(SizeType.Percent, 16.6F));
            tblFields.RowStyles.Add(new RowStyle(SizeType.Percent, 16.6F));
            tblFields.RowStyles.Add(new RowStyle(SizeType.Percent, 16.6F));
            tblFields.RowStyles.Add(new RowStyle(SizeType.Percent, 16.6F));
            pnlMain.Controls.Add(tblFields);

            // Row 0: MaSP
            var lblMaSP = CreateLabel("Mã sản phẩm *");
            txtMaSP = CreateTextBox("", false); // Read-only if edit
            txtMaSP.PlaceholderText = "Mã định danh duy nhất...";
            tblFields.Controls.Add(lblMaSP, 0, 0);
            tblFields.Controls.Add(txtMaSP, 1, 0);

            // Row 1: TenSP
            var lblTenSP = CreateLabel("Tên sản phẩm *");
            txtTenSP = CreateTextBox("");
            txtTenSP.PlaceholderText = "Nhập tên sản phẩm...";
            tblFields.Controls.Add(lblTenSP, 0, 1);
            tblFields.Controls.Add(txtTenSP, 1, 1);

            // Row 2: DanhMuc
            var lblDanhMuc = CreateLabel("Danh mục *");
            txtDanhMuc = CreateTextBox("");
            txtDanhMuc.PlaceholderText = "Ví dụ: Gia vị, Đồ uống...";
            tblFields.Controls.Add(lblDanhMuc, 0, 2);
            tblFields.Controls.Add(txtDanhMuc, 1, 2);

            // Row 3: DonGia
            var lblDonGia = CreateLabel("Đơn giá (VND) *");
            txtDonGia = CreateTextBox("");
            txtDonGia.PlaceholderText = "Ví dụ: 15000";
            tblFields.Controls.Add(lblDonGia, 0, 3);
            tblFields.Controls.Add(txtDonGia, 1, 3);

            // Row 4: SoLuongTon
            var lblSoLuongTon = CreateLabel("Số lượng tồn *");
            txtSoLuongTon = CreateTextBox("");
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

        #endregion

        private Guna2TextBox txtMaSP;
        private Guna2TextBox txtTenSP;
        private Guna2TextBox txtDanhMuc;
        private Guna2TextBox txtDonGia;
        private Guna2TextBox txtSoLuongTon;
        private Guna2ComboBox cbTrangThai;

        private Guna2Button btnSave;
        private Guna2Button btnCancel;
        private Label lblTitle;
    }
}
