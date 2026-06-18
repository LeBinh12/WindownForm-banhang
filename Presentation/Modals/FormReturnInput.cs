using System;
using System.Drawing;
using System.Windows.Forms;
using Guna.UI2.WinForms;

namespace QuanLyCuaHangTapHoa.Presentation.Modals
{
    public partial class FormReturnInput : Form
    {
        public int ReturnQuantity { get; private set; }
        public string ReturnReason { get; private set; }

        private readonly string _productName;
        private readonly int _maxQty;

        // Parameterless constructor for Visual Studio Designer
        public FormReturnInput()
        {
            InitializeComponent();
            SetupData();
            SetupEvents();
        }

        public FormReturnInput(string productName, int maxQty)
        {
            _productName = productName;
            _maxQty = maxQty;

            InitializeComponent();
            SetupData();
            SetupEvents();
        }

        private void SetupData()
        {
            if (System.ComponentModel.LicenseManager.CurrentContext.UsageMode == System.ComponentModel.LicenseUsageMode.Designtime)
                return;

            lblProduct.Text = _productName ?? "Sản phẩm";
            int max = _maxQty > 0 ? _maxQty : 1;
            lblPurchased.Text = $"{max} sản phẩm";
            numQty.Maximum = max;
        }

        private void SetupEvents()
        {
            if (System.ComponentModel.LicenseManager.CurrentContext.UsageMode == System.ComponentModel.LicenseUsageMode.Designtime)
                return;

            btnCancel.Click += (s, e) => this.Close();
            btnSave.Click += BtnSave_Click;
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            string reason = txtReason.Text.Trim();
            if (string.IsNullOrEmpty(reason))
            {
                Toast.Show("Lý do trả hàng lỗi không được để trống!", "danger");
                return;
            }

            ReturnQuantity = (int)numQty.Value;
            ReturnReason = reason;

            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
