using System.Drawing;
using System.Windows.Forms;
using Guna.UI2.WinForms;

namespace QuanLyCuaHangTapHoa.Presentation.Modals
{
    partial class FormCustomerCart
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
            this.splitLayout = new System.Windows.Forms.TableLayoutPanel();
            this.pnlLeft = new System.Windows.Forms.Panel();
            this.lblSelect = new System.Windows.Forms.Label();
            this.cbProducts = new Guna.UI2.WinForms.Guna2ComboBox();
            this.lblQty = new System.Windows.Forms.Label();
            this.numQty = new System.Windows.Forms.NumericUpDown();
            this.btnAdd = new Guna.UI2.WinForms.Guna2Button();
            this.pnlRight = new System.Windows.Forms.Panel();
            this.dgvCart = new System.Windows.Forms.DataGridView();
            this.colRemove = new System.Windows.Forms.DataGridViewButtonColumn();
            this.lblTotal = new System.Windows.Forms.Label();
            this.btnCancel = new Guna.UI2.WinForms.Guna2Button();
            this.btnSubmit = new Guna.UI2.WinForms.Guna2Button();

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
            this.lblTitle.Text = "TẠO YÊU CẦU ĐẶT GIỮ HÀNG KHO";
            this.lblTitle.Font = ThemeHelper.FontSubheading;
            this.lblTitle.ForeColor = ThemeHelper.Primary;
            this.lblTitle.AutoSize = true;
            this.lblTitle.Location = new System.Drawing.Point(20, 15);
            this.pnlMain.Controls.Add(this.lblTitle);

            // 
            // splitLayout
            // 
            this.splitLayout.Location = new System.Drawing.Point(20, 50);
            this.splitLayout.Size = new System.Drawing.Size(640, 350);
            this.splitLayout.ColumnCount = 2;
            this.splitLayout.RowCount = 1;
            this.splitLayout.BackColor = System.Drawing.Color.Transparent;
            this.splitLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.splitLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 60F));
            this.pnlMain.Controls.Add(this.splitLayout);

            // 
            // pnlLeft
            // 
            this.pnlLeft.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlLeft.Padding = new System.Windows.Forms.Padding(0, 10, 10, 0);
            this.splitLayout.Controls.Add(this.pnlLeft, 0, 0);

            // 
            // lblSelect
            // 
            this.lblSelect.Text = "Chọn sản phẩm *";
            this.lblSelect.Font = ThemeHelper.FontCaptionBold;
            this.lblSelect.ForeColor = ThemeHelper.TextSecondary;
            this.lblSelect.Location = new System.Drawing.Point(0, 10);
            this.lblSelect.AutoSize = true;

            // 
            // cbProducts
            // 
            this.cbProducts.Location = new System.Drawing.Point(0, 30);
            this.cbProducts.Width = 230;
            this.cbProducts.Height = 36;

            this.pnlLeft.Controls.Add(this.lblSelect);
            this.pnlLeft.Controls.Add(this.cbProducts);

            // 
            // lblQty
            // 
            this.lblQty.Text = "Số lượng đặt *";
            this.lblQty.Font = ThemeHelper.FontCaptionBold;
            this.lblQty.ForeColor = ThemeHelper.TextSecondary;
            this.lblQty.Location = new System.Drawing.Point(0, 80);
            this.lblQty.AutoSize = true;

            // 
            // numQty
            // 
            this.numQty.Location = new System.Drawing.Point(0, 100);
            this.numQty.Width = 100;
            this.numQty.Minimum = 1;
            this.numQty.Maximum = 100;
            this.numQty.Font = ThemeHelper.FontBody;

            this.pnlLeft.Controls.Add(this.lblQty);
            this.pnlLeft.Controls.Add(this.numQty);

            // 
            // btnAdd
            // 
            this.btnAdd.Text = "Thêm vào giỏ";
            this.btnAdd.Location = new System.Drawing.Point(0, 160);
            this.btnAdd.Size = new System.Drawing.Size(160, 38);
            this.btnAdd.BorderRadius = 19;
            this.btnAdd.FillColor = ThemeHelper.Primary;
            this.btnAdd.Font = ThemeHelper.FontBodyBold;
            this.btnAdd.ForeColor = System.Drawing.Color.White;
            this.btnAdd.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pnlLeft.Controls.Add(this.btnAdd);

            // 
            // pnlRight
            // 
            this.pnlRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlRight.Padding = new System.Windows.Forms.Padding(10, 10, 0, 0);
            this.splitLayout.Controls.Add(this.pnlRight, 1, 0);

            // 
            // dgvCart
            // 
            this.dgvCart.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvCart.AutoGenerateColumns = false;
            this.pnlRight.Controls.Add(this.dgvCart);

            // columns
            var colProduct = new System.Windows.Forms.DataGridViewTextBoxColumn();
            colProduct.HeaderText = "Sản phẩm";
            colProduct.DataPropertyName = "TenSP";
            colProduct.Width = 150;
            colProduct.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dgvCart.Columns.Add(colProduct);

            var colQty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            colQty.HeaderText = "SL";
            colQty.DataPropertyName = "quantity";
            colQty.Width = 50;
            this.dgvCart.Columns.Add(colQty);

            var colTotal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            colTotal.HeaderText = "Thành tiền";
            colTotal.DataPropertyName = "ThanhTienText";
            colTotal.Width = 100;
            this.dgvCart.Columns.Add(colTotal);

            // colRemove
            this.colRemove.Name = "colRemove";
            this.colRemove.HeaderText = "Xóa";
            this.colRemove.Text = "Xóa";
            this.colRemove.UseColumnTextForButtonValue = true;
            this.colRemove.Width = 60;
            this.dgvCart.Columns.Add(this.colRemove);

            // 
            // lblTotal
            // 
            this.lblTotal.Text = "TỔNG ĐƠN: 0 VND";
            this.lblTotal.Font = ThemeHelper.FontBodyBold;
            this.lblTotal.ForeColor = ThemeHelper.Danger;
            this.lblTotal.AutoSize = true;
            this.lblTotal.Location = new System.Drawing.Point(20, 425);
            this.pnlMain.Controls.Add(this.lblTotal);

            // 
            // btnCancel
            // 
            this.btnCancel.Text = "Hủy bỏ";
            this.btnCancel.Location = new System.Drawing.Point(440, 415);
            this.btnCancel.Size = new System.Drawing.Size(100, 38);
            this.btnCancel.BorderRadius = 19;
            this.btnCancel.FillColor = ThemeHelper.BorderLight;
            this.btnCancel.HoverState.FillColor = ThemeHelper.Border;
            this.btnCancel.Font = ThemeHelper.FontBodyBold;
            this.btnCancel.ForeColor = ThemeHelper.TextSecondary;
            this.btnCancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pnlMain.Controls.Add(this.btnCancel);

            // 
            // btnSubmit
            // 
            this.btnSubmit.Text = "Gửi yêu cầu";
            this.btnSubmit.Location = new System.Drawing.Point(550, 415);
            this.btnSubmit.Size = new System.Drawing.Size(110, 38);
            this.btnSubmit.BorderRadius = 19;
            this.btnSubmit.FillColor = ThemeHelper.Success;
            this.btnSubmit.HoverState.FillColor = System.Drawing.Color.FromArgb(4, 120, 87);
            this.btnSubmit.Font = ThemeHelper.FontBodyBold;
            this.btnSubmit.ForeColor = System.Drawing.Color.White;
            this.btnSubmit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pnlMain.Controls.Add(this.btnSubmit);

            // FORM properties
            this.Size = new System.Drawing.Size(680, 480);
            this.BackColor = System.Drawing.Color.White;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
        }

        #endregion

        private System.Windows.Forms.Panel pnlMain;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.TableLayoutPanel splitLayout;
        private System.Windows.Forms.Panel pnlLeft;
        private System.Windows.Forms.Label lblSelect;
        private System.Windows.Forms.Label lblQty;
        private System.Windows.Forms.Panel pnlRight;
        private System.Windows.Forms.DataGridViewButtonColumn colRemove;

        private Guna2ComboBox cbProducts;
        private NumericUpDown numQty;
        private Guna2Button btnAdd;
        private DataGridView dgvCart;
        private Label lblTotal;
        private Guna2Button btnSubmit;
        private Guna2Button btnCancel;
    }
}
