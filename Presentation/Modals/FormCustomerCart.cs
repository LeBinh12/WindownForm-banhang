using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Guna.UI2.WinForms;
using QuanLyCuaHangTapHoa.Application.Interfaces;
using QuanLyCuaHangTapHoa.Domain;

namespace QuanLyCuaHangTapHoa.Presentation.Modals
{
    public partial class FormCustomerCart : Form
    {
        private readonly IProductUseCase _productUseCase;
        private readonly IOrderUseCase _orderUseCase;
        private readonly TaiKhoan _currentUser;

        private List<(SanPham product, int quantity)> _cart = new List<(SanPham, int)>();

        // Parameterless constructor for Visual Studio Designer
        public FormCustomerCart()
        {
            InitializeComponent();
        }

        public FormCustomerCart(IProductUseCase productUseCase, IOrderUseCase orderUseCase, TaiKhoan currentUser)
        {
            _productUseCase = productUseCase;
            _orderUseCase = orderUseCase;
            _currentUser = currentUser;

            InitializeComponent();
            LoadProducts();
        }

        private void LoadProducts()
        {
            if (_productUseCase == null) return;
            try
            {
                // Customer only sees active products with stock > 0 (BR-01)
                var list = _productUseCase.SearchProducts("", "Tất cả")
                    .Where(p => p.TrangThaiSanPham == TrangThaiSanPham.SanSang && p.SoLuongTon > 0)
                    .ToList();
                cbProducts.DataSource = null;
                cbProducts.DataSource = list;
                cbProducts.DisplayMember = "TenSP";
                cbProducts.ValueMember = "MaSP";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải sản phẩm: " + ex.Message);
            }
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            var selected = cbProducts.SelectedItem as SanPham;
            if (selected == null) return;

            int qty = (int)numQty.Value;

            var existingIndex = _cart.FindIndex(x => x.product.MaSP == selected.MaSP);
            if (existingIndex >= 0)
            {
                var current = _cart[existingIndex];
                if (selected.SoLuongTon < current.quantity + qty)
                {
                    Toast.Show("Số lượng tồn kho không đủ!", "warning");
                    return;
                }
                _cart[existingIndex] = (current.product, current.quantity + qty);
            }
            else
            {
                if (selected.SoLuongTon < qty)
                {
                    Toast.Show("Số lượng tồn kho không đủ!", "warning");
                    return;
                }
                _cart.Add((selected, qty));
            }

            UpdateCartUI();
        }

        private void DgvCart_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            if (dgvCart.Columns[e.ColumnIndex].Name == "colRemove")
            {
                _cart.RemoveAt(e.RowIndex);
                UpdateCartUI();
            }
        }

        private void UpdateCartUI()
        {
            var display = _cart.Select(x => new
            {
                TenSP = x.product.TenSP,
                quantity = x.quantity,
                ThanhTienText = (x.quantity * x.product.DonGia).ToString("#,##0")
            }).ToList();

            dgvCart.DataSource = null;
            dgvCart.DataSource = display;

            decimal total = _cart.Sum(x => x.quantity * x.product.DonGia);
            lblTotal.Text = $"TỔNG TẠM TÍNH: {total:#,##0} VND";
        }

        private void BtnSubmit_Click(object sender, EventArgs e)
        {
            if (_orderUseCase == null || _currentUser == null) return;
            try
            {
                if (!_cart.Any())
                {
                    Toast.Show("Giỏ hàng rỗng!", "warning");
                    return;
                }

                var items = _cart.Select(x => (x.product.MaSP, x.quantity)).ToList();
                var order = _orderUseCase.CreateOrder(_currentUser.MaNguoiDung, items);

                Toast.Show($"Gửi yêu cầu thành công! Đơn hàng: {order.MaDon}", "success");
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                Toast.Show(ex.Message, "danger");
            }
        }
    }
}
