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
            this.Size = new Size(460, 320);
            this.BackColor = Color.White;
            this.FormBorderStyle = FormBorderStyle.None;
            this.StartPosition = FormStartPosition.CenterParent;

            Panel pnlMain = new Panel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(20),
                BorderStyle = BorderStyle.FixedSingle
            };
            this.Controls.Add(pnlMain);

            Label lblTitle = new Label
            {
                Text = "YÊU CẦU TRẢ HÀNG LỖI",
                Font = ThemeHelper.FontSubheading,
                ForeColor = ThemeHelper.Primary,
                AutoSize = true,
                Location = new Point(20, 15)
            };
            pnlMain.Controls.Add(lblTitle);

            // TableLayout
            TableLayoutPanel tblFields = new TableLayoutPanel
            {
                Location = new Point(20, 50),
                Size = new Size(420, 200),
                ColumnCount = 2,
                RowCount = 4,
                BackColor = Color.Transparent
            };
            tblFields.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 130));
            tblFields.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));

            tblFields.RowStyles.Add(new RowStyle(SizeType.Absolute, 45)); // Product Name
            tblFields.RowStyles.Add(new RowStyle(SizeType.Absolute, 45)); // Purchased Qty
            tblFields.RowStyles.Add(new RowStyle(SizeType.Absolute, 45)); // Return Qty
            tblFields.RowStyles.Add(new RowStyle(SizeType.Percent, 100)); // Reason

            pnlMain.Controls.Add(tblFields);

            // Row 0: Product Info
            tblFields.Controls.Add(CreateLabel("Sản phẩm:"), 0, 0);
            lblProduct = new Label
            {
                Text = "Sản phẩm",
                Font = ThemeHelper.FontBodyBold,
                ForeColor = ThemeHelper.Text,
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleLeft
            };
            tblFields.Controls.Add(lblProduct, 1, 0);

            // Row 1: Max Qty
            tblFields.Controls.Add(CreateLabel("Hóa đơn đã mua:"), 0, 1);
            lblPurchased = new Label
            {
                Text = "0 sản phẩm",
                Font = ThemeHelper.FontBody,
                ForeColor = ThemeHelper.TextSecondary,
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleLeft
            };
            tblFields.Controls.Add(lblPurchased, 1, 1);

            // Row 2: Return Qty
            tblFields.Controls.Add(CreateLabel("Số lượng trả lỗi *"), 0, 2);
            numQty = new NumericUpDown
            {
                Dock = DockStyle.Fill,
                Minimum = 1,
                Maximum = 1,
                Value = 1,
                Font = ThemeHelper.FontBody
            };
            tblFields.Controls.Add(numQty, 1, 2);

            // Row 3: Reason
            tblFields.Controls.Add(CreateLabel("Lý do trả lỗi *"), 0, 3);
            txtReason = new Guna2TextBox
            {
                Dock = DockStyle.Fill,
                Height = 36,
                BorderRadius = 8,
                BorderColor = ThemeHelper.Border,
                Font = ThemeHelper.FontBody,
                ForeColor = ThemeHelper.Text,
                PlaceholderText = "Nhập lý do lỗi chi tiết..."
            };
            tblFields.Controls.Add(txtReason, 1, 3);

            // Buttons
            btnCancel = new Guna2Button
            {
                Text = "Hủy bỏ",
                Location = new Point(220, 265),
                Size = new Size(100, 36),
                BorderRadius = 18,
                FillColor = ThemeHelper.BorderLight,
                HoverState = { FillColor = ThemeHelper.Border },
                Font = ThemeHelper.FontBodyBold,
                ForeColor = ThemeHelper.TextSecondary,
                Cursor = Cursors.Hand
            };
            pnlMain.Controls.Add(btnCancel);

            btnSave = new Guna2Button
            {
                Text = "Xác nhận",
                Location = new Point(330, 265),
                Size = new Size(110, 36),
                BorderRadius = 18,
                FillColor = ThemeHelper.Danger,
                HoverState = { FillColor = Color.FromArgb(185, 28, 28) },
                Font = ThemeHelper.FontBodyBold,
                ForeColor = Color.White,
                Cursor = Cursors.Hand
            };
            btnSave.Click += BtnSave_Click;
            pnlMain.Controls.Add(btnSave);
        }

        private Label CreateLabel(string text)
        {
            return new Label
            {
                Text = text,
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleRight,
                Font = ThemeHelper.FontCaptionBold,
                ForeColor = ThemeHelper.TextSecondary,
                Padding = new Padding(0, 0, 10, 0)
            };
        }

        #endregion

        private Label lblProduct;
        private Label lblPurchased;
        private NumericUpDown numQty;
        private Guna2TextBox txtReason;
        private Guna2Button btnSave;
        private Guna2Button btnCancel;
    }
}
