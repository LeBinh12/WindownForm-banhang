using System;
using System.Drawing;
using System.Windows.Forms;
using QuanLyCuaHangTapHoa.Application.Interfaces;
using QuanLyCuaHangTapHoa.Domain;

namespace QuanLyCuaHangTapHoa.Presentation
{
    public partial class LoginForm : Form
    {
        private readonly IAccountUseCase _accountUseCase;
        private readonly IProductUseCase _productUseCase;
        private readonly IOrderUseCase _orderUseCase;
        private readonly IInvoiceUseCase _invoiceUseCase;

        // Custom dragging fields
        private bool _isDragging = false;
        private Point _dragStartPoint = new Point(0, 0);

        // Parameterless constructor for Visual Studio Designer
        public LoginForm()
        {
            InitializeComponent();
        }

        public LoginForm(
            IAccountUseCase accountUseCase,
            IProductUseCase productUseCase,
            IOrderUseCase orderUseCase,
            IInvoiceUseCase invoiceUseCase)
        {
            _accountUseCase = accountUseCase;
            _productUseCase = productUseCase;
            _orderUseCase = orderUseCase;
            _invoiceUseCase = invoiceUseCase;

            InitializeComponent();
        }

        // Mouse Drag handlers for Form Movement
        private void TitleBar_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                _isDragging = true;
                _dragStartPoint = new Point(e.X, e.Y);
            }
        }

        private void TitleBar_MouseMove(object sender, MouseEventArgs e)
        {
            if (_isDragging)
            {
                Point p = PointToScreen(e.Location);
                this.Location = new Point(p.X - _dragStartPoint.X, p.Y - _dragStartPoint.Y);
            }
        }

        private void TitleBar_MouseUp(object sender, MouseEventArgs e)
        {
            _isDragging = false;
        }

        private void BtnLogin_Click(object sender, EventArgs e)
        {
            lblError.Text = "";
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text;

            try
            {
                var account = _accountUseCase.Login(username, password);
                if (account == null)
                {
                    lblError.Text = "Tên đăng nhập hoặc mật khẩu không chính xác. ❌";
                    return;
                }

                // Successful login -> Redirect to MainForm
                this.Hide();
                using (var mainForm = new MainForm(account, _productUseCase, _orderUseCase, _invoiceUseCase, _accountUseCase))
                {
                    var result = mainForm.ShowDialog();
                    if (result == DialogResult.OK)
                    {
                        txtPassword.Clear();
                        lblError.Text = "";
                        this.Show();
                    }
                    else
                    {
                        this.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message;
            }
        }
    }
}
