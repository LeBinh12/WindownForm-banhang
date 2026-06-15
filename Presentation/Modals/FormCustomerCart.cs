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
    public class FormCustomerCart : Form
    {
        private readonly IProductUseCase _productUseCase;
        private readonly IOrderUseCase _orderUseCase;
        private readonly TaiKhoan _currentUser;

        private List<(SanPham product, int quantity)> _cart = new List<(SanPham, int)>();

        private Guna2ComboBox cbProducts;
        private NumericUpDown numQty;
        private Guna2Button btnAdd;
        private DataGridView dgvCart;
        private Label lblTotal;
        private Guna2Button btnSubmit;
        private Guna2Button btnCancel;

        public FormCustomerCart(IProductUseCase productUseCase, IOrderUseCase orderUseCase, TaiKhoan currentUser)
        {
            _productUseCase = productUseCase;
            _orderUseCase = orderUseCase;
            _currentUser = currentUser;

            InitializeComponent();
            LoadProducts();
        }

        private void InitializeComponent()
        {
            this.Size = new Size(680, 480);
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
                Text = "TẠO YÊU CẦU ĐẶT GIỮ HÀNG KHO",
                Font = ThemeHelper.FontSubheading,
                ForeColor = ThemeHelper.Primary,
                AutoSize = true,
                Location = new Point(20, 15)
            };
            pnlMain.Controls.Add(lblTitle);

            // TableLayout split: Left input (40%), Right list (60%)
            TableLayoutPanel splitLayout = new TableLayoutPanel
            {
                Location = new Point(20, 50),
                Size = new Size(640, 350),
                ColumnCount = 2,
                RowCount = 1,
                BackColor = Color.Transparent
            };
            splitLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 40F));
            splitLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 60F));
            pnlMain.Controls.Add(splitLayout);

            // Left Input Panel
            Panel pnlLeft = new Panel { Dock = DockStyle.Fill, Padding = new Padding(0, 10, 10, 0) };
            splitLayout.Controls.Add(pnlLeft, 0, 0);

            var lblSelect = new Label { Text = "Chọn sản phẩm *", Font = ThemeHelper.FontCaptionBold, ForeColor = ThemeHelper.TextSecondary, Location = new Point(0, 10), AutoSize = true };
            cbProducts = new Guna2ComboBox { Location = new Point(0, 30), Width = 230, Height = 36 };
            ThemeHelper.StyleComboBox(cbProducts);
            pnlLeft.Controls.AddRange(new Control[] { lblSelect, cbProducts });

            var lblQty = new Label { Text = "Số lượng đặt *", Font = ThemeHelper.FontCaptionBold, ForeColor = ThemeHelper.TextSecondary, Location = new Point(0, 80), AutoSize = true };
            numQty = new NumericUpDown { Location = new Point(0, 100), Width = 100, Minimum = 1, Maximum = 100, Font = ThemeHelper.FontBody };
            pnlLeft.Controls.AddRange(new Control[] { lblQty, numQty });

            btnAdd = new Guna2Button
            {
                Text = "Thêm vào giỏ",
                Location = new Point(0, 160),
                Size = new Size(160, 38),
                BorderRadius = 19,
                FillColor = ThemeHelper.Primary,
                Font = ThemeHelper.FontBodyBold,
                ForeColor = Color.White,
                Cursor = Cursors.Hand
            };
            btnAdd.Click += BtnAdd_Click;
            pnlLeft.Controls.Add(btnAdd);

            // Right Panel
            Panel pnlRight = new Panel { Dock = DockStyle.Fill, Padding = new Padding(10, 10, 0, 0) };
            splitLayout.Controls.Add(pnlRight, 1, 0);

            dgvCart = new DataGridView
            {
                Dock = DockStyle.Fill,
                AutoGenerateColumns = false
            };
            ThemeHelper.StyleFlatDataGrid(dgvCart);
            dgvCart.DataError += (s, e) => { e.ThrowException = false; };
            pnlRight.Controls.Add(dgvCart);

            dgvCart.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Sản phẩm", DataPropertyName = "TenSP", Width = 150, AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill });
            dgvCart.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "SL", DataPropertyName = "quantity", Width = 50 });
            dgvCart.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Thành tiền", DataPropertyName = "ThanhTienText", Width = 100 });
            
            var colRemove = new DataGridViewButtonColumn
            {
                Name = "colRemove",
                HeaderText = "Xóa",
                Text = "Xóa",
                UseColumnTextForButtonValue = true,
                Width = 60
            };
            dgvCart.Columns.Add(colRemove);
            dgvCart.CellClick += DgvCart_CellClick;

            // Bottom elements
            lblTotal = new Label
            {
                Text = "TỔNG ĐƠN: 0 VND",
                Font = ThemeHelper.FontBodyBold,
                ForeColor = ThemeHelper.Danger,
                AutoSize = true,
                Location = new Point(20, 425)
            };
            pnlMain.Controls.Add(lblTotal);

            btnCancel = new Guna2Button
            {
                Text = "Hủy bỏ",
                Location = new Point(440, 415),
                Size = new Size(100, 38),
                BorderRadius = 19,
                FillColor = ThemeHelper.BorderLight,
                HoverState = { FillColor = ThemeHelper.Border },
                Font = ThemeHelper.FontBodyBold,
                ForeColor = ThemeHelper.TextSecondary,
                Cursor = Cursors.Hand
            };
            btnCancel.Click += (s, e) => this.Close();
            pnlMain.Controls.Add(btnCancel);

            btnSubmit = new Guna2Button
            {
                Text = "Gửi yêu cầu",
                Location = new Point(550, 415),
                Size = new Size(110, 38),
                BorderRadius = 19,
                FillColor = ThemeHelper.Success,
                HoverState = { FillColor = Color.FromArgb(4, 120, 87) },
                Font = ThemeHelper.FontBodyBold,
                ForeColor = Color.White,
                Cursor = Cursors.Hand
            };
            btnSubmit.Click += BtnSubmit_Click;
            pnlMain.Controls.Add(btnSubmit);
        }

        private void LoadProducts()
        {
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
