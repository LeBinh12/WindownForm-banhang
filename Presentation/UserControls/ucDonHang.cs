using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Guna.UI2.WinForms;
using QuanLyCuaHangTapHoa.Application.Interfaces;
using QuanLyCuaHangTapHoa.Domain;
using QuanLyCuaHangTapHoa.Presentation.Modals;

namespace QuanLyCuaHangTapHoa.Presentation.UserControls
{
    public class ucDonHang : UserControl
    {
        private readonly IOrderUseCase _orderUseCase;
        private readonly IProductUseCase _productUseCase;
        private readonly TaiKhoan _currentUser;

        // UI Controls
        private TableLayoutPanel tblRoot;
        private TableLayoutPanel tblHeader;
        private FlowLayoutPanel flowHeaderActions;
        private TableLayoutPanel tblFilter;
        private Guna2Panel cardGrid;

        private DataGridView dgvOrders;
        private Guna2TextBox txtSearch;
        private Guna2Button btnSearch;
        private Guna2Button btnReset;
        private Guna2Button btnAddNew;
        private Guna2Button btnCleanExpired;

        public ucDonHang(IOrderUseCase orderUseCase, IProductUseCase productUseCase, TaiKhoan currentUser)
        {
            _orderUseCase = orderUseCase;
            _productUseCase = productUseCase;
            _currentUser = currentUser;

            InitializeComponent();
            LoadData();
        }

        private void InitializeComponent()
        {
            this.Dock = DockStyle.Fill;
            this.BackColor = ThemeHelper.BackgroundApp;

            // 1. Root TableLayoutPanel (1 Column x 3 Rows, Padding = 20)
            tblRoot = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 1,
                RowCount = 3,
                Padding = new Padding(20),
                BackColor = Color.Transparent
            };
            tblRoot.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tblRoot.RowStyles.Add(new RowStyle(SizeType.AutoSize)); // Row 0: Header
            tblRoot.RowStyles.Add(new RowStyle(SizeType.AutoSize)); // Row 1: FilterBar
            tblRoot.RowStyles.Add(new RowStyle(SizeType.Percent, 100F)); // Row 2: Card Grid
            this.Controls.Add(tblRoot);

            // ==================== ROW 0: HEADER (TableLayoutPanel 2 Cols) ====================
            tblHeader = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 2,
                RowCount = 1,
                Margin = new Padding(0, 0, 0, 12),
                Padding = new Padding(0)
            };
            tblHeader.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F)); // Col 0: Title
            tblHeader.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));    // Col 1: Action Buttons Flow
            tblHeader.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tblRoot.Controls.Add(tblHeader, 0, 0);

            var lblTitle = new Label
            {
                Text = "YÊU CẦU ĐẶT GIỮ HÀNG",
                Font = ThemeHelper.FontH2,
                ForeColor = ThemeHelper.Primary,
                Anchor = AnchorStyles.Left,
                AutoSize = true,
                TextAlign = ContentAlignment.MiddleLeft
            };
            tblHeader.Controls.Add(lblTitle, 0, 0);

            // Header actions layout
            flowHeaderActions = new FlowLayoutPanel
            {
                FlowDirection = FlowDirection.RightToLeft,
                WrapContents = false,
                AutoSize = true,
                Anchor = AnchorStyles.Right,
                BackColor = Color.Transparent,
                Margin = new Padding(0)
            };
            tblHeader.Controls.Add(flowHeaderActions, 1, 0);

            bool isStaff = (_currentUser.NguoiDung is NhanVien || _currentUser.NguoiDung is Admin);

            if (isStaff)
            {
                btnCleanExpired = new Guna2Button
                {
                    Text = "Thu hồi đơn hết hạn",
                    AutoSize = true,
                    Padding = new Padding(12, 6, 12, 6),
                    BorderRadius = 20,
                    FillColor = ThemeHelper.Warning,
                    HoverState = { FillColor = Color.FromArgb(217, 119, 6) },
                    Font = ThemeHelper.FontBodyBold,
                    ForeColor = Color.White,
                    Cursor = Cursors.Hand,
                    Margin = new Padding(8, 0, 0, 0)
                };
                btnCleanExpired.Click += BtnCleanExpired_Click;
                flowHeaderActions.Controls.Add(btnCleanExpired);
            }
            else if (_currentUser.NguoiDung is KhachHang)
            {
                btnAddNew = new Guna2Button
                {
                    Text = "+ Tạo yêu cầu đặt giữ",
                    AutoSize = true,
                    Padding = new Padding(12, 6, 12, 6),
                    BorderRadius = 20,
                    FillColor = ThemeHelper.Success,
                    HoverState = { FillColor = Color.FromArgb(4, 120, 87) },
                    Font = ThemeHelper.FontBodyBold,
                    ForeColor = Color.White,
                    Cursor = Cursors.Hand,
                    Margin = new Padding(8, 0, 0, 0)
                };
                btnAddNew.Click += BtnAddNew_Click;
                flowHeaderActions.Controls.Add(btnAddNew);
            }

            // ==================== ROW 1: FILTERBAR (TableLayoutPanel 4 Cols) ====================
            // NOTE: Status filter removed — this screen only shows ChoDuyet orders
            tblFilter = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 4,
                RowCount = 1,
                Margin = new Padding(0, 0, 0, 16),
                Padding = new Padding(0),
                Height = 44
            };
            tblFilter.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 280F)); // Col 0: Search Input
            tblFilter.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));       // Col 1: Search Button
            tblFilter.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));       // Col 2: Reset Button
            tblFilter.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));  // Col 3: Spacer
            tblFilter.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tblRoot.Controls.Add(tblFilter, 0, 1);

            txtSearch = new Guna2TextBox
            {
                Dock = DockStyle.Fill,
                BorderRadius = 8,
                BorderColor = ThemeHelper.Border,
                Font = ThemeHelper.FontBody,
                ForeColor = ThemeHelper.Text,
                PlaceholderText = "Mã đơn hoặc tên khách...",
                Margin = new Padding(0, 0, 12, 0)
            };
            tblFilter.Controls.Add(txtSearch, 0, 0);

            btnSearch = new Guna2Button
            {
                Text = "Tìm lọc",
                Size = new Size(110, 36),
                BorderRadius = 18,
                FillColor = ThemeHelper.Primary,
                HoverState = { FillColor = ThemeHelper.PrimaryHover },
                Font = ThemeHelper.FontBodyBold,
                ForeColor = Color.White,
                Cursor = Cursors.Hand,
                Margin = new Padding(0, 0, 12, 0)
            };
            btnSearch.Click += (s, e) => LoadData();
            tblFilter.Controls.Add(btnSearch, 1, 0);

            btnReset = new Guna2Button
            {
                Text = "Xóa lọc",
                Size = new Size(110, 36),
                BorderRadius = 18,
                FillColor = ThemeHelper.BorderLight,
                HoverState = { FillColor = ThemeHelper.Border },
                Font = ThemeHelper.FontBodyBold,
                ForeColor = ThemeHelper.TextSecondary,
                Cursor = Cursors.Hand,
                Margin = new Padding(0, 0, 0, 0)
            };
            btnReset.Click += BtnReset_Click;
            tblFilter.Controls.Add(btnReset, 2, 0);

            // ==================== ROW 2: GRID CARD ====================
            cardGrid = new Guna2Panel
            {
                Dock = DockStyle.Fill,
                BorderRadius = 12,
                FillColor = Color.White,
                Padding = new Padding(16),
                Margin = new Padding(0)
            };
            cardGrid.ShadowDecoration.Enabled = true;
            tblRoot.Controls.Add(cardGrid, 0, 2);

            dgvOrders = new DataGridView
            {
                Dock = DockStyle.Fill,
                AutoGenerateColumns = false,
                RowHeadersVisible = false,
                AllowUserToResizeRows = false,
                AllowUserToAddRows = false
            };
            ThemeHelper.StyleFlatDataGrid(dgvOrders);
            dgvOrders.CellClick += DgvOrders_CellClick;
            cardGrid.Controls.Add(dgvOrders);

            // Setup explicit grid columns
            dgvOrders.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Mã Đơn", DataPropertyName = "MaDon", Width = 110, ReadOnly = true });
            dgvOrders.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Khách hàng", DataPropertyName = "KhachHang", AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill, FillWeight = 35, ReadOnly = true });
            dgvOrders.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Ngày đặt", DataPropertyName = "NgayDat", Width = 140, ReadOnly = true });
            dgvOrders.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Ngày duyệt", DataPropertyName = "NgayDuyet", Width = 140, ReadOnly = true });
            dgvOrders.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Trạng thái", DataPropertyName = "TrangThaiText", Width = 145, ReadOnly = true });

            // Action columns
            var colView = new DataGridViewButtonColumn
            {
                Name = "colView",
                HeaderText = "Chi tiết",
                Text = "Xem",
                UseColumnTextForButtonValue = true,
                Width = 70,
                FlatStyle = FlatStyle.Flat,
                Resizable = DataGridViewTriState.False
            };
            colView.DefaultCellStyle.Font = new Font("Segoe UI", 9f);
            dgvOrders.Columns.Add(colView);

            if (isStaff)
            {
                var colApprove = new DataGridViewButtonColumn
                {
                    Name = "colApprove",
                    HeaderText = "Duyệt",
                    Text = "Duyệt",
                    UseColumnTextForButtonValue = true,
                    Width = 70,
                    FlatStyle = FlatStyle.Flat,
                    Resizable = DataGridViewTriState.False
                };
                colApprove.DefaultCellStyle.Font = new Font("Segoe UI", 9f);
                dgvOrders.Columns.Add(colApprove);

                var colReject = new DataGridViewButtonColumn
                {
                    Name = "colReject",
                    HeaderText = "Hủy đơn",
                    Text = "Hủy đơn",
                    UseColumnTextForButtonValue = true,
                    Width = 110,
                    FlatStyle = FlatStyle.Flat,
                    Resizable = DataGridViewTriState.False
                };
                colReject.DefaultCellStyle.Font = new Font("Segoe UI", 9f);
                dgvOrders.Columns.Add(colReject);
            }
        }

        private void LoadData()
        {
            try
            {
                // LUỒNG A: Màn hình này CHỈ hiển thị đơn đặt giữ (ChoDuyet)
                // LUỒNG B (POS bán lẻ) tạo đơn với TrangThaiDonHang = DaThanhToan => không bao giờ xuất hiện ở đây
                List<DonDatHang> orders;
                if (_currentUser.NguoiDung is KhachHang)
                {
                    orders = _orderUseCase.GetOrdersByCustomer(_currentUser.MaNguoiDung)
                        .Where(o => o.TrangThaiDonHang == TrangThaiDonHang.ChoDuyet)
                        .ToList();
                }
                else
                {
                    orders = _orderUseCase.GetByStatus(TrangThaiDonHang.ChoDuyet);
                }

                // Text search filter only (no status filter — this screen is always ChoDuyet)
                string q = txtSearch.Text.Trim().ToLower();
                var filtered = orders.Where(o =>
                    string.IsNullOrEmpty(q) ||
                    o.MaDon.ToLower().Contains(q) ||
                    (o.KhachHang?.HoTen ?? "").ToLower().Contains(q)
                ).OrderByDescending(o => o.NgayDat).ToList();

                var viewList = filtered.Select(o => new DonHangViewModel
                {
                    MaDon = o.MaDon,
                    KhachHang = o.KhachHang?.HoTen ?? "Khách lẻ",
                    NgayDat = o.NgayDat.ToString("dd/MM/yyyy HH:mm"),
                    NgayDuyet = o.NgayDuyet?.ToString("dd/MM/yyyy HH:mm") ?? "Chưa duyệt",
                    TrangThaiText = MapTrangThaiText(o.TrangThaiDonHang),
                    RawOrder = o
                }).ToList();

                dgvOrders.DataSource = null;
                dgvOrders.DataSource = viewList;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải đơn đặt hàng: " + ex.Message);
            }
        }

        private string MapTrangThaiText(TrangThaiDonHang status)
        {
            return status switch
            {
                TrangThaiDonHang.ChoDuyet => "🟡 Chờ duyệt",
                TrangThaiDonHang.DaDuyet => "🟢 Đã duyệt",
                TrangThaiDonHang.DaThanhToan => "🔵 Đã thanh toán",
                TrangThaiDonHang.TuChoi => "🔴 Từ chối",
                TrangThaiDonHang.DaHuy => "⚫ Đã hủy",
                _ => status.ToString()
            };
        }

        private void BtnReset_Click(object sender, EventArgs e)
        {
            txtSearch.Clear();
            LoadData();
        }

        private void BtnAddNew_Click(object sender, EventArgs e)
        {
            Form parent = this.FindForm();
            using (var form = new FormCustomerCart(_productUseCase, _orderUseCase, _currentUser))
            {
                var result = ThemeHelper.ShowCustomDialog(parent, form);
                if (result == DialogResult.OK)
                {
                    LoadData();
                }
            }
        }

        private void BtnCleanExpired_Click(object sender, EventArgs e)
        {
            Form parent = this.FindForm();
            bool confirm = ThemeHelper.ShowConfirmDialog(parent, "Thu hồi đơn hết hạn", "Xác nhận kiểm tra và thu hồi các đơn đặt giữ quá 3 ngày chưa thanh toán?");
            if (confirm)
            {
                try
                {
                    _orderUseCase.CheckAndCancelExpiredOrders();
                    Toast.Show("Thu hồi đơn hàng quá hạn thành công!", "success");
                    LoadData();
                }
                catch (Exception ex)
                {
                    Toast.Show(ex.Message, "danger");
                }
            }
        }

        private void DgvOrders_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            var selectedItem = dgvOrders.Rows[e.RowIndex].DataBoundItem as DonHangViewModel;
            if (selectedItem == null) return;

            // VIEW DETAILS
            if (dgvOrders.Columns[e.ColumnIndex].Name == "colView")
            {
                Form parent = this.FindForm();
                using (var form = new FormOrderDetails(selectedItem.RawOrder))
                {
                    ThemeHelper.ShowCustomDialog(parent, form);
                }
            }

            // APPROVE ACTION — lấy lại trạng thái mới nhất từ DB trước khi duyệt
            if (dgvOrders.Columns[e.ColumnIndex].Name == "colApprove")
            {
                // Refresh từ DB để tránh dữ liệu cũ (stale cache)
                var don = _orderUseCase.GetById(selectedItem.MaDon);
                if (don == null)
                {
                    Toast.Show($"Không tìm thấy đơn hàng {selectedItem.MaDon}.", "danger");
                    LoadData();
                    return;
                }
                if (don.TrangThaiDonHang != TrangThaiDonHang.ChoDuyet)
                {
                    Toast.Show($"Đơn {don.MaDon} đang ở trạng thái [{MapTrangThaiText(don.TrangThaiDonHang)}], không thể duyệt.", "warning");
                    LoadData();
                    return;
                }

                Form parent = this.FindForm();
                bool confirm = ThemeHelper.ShowConfirmDialog(parent, "Duyệt Đơn Hàng", $"Xác nhận phê duyệt đơn hàng {don.MaDon}?");
                if (confirm)
                {
                    try
                    {
                        _orderUseCase.ApproveOrder(don.MaDon);
                        Toast.Show($"Phê duyệt đơn {don.MaDon} thành công!", "success");
                        LoadData();
                    }
                    catch (Exception ex)
                    {
                        Toast.Show(ex.Message, "danger");
                    }
                }
            }

            // REJECT ACTION — lấy lại trạng thái mới nhất từ DB trước khi hủy
            if (dgvOrders.Columns[e.ColumnIndex].Name == "colReject")
            {
                // Refresh từ DB để tránh dữ liệu cũ (stale cache)
                var don = _orderUseCase.GetById(selectedItem.MaDon);
                if (don == null)
                {
                    Toast.Show($"Không tìm thấy đơn hàng {selectedItem.MaDon}.", "danger");
                    LoadData();
                    return;
                }
                if (don.TrangThaiDonHang != TrangThaiDonHang.ChoDuyet && don.TrangThaiDonHang != TrangThaiDonHang.DaDuyet)
                {
                    Toast.Show($"Đơn {don.MaDon} đang ở trạng thái [{MapTrangThaiText(don.TrangThaiDonHang)}], không thể hủy.", "warning");
                    LoadData();
                    return;
                }

                Form parent = this.FindForm();
                bool confirm = ThemeHelper.ShowConfirmDialog(parent, "Hủy / Từ Chối Đơn", $"Xác nhận từ chối hoặc hủy bỏ đơn đặt giữ {don.MaDon}?");
                if (confirm)
                {
                    try
                    {
                        _orderUseCase.CancelOrRejectOrder(don.MaDon, isReject: true);
                        Toast.Show($"Đã từ chối đơn {don.MaDon} thành công!", "warning");
                        LoadData();
                    }
                    catch (Exception ex)
                    {
                        Toast.Show(ex.Message, "danger");
                    }
                }
            }
        }

        private class DonHangViewModel
        {
            public string MaDon { get; set; }
            public string KhachHang { get; set; }
            public string NgayDat { get; set; }
            public string NgayDuyet { get; set; }
            public string TrangThaiText { get; set; }
            public DonDatHang RawOrder { get; set; }
        }
    }
}
