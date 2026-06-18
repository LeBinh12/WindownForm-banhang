using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Guna.UI2.WinForms;
using QuanLyCuaHangTapHoa.Domain;

namespace QuanLyCuaHangTapHoa.Presentation.Modals
{
    public partial class FormOrderDetails : Form
    {
        private readonly DonDatHang _order;

        // Parameterless constructor for Visual Studio Designer
        public FormOrderDetails()
        {
            InitializeComponent();
            SetupEvents();
            SetupData();
        }

        public FormOrderDetails(DonDatHang order)
        {
            _order = order;
            InitializeComponent();
            SetupEvents();
            SetupData();
            LoadDetails();
        }

        private void SetupEvents()
        {
            if (System.ComponentModel.LicenseManager.CurrentContext.UsageMode == System.ComponentModel.LicenseUsageMode.Designtime)
                return;

            btnClose.Click += (s, e) => this.Close();
            pnlFooter.SizeChanged += (s, e) =>
            {
                btnClose.Left = pnlFooter.Width - btnClose.Width;
            };
        }

        private void SetupData()
        {
            if (System.ComponentModel.LicenseManager.CurrentContext.UsageMode == System.ComponentModel.LicenseUsageMode.Designtime)
                return;

            ThemeHelper.StyleFlatDataGrid(dgvDetails);
        }

        private void LoadDetails()
        {
            if (_order == null) return;
            try
            {
                lblTitle.Text = $"CHI TIẾT ĐƠN HÀNG: {_order.MaDon}";
                lblValNgayDat.Text = _order.NgayDat.ToString("dd/MM/yyyy HH:mm");
                lblValTrangThai.Text = _order.TrangThaiDonHang switch
                {
                    TrangThaiDonHang.ChoDuyet    => "🟡 Chờ duyệt",
                    TrangThaiDonHang.DaDuyet     => "🟢 Đã duyệt",
                    TrangThaiDonHang.DaThanhToan => "🔵 Đã thanh toán",
                    TrangThaiDonHang.TuChoi      => "🔴 Từ chối",
                    TrangThaiDonHang.DaHuy       => "⚫ Đã hủy",
                    _                            => _order.TrangThaiDonHang.ToString()
                };
                lblValKhachHang.Text = _order.KhachHang?.HoTen ?? "—";
                lblValNgayDuyet.Text = _order.NgayDuyet.HasValue ? _order.NgayDuyet.Value.ToString("dd/MM/yyyy HH:mm") : "Chưa duyệt";

                var displayList = _order.ChiTietDonHangs.Select(ct => new
                {
                    TenSanPham = ct.SanPham?.TenSP ?? ct.MaSP,
                    DonGiaText = ct.DonGia.ToString("#,##0") + " đ",
                    SoLuong = ct.SoLuong,
                    ThanhTienText = (ct.SoLuong * ct.DonGia).ToString("#,##0") + " đ"
                }).ToList();

                dgvDetails.DataSource = null;
                dgvDetails.DataSource = displayList;

                decimal total = _order.ChiTietDonHangs.Sum(ct => ct.SoLuong * ct.DonGia);
                lblTotal.Text = $"TỔNG TIỀN CỦA ĐƠN: {total:#,##0} VND";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi hiển thị chi tiết đơn hàng: " + ex.Message);
            }
        }
    }
}
