using System;
using System.Drawing;
using System.Windows.Forms;
using Guna.UI2.WinForms;
using QuanLyCuaHangTapHoa.Domain;

namespace QuanLyCuaHangTapHoa.Presentation.Modals
{
    partial class FormOrderDetails
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
            this.MinimumSize = new Size(700, 540);
            this.Size        = new Size(760, 580);
            this.BackColor   = ThemeHelper.BackgroundApp;
            this.FormBorderStyle = FormBorderStyle.None;
            this.StartPosition   = FormStartPosition.CenterParent;

            var root = new TableLayoutPanel
            {
                Dock        = DockStyle.Fill,
                ColumnCount = 1,
                RowCount    = 4,
                Padding     = new Padding(20),
                BackColor   = ThemeHelper.BackgroundApp
            };
            root.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            root.RowStyles.Add(new RowStyle(SizeType.AutoSize));          // 0 info card
            root.RowStyles.Add(new RowStyle(SizeType.AutoSize));          // 1 section label
            root.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));     // 2 grid
            root.RowStyles.Add(new RowStyle(SizeType.AutoSize));          // 3 footer
            this.Controls.Add(root);

            // ── ROW 0: Info Card ──────────────────────────────────────
            var cardInfo = new Guna2Panel
            {
                Dock            = DockStyle.Fill,
                BorderRadius    = 12,
                FillColor       = Color.White,
                Padding         = new Padding(16),
                Margin          = new Padding(0, 0, 0, 12),
                AutoSize        = true,
                AutoSizeMode    = AutoSizeMode.GrowAndShrink
            };
            cardInfo.ShadowDecoration.Enabled = true;
            root.Controls.Add(cardInfo, 0, 0);

            // 2-column label grid inside card: [label | value] × 5 rows
            var tblInfo = new TableLayoutPanel
            {
                ColumnCount = 4,   // Label | Value | Label | Value
                RowCount    = 3,
                AutoSize    = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
                Dock        = DockStyle.Top,
                BackColor   = Color.Transparent
            };
            tblInfo.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 110F));
            tblInfo.ColumnStyles.Add(new ColumnStyle(SizeType.Percent,  40F));
            tblInfo.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 110F));
            tblInfo.ColumnStyles.Add(new ColumnStyle(SizeType.Percent,  60F));
            cardInfo.Controls.Add(tblInfo);

            lblTitle = new Label
            {
                Text      = "CHI TIẾT ĐƠN HÀNG",
                Font      = ThemeHelper.FontSubheading,
                ForeColor = ThemeHelper.Primary,
                AutoSize  = true,
                Margin    = new Padding(0, 0, 0, 10)
            };
            tblInfo.Controls.Add(lblTitle, 0, 0);
            tblInfo.SetColumnSpan(lblTitle, 4);

            lblValNgayDat = new Label { Text = "18/06/2026", Font = ThemeHelper.FontBody, ForeColor = ThemeHelper.Text, AutoSize = true, Margin = new Padding(0, 0, 0, 6), Anchor = AnchorStyles.Left };
            lblValTrangThai = new Label { Text = "Cho duyet", Font = ThemeHelper.FontBodyBold, ForeColor = ThemeHelper.Text, AutoSize = true, Margin = new Padding(0, 0, 0, 6), Anchor = AnchorStyles.Left };
            lblValKhachHang = new Label { Text = "Nguyen Van A", Font = ThemeHelper.FontBody, ForeColor = ThemeHelper.Text, AutoSize = true, Margin = new Padding(0, 0, 0, 6), Anchor = AnchorStyles.Left };
            lblValNgayDuyet = new Label { Text = "Chua duyet", Font = ThemeHelper.FontBodyBold, ForeColor = ThemeHelper.Text, AutoSize = true, Margin = new Padding(0, 0, 0, 6), Anchor = AnchorStyles.Left };

            AddInfoRow(tblInfo, 1, "Ngày đặt:", lblValNgayDat, "Trạng thái:", lblValTrangThai);
            AddInfoRow(tblInfo, 2, "Khách hàng:", lblValKhachHang, "Ngày duyệt:", lblValNgayDuyet);

            // ── ROW 1: Section label ──────────────────────────────────
            var lblSection = new Label
            {
                Text      = "CHI TIẾT MẶT HÀNG",
                Font      = ThemeHelper.FontCaptionBold,
                ForeColor = ThemeHelper.TextSecondary,
                AutoSize  = true,
                Margin    = new Padding(0, 4, 0, 6)
            };
            root.Controls.Add(lblSection, 0, 1);

            // ── ROW 2: Grid Card ──────────────────────────────────────
            var cardGrid = new Guna2Panel
            {
                Dock         = DockStyle.Fill,
                BorderRadius = 12,
                FillColor    = Color.White,
                Padding      = new Padding(12),
                Margin       = new Padding(0, 0, 0, 12)
            };
            cardGrid.ShadowDecoration.Enabled = true;
            root.Controls.Add(cardGrid, 0, 2);

            dgvDetails = new DataGridView
            {
                Dock                 = DockStyle.Fill,
                AutoGenerateColumns  = false
            };
            ThemeHelper.StyleFlatDataGrid(dgvDetails);
            cardGrid.Controls.Add(dgvDetails);

            // Columns: Tên SP (fill) | Đơn giá (right) | SL (center) | Thành tiền (right)
            dgvDetails.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText       = "Tên sản phẩm",
                DataPropertyName = "TenSanPham",
                AutoSizeMode     = DataGridViewAutoSizeColumnMode.Fill,
                FillWeight       = 50,
                ReadOnly         = true
            });

            var colPrice = new DataGridViewTextBoxColumn
            {
                HeaderText       = "Đơn giá",
                DataPropertyName = "DonGiaText",
                Width            = 110,
                ReadOnly         = true
            };
            colPrice.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            colPrice.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvDetails.Columns.Add(colPrice);

            var colQty = new DataGridViewTextBoxColumn
            {
                HeaderText       = "SL đặt",
                DataPropertyName = "SoLuong",
                Width            = 80,
                ReadOnly         = true
            };
            colQty.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            colQty.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvDetails.Columns.Add(colQty);

            var colTotal = new DataGridViewTextBoxColumn
            {
                HeaderText       = "Thành tiền",
                DataPropertyName = "ThanhTienText",
                Width            = 120,
                ReadOnly         = true
            };
            colTotal.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            colTotal.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvDetails.Columns.Add(colTotal);

            // ── ROW 3: Footer Panel ───────────────────────────────────
            pnlFooter = new Panel
            {
                Dock    = DockStyle.Fill,
                Height  = 46,
                Margin  = new Padding(0)
            };
            root.Controls.Add(pnlFooter, 0, 3);

            lblTotal = new Label
            {
                Text      = "TỔNG TIỀN: 0 VND",
                Font      = ThemeHelper.FontH2,
                ForeColor = ThemeHelper.Danger,
                AutoSize  = true,
                Location  = new Point(0, 10)
            };
            pnlFooter.Controls.Add(lblTotal);

            btnClose = new Guna2Button
            {
                Text         = "Đóng",
                Location     = new Point(600, 4),
                Size         = new Size(120, 38),
                BorderRadius = 19,
                FillColor    = ThemeHelper.Primary,
                Font         = ThemeHelper.FontBodyBold,
                ForeColor    = Color.White,
                Cursor       = Cursors.Hand
            };
            pnlFooter.Controls.Add(btnClose);
        }

        private void AddInfoRow(TableLayoutPanel table, int row, string label1, Label val1Label, string label2, Label val2Label)
        {
            var l1 = new Label { Text = label1, Font = ThemeHelper.FontCaptionBold, ForeColor = ThemeHelper.TextSecondary, AutoSize = true, Margin = new Padding(0, 0, 0, 6), Anchor = AnchorStyles.Left };
            var l2 = new Label { Text = label2, Font = ThemeHelper.FontCaptionBold, ForeColor = ThemeHelper.TextSecondary, AutoSize = true, Margin = new Padding(0, 0, 0, 6), Anchor = AnchorStyles.Left };

            table.Controls.Add(l1, 0, row);
            table.Controls.Add(val1Label, 1, row);
            table.Controls.Add(l2, 2, row);
            table.Controls.Add(val2Label, 3, row);
        }

        #endregion

        private DataGridView dgvDetails;
        private Label lblTotal;
        private Guna2Button btnClose;
        private Label lblTitle;
        private Label lblValNgayDat;
        private Label lblValTrangThai;
        private Label lblValKhachHang;
        private Label lblValNgayDuyet;
        private Panel pnlFooter;
    }
}
