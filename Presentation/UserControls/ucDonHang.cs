using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using QuanLyCuaHangTapHoa.Application.Interfaces;
using QuanLyCuaHangTapHoa.Domain;
using QuanLyCuaHangTapHoa.Presentation.Modals;

namespace QuanLyCuaHangTapHoa.Presentation.UserControls
{
    public partial class ucDonHang : UserControl
    {
        private readonly IOrderUseCase _orderUseCase;
        private readonly IProductUseCase _productUseCase;
        private readonly TaiKhoan _currentUser;

        // Parameterless constructor for Visual Studio Designer
        public ucDonHang()
        {
            InitializeComponent();
            SetupEvents();
            SetupData();
        }

        public ucDonHang(IOrderUseCase orderUseCase, IProductUseCase productUseCase, TaiKhoan currentUser)
        {
            _orderUseCase = orderUseCase;
            _productUseCase = productUseCase;
            _currentUser = currentUser;

            InitializeComponent();
            SetupEvents();
            SetupData();
            LoadData();
        }

        private void SetupEvents()
        {
            btnSearch.Click += (s, e) => LoadData();
        }

        private void SetupData()
        {
            bool isStaff = _currentUser != null && (_currentUser.NguoiDung is NhanVien || _currentUser.NguoiDung is Admin);

            flowHeaderActions.Controls.Clear();
            if (isStaff)
            {
                flowHeaderActions.Controls.Add(btnCleanExpired);

                if (!dgvOrders.Columns.Contains("colApprove"))
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
                }

                if (!dgvOrders.Columns.Contains("colReject"))
                {
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
            else if (_currentUser != null && _currentUser.NguoiDung is KhachHang)
            {
                flowHeaderActions.Controls.Add(btnAddNew);
            }
            else
            {
                flowHeaderActions.Controls.Add(btnCleanExpired);
                flowHeaderActions.Controls.Add(btnAddNew);
            }
        }

        private void LoadData()
        {
            if (_orderUseCase == null || _currentUser == null) return;
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
            if (_orderUseCase == null) return;
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
            if (e.RowIndex < 0 || _orderUseCase == null) return;

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
