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
    public partial class ucThanhToan : UserControl
    {
        private readonly IInvoiceUseCase _invoiceUseCase;
        private readonly IOrderUseCase _orderUseCase;
        private readonly IProductUseCase _productUseCase;
        private readonly TaiKhoan _currentUser;

        // POS cart
        private List<(SanPham product, int quantity)> _posCart = new List<(SanPham, int)>();

        // Parameterless constructor for Visual Studio Designer
        public ucThanhToan()
        {
            InitializeComponent();
            SetupEvents();
        }

        public ucThanhToan(
            IInvoiceUseCase invoiceUseCase,
            IOrderUseCase orderUseCase,
            IProductUseCase productUseCase,
            TaiKhoan currentUser)
        {
            _invoiceUseCase = invoiceUseCase;
            _orderUseCase = orderUseCase;
            _productUseCase = productUseCase;
            _currentUser = currentUser;

            InitializeComponent();
            SetupEvents();
            LoadPOSProducts();
            LoadApprovedOrders();
        }

        private void SetupEvents()
        {
            dgvPosCart.DataError += (s, e) => { e.ThrowException = false; };
            dgvInvoiceDetails.DataError += (s, e) => { e.ThrowException = false; };
            btnSearchHistory.Click += (s, e) => LoadInvoiceHistory();
            dgvHistoryInvoices.DataError += (s, e) => { e.ThrowException = false; };
            tabThanhToan.SelectedIndexChanged += (s, e) =>
            {
                if (tabThanhToan.SelectedTab == tabHistory)
                {
                    LoadInvoiceHistory();
                }
            };
        }

        private void LoadPOSProducts()
        {
            if (_productUseCase == null) return;
            try
            {
                var list = _productUseCase.SearchProducts("", "Tất cả")
                    .Where(p => p.TrangThaiSanPham == TrangThaiSanPham.SanSang && p.SoLuongTon > 0)
                    .ToList();
                cbPosProducts.DataSource = null;
                cbPosProducts.DataSource = list;
                cbPosProducts.DisplayMember = "TenSP";
                cbPosProducts.ValueMember = "MaSP";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải sản phẩm POS: " + ex.Message);
            }
        }

        private void LoadApprovedOrders()
        {
            if (_orderUseCase == null) return;
            try
            {
                var list = _orderUseCase.GetAllOrders()
                    .Where(o => o.TrangThaiDonHang == TrangThaiDonHang.DaDuyet)
                    .ToList();
                cbReservedOrders.DataSource = null;
                cbReservedOrders.DataSource = list;
                cbReservedOrders.DisplayMember = "MaDon";
                cbReservedOrders.ValueMember = "MaDon";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải đơn hàng: " + ex.Message);
            }
        }

        private void LoadInvoiceHistory()
        {
            if (_invoiceUseCase == null) return;
            try
            {
                var list = _invoiceUseCase.GetAllInvoices();
                string q = txtSearchHistory.Text.Trim().ToLower();
                if (!string.IsNullOrEmpty(q))
                {
                    list = list.Where(h => h.MaHoaDon.ToLower().Contains(q) || h.MaDon.ToLower().Contains(q)).ToList();
                }

                var display = list.Select(h => new {
                    MaHoaDon = h.MaHoaDon,
                    NgayLap = h.NgayLap.ToString("dd/MM/yyyy HH:mm"),
                    TongTienText = h.TongTien.ToString("#,##0") + " đ",
                    MaDon = h.MaDon,
                    NhanVien = h.NhanVien?.HoTen ?? h.MaNguoiDungNhanVien
                }).ToList();

                dgvHistoryInvoices.DataSource = null;
                dgvHistoryInvoices.DataSource = display;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải lịch sử hóa đơn: " + ex.Message);
            }
        }

        private void BtnPosAdd_Click(object sender, EventArgs e)
        {
            var selected = cbPosProducts.SelectedItem as SanPham;
            if (selected == null) return;

            int qty = (int)numPosQty.Value;

            var existingIndex = _posCart.FindIndex(x => x.product.MaSP == selected.MaSP);
            if (existingIndex >= 0)
            {
                var current = _posCart[existingIndex];
                if (current.product.SoLuongTon < current.quantity + qty)
                {
                    Toast.Show("Hàng trong kho không đủ để bán!", "warning");
                    return;
                }
                _posCart[existingIndex] = (current.product, current.quantity + qty);
            }
            else
            {
                if (selected.SoLuongTon < qty)
                {
                    Toast.Show("Hàng trong kho không đủ để bán!", "warning");
                    return;
                }
                _posCart.Add((selected, qty));
            }

            UpdatePosCartUI();
        }

        private void DgvPosCart_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            if (dgvPosCart.Columns[e.ColumnIndex].Name == "colPosRemove")
            {
                _posCart.RemoveAt(e.RowIndex);
                UpdatePosCartUI();
            }
        }

        private void UpdatePosCartUI()
        {
            var display = _posCart.Select(x => new
            {
                MaSP = x.product.MaSP,
                TenSP = x.product.TenSP,
                SoLuong = x.quantity,
                DonGiaText = x.product.DonGia.ToString("#,##0") + " đ",
                ThanhTienText = (x.quantity * x.product.DonGia).ToString("#,##0") + " đ"
            }).ToList();

            dgvPosCart.DataSource = null;
            dgvPosCart.DataSource = display;

            decimal total = _posCart.Sum(x => x.quantity * x.product.DonGia);
            lblPosTotal.Text = $"TỔNG THANH TOÁN: {total:#,##0} VND";
        }

        private void BtnPosClear_Click(object sender, EventArgs e)
        {
            _posCart.Clear();
            UpdatePosCartUI();
        }

        private void BtnPosCheckout_Click(object sender, EventArgs e)
        {
            if (_invoiceUseCase == null || _currentUser == null) return;
            try
            {
                if (!_posCart.Any())
                {
                    Toast.Show("Giỏ hàng POS trống!", "warning");
                    return;
                }

                var items = _posCart.Select(x => (x.product.MaSP, x.quantity)).ToList();
                var invoice = _invoiceUseCase.CreatePOSInvoice(_currentUser.MaNguoiDung, items);

                Toast.Show($"Thanh toán thành công! Mã HĐ: {invoice.MaHoaDon}", "success");

                _posCart.Clear();
                UpdatePosCartUI();
                LoadPOSProducts();
            }
            catch (Exception ex)
            {
                Toast.Show(ex.Message, "danger");
            }
        }

        private void CbReservedOrders_SelectedIndexChanged(object sender, EventArgs e)
        {
            var selectedOrder = cbReservedOrders.SelectedItem as DonDatHang;
            if (selectedOrder == null)
            {
                lblReservedDetails.Text = "Chi tiết đơn hàng:\nChưa chọn đơn.";
                return;
            }

            decimal total = selectedOrder.ChiTietDonHangs.Sum(x => x.SoLuong * x.DonGia);
            string details = $"Mã Đơn: {selectedOrder.MaDon}\n" +
                             $"Khách hàng: {selectedOrder.KhachHang?.HoTen ?? "Khách lẻ"}\n" +
                             $"Ngày đặt: {selectedOrder.NgayDat:dd/MM/yyyy HH:mm}\n" +
                             $"Mặt hàng: {selectedOrder.ChiTietDonHangs.Count}\n" +
                             $"Cần thu: {total:#,##0} VND";

            lblReservedDetails.Text = details;
        }

        private void BtnPayReserved_Click(object sender, EventArgs e)
        {
            if (_invoiceUseCase == null || _currentUser == null) return;
            try
            {
                var selectedOrder = cbReservedOrders.SelectedItem as DonDatHang;
                if (selectedOrder == null)
                {
                    Toast.Show("Vui lòng chọn đơn hàng!", "warning");
                    return;
                }

                var invoice = _invoiceUseCase.CreateInvoice(selectedOrder.MaDon, _currentUser.MaNguoiDung);
                Toast.Show($"Thanh toán xuất hóa đơn {invoice.MaHoaDon} thành công!", "success");

                LoadApprovedOrders();
                LoadPOSProducts();
            }
            catch (Exception ex)
            {
                Toast.Show("Thanh toán đơn thất bại: " + ex.Message, "danger");
            }
        }

        private void BtnSearchInvoice_Click(object sender, EventArgs e)
        {
            if (_invoiceUseCase == null) return;
            try
            {
                string invId = txtSearchInvoiceId.Text.Trim();
                if (string.IsNullOrEmpty(invId)) return;

                var invoice = _invoiceUseCase.GetInvoiceById(invId);
                if (invoice == null)
                {
                    Toast.Show("Không tìm thấy hóa đơn này!", "warning");
                    lblInvoiceDate.Text = "Ngày lập: ---";
                    lblInvoiceTotal.Text = "Tổng tiền: ---";
                    dgvInvoiceDetails.DataSource = null;
                    return;
                }

                lblInvoiceDate.Text = $"Ngày lập: {invoice.NgayLap:dd/MM/yyyy HH:mm}";
                lblInvoiceTotal.Text = $"Tổng tiền: {invoice.TongTien:#,##0} VND";

                var details = invoice.DonDatHang.ChiTietDonHangs.Select(ct => new
                {
                    ct.MaSP,
                    SanPhamName = ct.SanPham?.TenSP ?? ct.MaSP,
                    ct.SoLuong,
                    DonGiaText = ct.DonGia.ToString("#,##0") + " đ",
                    ThanhTienText = (ct.SoLuong * ct.DonGia).ToString("#,##0") + " đ",
                    RawDetail = ct
                }).ToList();

                dgvInvoiceDetails.DataSource = null;
                dgvInvoiceDetails.DataSource = details;
            }
            catch (Exception ex)
            {
                Toast.Show("Tìm hóa đơn lỗi: " + ex.Message, "danger");
            }
        }

        private void DgvInvoiceDetails_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || _invoiceUseCase == null) return;
            if (dgvInvoiceDetails.Columns[e.ColumnIndex].Name == "colReturn")
            {
                string invId = txtSearchInvoiceId.Text.Trim();
                if (string.IsNullOrEmpty(invId)) return;

                var invoice = _invoiceUseCase.GetInvoiceById(invId);
                if (invoice == null) return;

                var rowData = dgvInvoiceDetails.Rows[e.RowIndex].DataBoundItem;
                if (rowData == null) return;

                var propMaSP = rowData.GetType().GetProperty("MaSP");
                var propName = rowData.GetType().GetProperty("SanPhamName");
                var propQty = rowData.GetType().GetProperty("SoLuong");

                string maSp = propMaSP?.GetValue(rowData)?.ToString();
                string name = propName?.GetValue(rowData)?.ToString() ?? maSp;
                int maxQty = (int)(propQty?.GetValue(rowData) ?? 0);

                Form parent = this.FindForm();
                using (var form = new FormReturnInput(name, maxQty))
                {
                    var result = ThemeHelper.ShowCustomDialog(parent, form);
                    if (result == DialogResult.OK)
                    {
                        try
                        {
                            _invoiceUseCase.ReturnProduct(invId, maSp, form.ReturnQuantity, form.ReturnReason);
                            Toast.Show("Đổi trả và hoàn tiền hóa đơn thành công!", "success");

                            BtnSearchInvoice_Click(null, null);
                            LoadPOSProducts();
                        }
                        catch (Exception ex)
                        {
                            Toast.Show(ex.Message, "danger");
                        }
                    }
                }
            }
        }
    }
}
