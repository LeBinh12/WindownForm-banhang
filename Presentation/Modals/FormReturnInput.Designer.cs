using System.Drawing;
using System.Windows.Forms;
using Guna.UI2.WinForms;

namespace QuanLyCuaHangTapHoa.Presentation.Modals
{
    partial class FormReturnInput
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
            this.lblProductHeader = new System.Windows.Forms.Label();
            this.lblProduct = new System.Windows.Forms.Label();
            this.lblPurchasedHeader = new System.Windows.Forms.Label();
            this.lblPurchased = new System.Windows.Forms.Label();
            this.lblReturnQtyHeader = new System.Windows.Forms.Label();
            this.numQty = new System.Windows.Forms.NumericUpDown();
            this.lblReasonHeader = new System.Windows.Forms.Label();
            this.txtReason = new Guna.UI2.WinForms.Guna2TextBox();
            this.btnCancel = new Guna.UI2.WinForms.Guna2Button();
            this.btnSave = new Guna.UI2.WinForms.Guna2Button();

            // 
            // pnlMain
            // 
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Padding = new System.Windows.Forms.Padding(20);
            this.pnlMain.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.pnlMain);

            // 
            // lblTitle
            // 
            this.lblTitle.Text = "YÊU CẦU TRẢ HÀNG LỖI";
            this.lblTitle.Font = ThemeHelper.FontSubheading;
            this.lblTitle.ForeColor = ThemeHelper.Primary;
            this.lblTitle.AutoSize = true;
            this.lblTitle.Location = new System.Drawing.Point(20, 15);
            this.pnlMain.Controls.Add(this.lblTitle);

            // 
            // tblFields
            // 
            this.tblFields.Location = new System.Drawing.Point(20, 50);
            this.tblFields.Size = new System.Drawing.Size(420, 200);
            this.tblFields.ColumnCount = 2;
            this.tblFields.RowCount = 4;
            this.tblFields.BackColor = System.Drawing.Color.Transparent;
            this.tblFields.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 130));
            this.tblFields.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100));
            this.tblFields.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 45));
            this.tblFields.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 45));
            this.tblFields.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 45));
            this.tblFields.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100));
            this.pnlMain.Controls.Add(this.tblFields);

            // 
            // lblProductHeader
            // 
            this.lblProductHeader.Text = "Sản phẩm:";
            this.lblProductHeader.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblProductHeader.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblProductHeader.Font = ThemeHelper.FontCaptionBold;
            this.lblProductHeader.ForeColor = ThemeHelper.TextSecondary;
            this.lblProductHeader.Padding = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this.tblFields.Controls.Add(this.lblProductHeader, 0, 0);

            // 
            // lblProduct
            // 
            this.lblProduct.Text = "Sữa tươi Vinamilk 1L";
            this.lblProduct.Font = ThemeHelper.FontBodyBold;
            this.lblProduct.ForeColor = ThemeHelper.Text;
            this.lblProduct.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblProduct.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.tblFields.Controls.Add(this.lblProduct, 1, 0);

            // 
            // lblPurchasedHeader
            // 
            this.lblPurchasedHeader.Text = "Hóa đơn đã mua:";
            this.lblPurchasedHeader.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblPurchasedHeader.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblPurchasedHeader.Font = ThemeHelper.FontCaptionBold;
            this.lblPurchasedHeader.ForeColor = ThemeHelper.TextSecondary;
            this.lblPurchasedHeader.Padding = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this.tblFields.Controls.Add(this.lblPurchasedHeader, 0, 1);

            // 
            // lblPurchased
            // 
            this.lblPurchased.Text = "5 sản phẩm";
            this.lblPurchased.Font = ThemeHelper.FontBody;
            this.lblPurchased.ForeColor = ThemeHelper.TextSecondary;
            this.lblPurchased.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblPurchased.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.tblFields.Controls.Add(this.lblPurchased, 1, 1);

            // 
            // lblReturnQtyHeader
            // 
            this.lblReturnQtyHeader.Text = "Số lượng trả lỗi *";
            this.lblReturnQtyHeader.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblReturnQtyHeader.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblReturnQtyHeader.Font = ThemeHelper.FontCaptionBold;
            this.lblReturnQtyHeader.ForeColor = ThemeHelper.TextSecondary;
            this.lblReturnQtyHeader.Padding = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this.tblFields.Controls.Add(this.lblReturnQtyHeader, 0, 2);

            // 
            // numQty
            // 
            this.numQty.Dock = System.Windows.Forms.DockStyle.Fill;
            this.numQty.Minimum = 1;
            this.numQty.Maximum = 5;
            this.numQty.Value = 1;
            this.numQty.Font = ThemeHelper.FontBody;
            this.tblFields.Controls.Add(this.numQty, 1, 2);

            // 
            // lblReasonHeader
            // 
            this.lblReasonHeader.Text = "Lý do trả lỗi *";
            this.lblReasonHeader.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblReasonHeader.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblReasonHeader.Font = ThemeHelper.FontCaptionBold;
            this.lblReasonHeader.ForeColor = ThemeHelper.TextSecondary;
            this.lblReasonHeader.Padding = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this.tblFields.Controls.Add(this.lblReasonHeader, 0, 3);

            // 
            // txtReason
            // 
            this.txtReason.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtReason.Height = 36;
            this.txtReason.BorderRadius = 8;
            this.txtReason.BorderColor = ThemeHelper.Border;
            this.txtReason.Font = ThemeHelper.FontBody;
            this.txtReason.ForeColor = ThemeHelper.Text;
            this.txtReason.PlaceholderText = "Nhập lý do lỗi chi tiết...";
            this.tblFields.Controls.Add(this.txtReason, 1, 3);

            // 
            // btnCancel
            // 
            this.btnCancel.Text = "Hủy bỏ";
            this.btnCancel.Location = new System.Drawing.Point(220, 265);
            this.btnCancel.Size = new System.Drawing.Size(100, 36);
            this.btnCancel.BorderRadius = 18;
            this.btnCancel.FillColor = ThemeHelper.BorderLight;
            this.btnCancel.Font = ThemeHelper.FontBodyBold;
            this.btnCancel.ForeColor = ThemeHelper.TextSecondary;
            this.btnCancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancel.HoverState.FillColor = ThemeHelper.Border;
            this.pnlMain.Controls.Add(this.btnCancel);

            // 
            // btnSave
            // 
            this.btnSave.Text = "Xác nhận";
            this.btnSave.Location = new System.Drawing.Point(330, 265);
            this.btnSave.Size = new System.Drawing.Size(110, 36);
            this.btnSave.BorderRadius = 18;
            this.btnSave.FillColor = ThemeHelper.Danger;
            this.btnSave.Font = ThemeHelper.FontBodyBold;
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSave.HoverState.FillColor = System.Drawing.Color.FromArgb(185, 28, 28);
            this.pnlMain.Controls.Add(this.btnSave);

            // FORM properties
            this.Size = new System.Drawing.Size(460, 320);
            this.BackColor = System.Drawing.Color.White;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
        }

        #endregion

        private System.Windows.Forms.Panel pnlMain;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.TableLayoutPanel tblFields;
        private System.Windows.Forms.Label lblProductHeader;
        private System.Windows.Forms.Label lblPurchasedHeader;
        private System.Windows.Forms.Label lblReturnQtyHeader;
        private System.Windows.Forms.Label lblReasonHeader;

        private Label lblProduct;
        private Label lblPurchased;
        private NumericUpDown numQty;
        private Guna2TextBox txtReason;
        private Guna2Button btnSave;
        private Guna2Button btnCancel;
    }
}
