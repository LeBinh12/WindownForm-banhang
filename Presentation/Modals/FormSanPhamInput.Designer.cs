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
            this.pnlMain = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.tblFields = new System.Windows.Forms.TableLayoutPanel();
            this.lblMaSP = new System.Windows.Forms.Label();
            this.txtMaSP = new Guna.UI2.WinForms.Guna2TextBox();
            this.lblTenSP = new System.Windows.Forms.Label();
            this.txtTenSP = new Guna.UI2.WinForms.Guna2TextBox();
            this.lblDanhMuc = new System.Windows.Forms.Label();
            this.txtDanhMuc = new Guna.UI2.WinForms.Guna2TextBox();
            this.lblDonGia = new System.Windows.Forms.Label();
            this.txtDonGia = new Guna.UI2.WinForms.Guna2TextBox();
            this.lblSoLuongTon = new System.Windows.Forms.Label();
            this.txtSoLuongTon = new Guna.UI2.WinForms.Guna2TextBox();
            this.lblTrangThai = new System.Windows.Forms.Label();
            this.cbTrangThai = new Guna.UI2.WinForms.Guna2ComboBox();
            this.pnlButtons = new System.Windows.Forms.FlowLayoutPanel();
            this.btnCancel = new Guna.UI2.WinForms.Guna2Button();
            this.btnSave = new Guna.UI2.WinForms.Guna2Button();

            // 
            // pnlMain
            // 
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Padding = new System.Windows.Forms.Padding(24);
            this.pnlMain.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.pnlMain);

            // 
            // lblTitle
            // 
            this.lblTitle.Text = "SẢN PHẨM";
            this.lblTitle.Font = ThemeHelper.FontSubheading;
            this.lblTitle.ForeColor = ThemeHelper.Primary;
            this.lblTitle.AutoSize = true;
            this.lblTitle.Location = new System.Drawing.Point(24, 20);
            this.pnlMain.Controls.Add(this.lblTitle);

            // 
            // tblFields
            // 
            this.tblFields.Location = new System.Drawing.Point(24, 60);
            this.tblFields.Size = new System.Drawing.Size(450, 320);
            this.tblFields.ColumnCount = 2;
            this.tblFields.RowCount = 6;
            this.tblFields.BackColor = System.Drawing.Color.Transparent;
            this.tblFields.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 140));
            this.tblFields.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100));
            this.tblFields.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.6F));
            this.tblFields.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.6F));
            this.tblFields.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.6F));
            this.tblFields.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.6F));
            this.tblFields.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.6F));
            this.tblFields.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.6F));
            this.pnlMain.Controls.Add(this.tblFields);

            // 
            // lblMaSP
            // 
            this.lblMaSP.Text = "Mã sản phẩm *";
            this.lblMaSP.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblMaSP.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblMaSP.Font = ThemeHelper.FontCaptionBold;
            this.lblMaSP.ForeColor = ThemeHelper.TextSecondary;
            this.lblMaSP.Padding = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this.tblFields.Controls.Add(this.lblMaSP, 0, 0);

            // 
            // txtMaSP
            // 
            this.txtMaSP.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtMaSP.Size = new System.Drawing.Size(290, 36);
            this.txtMaSP.BorderRadius = 8;
            this.txtMaSP.BorderColor = ThemeHelper.Border;
            this.txtMaSP.Font = ThemeHelper.FontBody;
            this.txtMaSP.ForeColor = ThemeHelper.Text;
            this.txtMaSP.PlaceholderText = "Mã định danh duy nhất...";
            this.txtMaSP.Text = "SP001";
            this.tblFields.Controls.Add(this.txtMaSP, 1, 0);

            // 
            // lblTenSP
            // 
            this.lblTenSP.Text = "Tên sản phẩm *";
            this.lblTenSP.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTenSP.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblTenSP.Font = ThemeHelper.FontCaptionBold;
            this.lblTenSP.ForeColor = ThemeHelper.TextSecondary;
            this.lblTenSP.Padding = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this.tblFields.Controls.Add(this.lblTenSP, 0, 1);

            // 
            // txtTenSP
            // 
            this.txtTenSP.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtTenSP.Size = new System.Drawing.Size(290, 36);
            this.txtTenSP.BorderRadius = 8;
            this.txtTenSP.BorderColor = ThemeHelper.Border;
            this.txtTenSP.Font = ThemeHelper.FontBody;
            this.txtTenSP.ForeColor = ThemeHelper.Text;
            this.txtTenSP.PlaceholderText = "Nhập tên sản phẩm...";
            this.txtTenSP.Text = "Sữa tươi Vinamilk 1L";
            this.tblFields.Controls.Add(this.txtTenSP, 1, 1);

            // 
            // lblDanhMuc
            // 
            this.lblDanhMuc.Text = "Danh mục *";
            this.lblDanhMuc.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblDanhMuc.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblDanhMuc.Font = ThemeHelper.FontCaptionBold;
            this.lblDanhMuc.ForeColor = ThemeHelper.TextSecondary;
            this.lblDanhMuc.Padding = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this.tblFields.Controls.Add(this.lblDanhMuc, 0, 2);

            // 
            // txtDanhMuc
            // 
            this.txtDanhMuc.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtDanhMuc.Size = new System.Drawing.Size(290, 36);
            this.txtDanhMuc.BorderRadius = 8;
            this.txtDanhMuc.BorderColor = ThemeHelper.Border;
            this.txtDanhMuc.Font = ThemeHelper.FontBody;
            this.txtDanhMuc.ForeColor = ThemeHelper.Text;
            this.txtDanhMuc.PlaceholderText = "Ví dụ: Gia vị, Đồ uống...";
            this.txtDanhMuc.Text = "Đồ uống";
            this.tblFields.Controls.Add(this.txtDanhMuc, 1, 2);

            // 
            // lblDonGia
            // 
            this.lblDonGia.Text = "Đơn giá (VND) *";
            this.lblDonGia.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblDonGia.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblDonGia.Font = ThemeHelper.FontCaptionBold;
            this.lblDonGia.ForeColor = ThemeHelper.TextSecondary;
            this.lblDonGia.Padding = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this.tblFields.Controls.Add(this.lblDonGia, 0, 3);

            // 
            // txtDonGia
            // 
            this.txtDonGia.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtDonGia.Size = new System.Drawing.Size(290, 36);
            this.txtDonGia.BorderRadius = 8;
            this.txtDonGia.BorderColor = ThemeHelper.Border;
            this.txtDonGia.Font = ThemeHelper.FontBody;
            this.txtDonGia.ForeColor = ThemeHelper.Text;
            this.txtDonGia.PlaceholderText = "Ví dụ: 15000";
            this.txtDonGia.Text = "30000";
            this.tblFields.Controls.Add(this.txtDonGia, 1, 3);

            // 
            // lblSoLuongTon
            // 
            this.lblSoLuongTon.Text = "Số lượng tồn *";
            this.lblSoLuongTon.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblSoLuongTon.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblSoLuongTon.Font = ThemeHelper.FontCaptionBold;
            this.lblSoLuongTon.ForeColor = ThemeHelper.TextSecondary;
            this.lblSoLuongTon.Padding = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this.tblFields.Controls.Add(this.lblSoLuongTon, 0, 4);

            // 
            // txtSoLuongTon
            // 
            this.txtSoLuongTon.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtSoLuongTon.Size = new System.Drawing.Size(290, 36);
            this.txtSoLuongTon.BorderRadius = 8;
            this.txtSoLuongTon.BorderColor = ThemeHelper.Border;
            this.txtSoLuongTon.Font = ThemeHelper.FontBody;
            this.txtSoLuongTon.ForeColor = ThemeHelper.Text;
            this.txtSoLuongTon.PlaceholderText = "Ví dụ: 100";
            this.txtSoLuongTon.Text = "50";
            this.tblFields.Controls.Add(this.txtSoLuongTon, 1, 4);

            // 
            // lblTrangThai
            // 
            this.lblTrangThai.Text = "Trạng thái";
            this.lblTrangThai.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTrangThai.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblTrangThai.Font = ThemeHelper.FontCaptionBold;
            this.lblTrangThai.ForeColor = ThemeHelper.TextSecondary;
            this.lblTrangThai.Padding = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this.tblFields.Controls.Add(this.lblTrangThai, 0, 5);

            // 
            // cbTrangThai
            // 
            this.cbTrangThai.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cbTrangThai.Margin = new System.Windows.Forms.Padding(0);
            this.tblFields.Controls.Add(this.cbTrangThai, 1, 5);

            // 
            // pnlButtons
            // 
            this.pnlButtons.Location = new System.Drawing.Point(24, 400);
            this.pnlButtons.Size = new System.Drawing.Size(450, 50);
            this.pnlButtons.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.pnlButtons.BackColor = System.Drawing.Color.Transparent;
            this.pnlMain.Controls.Add(this.pnlButtons);

            // 
            // btnCancel
            // 
            this.btnCancel.Text = "Hủy bỏ";
            this.btnCancel.Size = new System.Drawing.Size(110, 40);
            this.btnCancel.BorderRadius = 20;
            this.btnCancel.FillColor = ThemeHelper.BorderLight;
            this.btnCancel.HoverState.FillColor = ThemeHelper.Border;
            this.btnCancel.Font = ThemeHelper.FontBodyBold;
            this.btnCancel.ForeColor = ThemeHelper.TextSecondary;
            this.btnCancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pnlButtons.Controls.Add(this.btnCancel);

            // 
            // btnSave
            // 
            this.btnSave.Text = "Lưu lại";
            this.btnSave.Size = new System.Drawing.Size(110, 40);
            this.btnSave.BorderRadius = 20;
            this.btnSave.FillColor = ThemeHelper.Success;
            this.btnSave.HoverState.FillColor = System.Drawing.Color.FromArgb(4, 120, 87);
            this.btnSave.Font = ThemeHelper.FontBodyBold;
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pnlButtons.Controls.Add(this.btnSave);

            // FORM properties
            this.Size = new System.Drawing.Size(500, 480);
            this.BackColor = System.Drawing.Color.White;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
        }

        #endregion

        private System.Windows.Forms.Panel pnlMain;
        private System.Windows.Forms.TableLayoutPanel tblFields;
        private System.Windows.Forms.Label lblMaSP;
        private System.Windows.Forms.Label lblTenSP;
        private System.Windows.Forms.Label lblDanhMuc;
        private System.Windows.Forms.Label lblDonGia;
        private System.Windows.Forms.Label lblSoLuongTon;
        private System.Windows.Forms.Label lblTrangThai;
        private System.Windows.Forms.FlowLayoutPanel pnlButtons;

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
