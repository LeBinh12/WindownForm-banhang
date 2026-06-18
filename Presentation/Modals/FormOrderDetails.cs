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
        }

        public FormOrderDetails(DonDatHang order)
        {
            _order = order;
            InitializeComponent();
            LoadDetails();
        }

        private void LoadDetails()
        {
            if (_order == null) return;
            try
            {
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
