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
            this.root = new System.Windows.Forms.TableLayoutPanel();
            this.cardInfo = new Guna.UI2.WinForms.Guna2Panel();
            this.tblInfo = new System.Windows.Forms.TableLayoutPanel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblNgayDat = new System.Windows.Forms.Label();
            this.lblValNgayDat = new System.Windows.Forms.Label();
            this.lblTrangThai = new System.Windows.Forms.Label();
            this.lblValTrangThai = new System.Windows.Forms.Label();
            this.lblKhachHang = new System.Windows.Forms.Label();
            this.lblValKhachHang = new System.Windows.Forms.Label();
            this.lblNgayDuyet = new System.Windows.Forms.Label();
            this.lblValNgayDuyet = new System.Windows.Forms.Label();
            this.lblSection = new System.Windows.Forms.Label();
            this.cardGrid = new Guna.UI2.WinForms.Guna2Panel();
            this.dgvDetails = new System.Windows.Forms.DataGridView();
            this.pnlFooter = new System.Windows.Forms.Panel();
            this.lblTotal = new System.Windows.Forms.Label();
            this.btnClose = new Guna.UI2.WinForms.Guna2Button();

            // 
            // root
            // 
            this.root.Dock = System.Windows.Forms.DockStyle.Fill;
            this.root.ColumnCount = 1;
            this.root.RowCount = 4;
            this.root.Padding = new System.Windows.Forms.Padding(20);
            this.root.BackColor = ThemeHelper.BackgroundApp;
            this.root.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.root.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.AutoSize));
            this.root.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.AutoSize));
            this.root.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.root.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.AutoSize));
            this.Controls.Add(this.root);

            // 
            // cardInfo
            // 
            this.cardInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cardInfo.BorderRadius = 12;
            this.cardInfo.FillColor = System.Drawing.Color.White;
            this.cardInfo.Padding = new System.Windows.Forms.Padding(16);
            this.cardInfo.Margin = new System.Windows.Forms.Padding(0, 0, 0, 12);
            this.cardInfo.AutoSize = true;
            this.cardInfo.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.cardInfo.ShadowDecoration.Enabled = true;
            this.root.Controls.Add(this.cardInfo, 0, 0);

            // 
            // tblInfo
            // 
            this.tblInfo.ColumnCount = 4;
            this.tblInfo.RowCount = 3;
            this.tblInfo.AutoSize = true;
            this.tblInfo.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tblInfo.Dock = System.Windows.Forms.DockStyle.Top;
            this.tblInfo.BackColor = System.Drawing.Color.Transparent;
            this.tblInfo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 110F));
            this.tblInfo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.tblInfo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 110F));
            this.tblInfo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 60F));
            this.cardInfo.Controls.Add(this.tblInfo);

            // 
            // lblTitle
            // 
            this.lblTitle.Text = "CHI TIẾT ĐƠN HÀNG";
            this.lblTitle.Font = ThemeHelper.FontSubheading;
            this.lblTitle.ForeColor = ThemeHelper.Primary;
            this.lblTitle.AutoSize = true;
            this.lblTitle.Margin = new System.Windows.Forms.Padding(0, 0, 0, 10);
            this.tblInfo.Controls.Add(this.lblTitle, 0, 0);
            this.tblInfo.SetColumnSpan(this.lblTitle, 4);

            // 
            // lblNgayDat
            // 
            this.lblNgayDat.Text = "Ngày đặt:";
            this.lblNgayDat.Font = ThemeHelper.FontCaptionBold;
            this.lblNgayDat.ForeColor = ThemeHelper.TextSecondary;
            this.lblNgayDat.AutoSize = true;
            this.lblNgayDat.Margin = new System.Windows.Forms.Padding(0, 0, 0, 6);
            this.lblNgayDat.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.tblInfo.Controls.Add(this.lblNgayDat, 0, 1);

            // 
            // lblValNgayDat
            // 
            this.lblValNgayDat.Text = "18/06/2026";
            this.lblValNgayDat.Font = ThemeHelper.FontBody;
            this.lblValNgayDat.ForeColor = ThemeHelper.Text;
            this.lblValNgayDat.AutoSize = true;
            this.lblValNgayDat.Margin = new System.Windows.Forms.Padding(0, 0, 0, 6);
            this.lblValNgayDat.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.tblInfo.Controls.Add(this.lblValNgayDat, 1, 1);

            // 
            // lblTrangThai
            // 
            this.lblTrangThai.Text = "Trạng thái:";
            this.lblTrangThai.Font = ThemeHelper.FontCaptionBold;
            this.lblTrangThai.ForeColor = ThemeHelper.TextSecondary;
            this.lblTrangThai.AutoSize = true;
            this.lblTrangThai.Margin = new System.Windows.Forms.Padding(0, 0, 0, 6);
            this.lblTrangThai.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.tblInfo.Controls.Add(this.lblTrangThai, 2, 1);

            // 
            // lblValTrangThai
            // 
            this.lblValTrangThai.Text = "Cho duyet";
            this.lblValTrangThai.Font = ThemeHelper.FontBodyBold;
            this.lblValTrangThai.ForeColor = ThemeHelper.Text;
            this.lblValTrangThai.AutoSize = true;
            this.lblValTrangThai.Margin = new System.Windows.Forms.Padding(0, 0, 0, 6);
            this.lblValTrangThai.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.tblInfo.Controls.Add(this.lblValTrangThai, 3, 1);

            // 
            // lblKhachHang
            // 
            this.lblKhachHang.Text = "Khách hàng:";
            this.lblKhachHang.Font = ThemeHelper.FontCaptionBold;
            this.lblKhachHang.ForeColor = ThemeHelper.TextSecondary;
            this.lblKhachHang.AutoSize = true;
            this.lblKhachHang.Margin = new System.Windows.Forms.Padding(0, 0, 0, 6);
            this.lblKhachHang.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.tblInfo.Controls.Add(this.lblKhachHang, 0, 2);

            // 
            // lblValKhachHang
            // 
            this.lblValKhachHang.Text = "Nguyen Van A";
            this.lblValKhachHang.Font = ThemeHelper.FontBody;
            this.lblValKhachHang.ForeColor = ThemeHelper.Text;
            this.lblValKhachHang.AutoSize = true;
            this.lblValKhachHang.Margin = new System.Windows.Forms.Padding(0, 0, 0, 6);
            this.lblValKhachHang.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.tblInfo.Controls.Add(this.lblValKhachHang, 1, 2);

            // 
            // lblNgayDuyet
            // 
            this.lblNgayDuyet.Text = "Ngày duyệt:";
            this.lblNgayDuyet.Font = ThemeHelper.FontCaptionBold;
            this.lblNgayDuyet.ForeColor = ThemeHelper.TextSecondary;
            this.lblNgayDuyet.AutoSize = true;
            this.lblNgayDuyet.Margin = new System.Windows.Forms.Padding(0, 0, 0, 6);
            this.lblNgayDuyet.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.tblInfo.Controls.Add(this.lblNgayDuyet, 2, 2);

            // 
            // lblValNgayDuyet
            // 
            this.lblValNgayDuyet.Text = "Chua duyet";
            this.lblValNgayDuyet.Font = ThemeHelper.FontBodyBold;
            this.lblValNgayDuyet.ForeColor = ThemeHelper.Text;
            this.lblValNgayDuyet.AutoSize = true;
            this.lblValNgayDuyet.Margin = new System.Windows.Forms.Padding(0, 0, 0, 6);
            this.lblValNgayDuyet.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.tblInfo.Controls.Add(this.lblValNgayDuyet, 3, 2);

            // 
            // lblSection
            // 
            this.lblSection.Text = "CHI TIẾT MẶT HÀNG";
            this.lblSection.Font = ThemeHelper.FontCaptionBold;
            this.lblSection.ForeColor = ThemeHelper.TextSecondary;
            this.lblSection.AutoSize = true;
            this.lblSection.Margin = new System.Windows.Forms.Padding(0, 4, 0, 6);
            this.root.Controls.Add(this.lblSection, 0, 1);

            // 
            // cardGrid
            // 
            this.cardGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cardGrid.BorderRadius = 12;
            this.cardGrid.FillColor = System.Drawing.Color.White;
            this.cardGrid.Padding = new System.Windows.Forms.Padding(12);
            this.cardGrid.Margin = new System.Windows.Forms.Padding(0, 0, 0, 12);
            this.cardGrid.ShadowDecoration.Enabled = true;
            this.root.Controls.Add(this.cardGrid, 0, 2);

            // 
            // dgvDetails
            // 
            this.dgvDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvDetails.AutoGenerateColumns = false;
            this.cardGrid.Controls.Add(this.dgvDetails);

            // columns
            var colName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            colName.HeaderText = "Tên sản phẩm";
            colName.DataPropertyName = "TenSanPham";
            colName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            colName.FillWeight = 50;
            colName.ReadOnly = true;
            this.dgvDetails.Columns.Add(colName);

            var colPrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            colPrice.HeaderText = "Đơn giá";
            colPrice.DataPropertyName = "DonGiaText";
            colPrice.Width = 110;
            colPrice.ReadOnly = true;
            colPrice.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            colPrice.HeaderCell.Style.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.dgvDetails.Columns.Add(colPrice);

            var colQty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            colQty.HeaderText = "SL đặt";
            colQty.DataPropertyName = "SoLuong";
            colQty.Width = 80;
            colQty.ReadOnly = true;
            colQty.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            colQty.HeaderCell.Style.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dgvDetails.Columns.Add(colQty);

            var colTotal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            colTotal.HeaderText = "Thành tiền";
            colTotal.DataPropertyName = "ThanhTienText";
            colTotal.Width = 120;
            colTotal.ReadOnly = true;
            colTotal.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            colTotal.HeaderCell.Style.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.dgvDetails.Columns.Add(colTotal);

            // 
            // pnlFooter
            // 
            this.pnlFooter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlFooter.Height = 46;
            this.pnlFooter.Margin = new System.Windows.Forms.Padding(0);
            this.root.Controls.Add(this.pnlFooter, 0, 3);

            // 
            // lblTotal
            // 
            this.lblTotal.Text = "TỔNG TIỀN: 0 VND";
            this.lblTotal.Font = ThemeHelper.FontH2;
            this.lblTotal.ForeColor = ThemeHelper.Danger;
            this.lblTotal.AutoSize = true;
            this.lblTotal.Location = new System.Drawing.Point(0, 10);
            this.pnlFooter.Controls.Add(this.lblTotal);

            // 
            // btnClose
            // 
            this.btnClose.Text = "Đóng";
            this.btnClose.Location = new System.Drawing.Point(600, 4);
            this.btnClose.Size = new System.Drawing.Size(120, 38);
            this.btnClose.BorderRadius = 19;
            this.btnClose.FillColor = ThemeHelper.Primary;
            this.btnClose.Font = ThemeHelper.FontBodyBold;
            this.btnClose.ForeColor = System.Drawing.Color.White;
            this.btnClose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pnlFooter.Controls.Add(this.btnClose);

            // FORM properties
            this.MinimumSize = new System.Drawing.Size(700, 540);
            this.Size = new System.Drawing.Size(760, 580);
            this.BackColor = ThemeHelper.BackgroundApp;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel root;
        private Guna.UI2.WinForms.Guna2Panel cardInfo;
        private System.Windows.Forms.TableLayoutPanel tblInfo;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblNgayDat;
        private System.Windows.Forms.Label lblValNgayDat;
        private System.Windows.Forms.Label lblTrangThai;
        private System.Windows.Forms.Label lblValTrangThai;
        private System.Windows.Forms.Label lblKhachHang;
        private System.Windows.Forms.Label lblValKhachHang;
        private System.Windows.Forms.Label lblNgayDuyet;
        private System.Windows.Forms.Label lblValNgayDuyet;
        private System.Windows.Forms.Label lblSection;
        private Guna.UI2.WinForms.Guna2Panel cardGrid;
        private System.Windows.Forms.DataGridView dgvDetails;
        private System.Windows.Forms.Panel pnlFooter;
        private System.Windows.Forms.Label lblTotal;
        private Guna.UI2.WinForms.Guna2Button btnClose;
    }
}
