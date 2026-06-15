using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Guna.UI2.WinForms;
using QuanLyCuaHangTapHoa.Domain;

namespace QuanLyCuaHangTapHoa.Presentation.Modals
{
    public class FormOrderDetails : Form
    {
        private readonly DonDatHang _order;
        private DataGridView dgvDetails;
        private Label lblTotal;
        private Guna2Button btnClose;

        public FormOrderDetails(DonDatHang order)
        {
            _order = order;
            InitializeComponent();
            LoadDetails();
        }

        private void InitializeComponent()
        {
            this.MinimumSize = new Size(700, 540);
            this.Size        = new Size(760, 580);
            this.BackColor   = ThemeHelper.BackgroundApp;
            this.FormBorderStyle = FormBorderStyle.None;
            this.StartPosition   = FormStartPosition.CenterParent;

            // ═══════════════════════════════════════════════════════
            //  ROOT: TableLayoutPanel  1 col × 4 rows
            //  Row 0 — Info card (AutoSize)
            //  Row 1 — Section label (AutoSize)
            //  Row 2 — Grid card (Fill 100%)
            //  Row 3 — Footer: tổng + nút Đóng (AutoSize)
            // ═══════════════════════════════════════════════════════
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

            // Title spanning all 4 columns
            var lblTitle = new Label
            {
                Text      = $"CHI TIẾT ĐƠN HÀNG: {_order.MaDon}",
                Font      = ThemeHelper.FontSubheading,
                ForeColor = ThemeHelper.Primary,
                AutoSize  = true,
                Margin    = new Padding(0, 0, 0, 10)
            };
            tblInfo.Controls.Add(lblTitle, 0, 0);
            tblInfo.SetColumnSpan(lblTitle, 4);

            string trangThai = _order.TrangThaiDonHang switch
            {
                TrangThaiDonHang.ChoDuyet    => "🟡 Chờ duyệt",
                TrangThaiDonHang.DaDuyet     => "🟢 Đã duyệt",
                TrangThaiDonHang.DaThanhToan => "🔵 Đã thanh toán",
                TrangThaiDonHang.TuChoi      => "🔴 Từ chối",
                TrangThaiDonHang.DaHuy       => "⚫ Đã hủy",
                _                            => _order.TrangThaiDonHang.ToString()
            };

            // Row 1: Mã đơn | value | Trạng thái | value
            AddInfoRow(tblInfo, 1, "Ngày đặt:", _order.NgayDat.ToString("dd/MM/yyyy HH:mm"),
                                   "Trạng thái:", trangThai);
            // Row 2: Khách hàng | value | Ngày duyệt | value
            AddInfoRow(tblInfo, 2,
                "Khách hàng:", _order.KhachHang?.HoTen ?? "—",
                "Ngày duyệt:", _order.NgayDuyet.HasValue
                    ? _order.NgayDuyet.Value.ToString("dd/MM/yyyy HH:mm") : "Chưa duyệt");

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
                FillWeight       = 45,
                MinimumWidth     = 140
            });
            dgvDetails.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name             = "colDonGia",
                HeaderText       = "Đơn giá",
                DataPropertyName = "DonGiaText",
                Width            = 110,
                DefaultCellStyle = { Alignment = DataGridViewContentAlignment.MiddleRight },
                HeaderCell       = { Style = { Alignment = DataGridViewContentAlignment.MiddleRight } },
                Resizable        = DataGridViewTriState.False
            });
            dgvDetails.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name             = "colSoLuong",
                HeaderText       = "SL",
                DataPropertyName = "SoLuong",
                Width            = 60,
                DefaultCellStyle = { Alignment = DataGridViewContentAlignment.MiddleCenter },
                HeaderCell       = { Style = { Alignment = DataGridViewContentAlignment.MiddleCenter } },
                Resizable        = DataGridViewTriState.False
            });
            dgvDetails.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name             = "colThanhTien",
                HeaderText       = "Thành tiền",
                DataPropertyName = "ThanhTienText",
                Width            = 120,
                DefaultCellStyle = { Alignment = DataGridViewContentAlignment.MiddleRight },
                HeaderCell       = { Style = { Alignment = DataGridViewContentAlignment.MiddleRight } },
                Resizable        = DataGridViewTriState.False
            });

            // ── ROW 3: Footer (tổng + nút Đóng) ─────────────────────
            var pnlFooter = new TableLayoutPanel
            {
                Dock        = DockStyle.Fill,
                ColumnCount = 2,
                RowCount    = 1,
                Margin      = new Padding(0),
                BackColor   = Color.Transparent
            };
            pnlFooter.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            pnlFooter.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
            pnlFooter.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            root.Controls.Add(pnlFooter, 0, 3);

            lblTotal = new Label
            {
                Text      = "TỔNG CỘNG: 0 đ",
                Font      = new Font("Segoe UI", 12f, FontStyle.Bold),
                ForeColor = ThemeHelper.Danger,
                AutoSize  = true,
                Anchor    = AnchorStyles.Left | AnchorStyles.Top,
                Margin    = new Padding(0, 6, 0, 0)
            };
            pnlFooter.Controls.Add(lblTotal, 0, 0);

            btnClose = new Guna2Button
            {
                Text         = "Đóng lại",
                Size         = new Size(120, 40),
                BorderRadius = 20,
                FillColor    = ThemeHelper.Primary,
                HoverState   = { FillColor = ThemeHelper.PrimaryHover },
                Font         = ThemeHelper.FontBodyBold,
                ForeColor    = Color.White,
                Cursor       = Cursors.Hand
            };
            btnClose.Click += (s, e) => this.Close();
            pnlFooter.Controls.Add(btnClose, 1, 0);
        }

        // Helper: add a pair of (label, value) × 2 in one row
        private static void AddInfoRow(TableLayoutPanel tbl, int row,
            string lbl1, string val1, string lbl2, string val2)
        {
            var makeLabel = (string t, bool isKey) => new Label
            {
                Text      = t,
                Font      = isKey ? ThemeHelper.FontCaptionBold : ThemeHelper.FontBody,
                ForeColor = isKey ? ThemeHelper.TextSecondary : ThemeHelper.Text,
                AutoSize  = true,
                Margin    = new Padding(0, 4, 8, 4),
                Anchor    = AnchorStyles.Left | AnchorStyles.Top
            };

            tbl.Controls.Add(makeLabel(lbl1, true),  0, row);
            tbl.Controls.Add(makeLabel(val1, false), 1, row);
            tbl.Controls.Add(makeLabel(lbl2, true),  2, row);
            tbl.Controls.Add(makeLabel(val2, false), 3, row);
        }

        private void LoadDetails()
        {
            try
            {
                var rows = _order.ChiTietDonHangs.Select(ct => new
                {
                    TenSanPham  = ct.SanPham?.TenSP ?? ct.MaSP,
                    SoLuong     = ct.SoLuong,
                    DonGiaText  = ct.DonGia.ToString("#,##0") + " đ",
                    ThanhTienText = (ct.SoLuong * ct.DonGia).ToString("#,##0") + " đ",
                    Raw          = ct.SoLuong * ct.DonGia
                }).ToList();

                dgvDetails.DataSource = null;
                dgvDetails.DataSource = rows;

                decimal sum = rows.Sum(r => r.Raw);
                lblTotal.Text = $"TỔNG GIÁ TRỊ ĐƠN HÀNG: {sum:#,##0} đ";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải chi tiết đơn hàng: " + ex.Message);
            }
        }
    }
}
