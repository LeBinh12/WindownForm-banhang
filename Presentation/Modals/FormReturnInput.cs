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
        }

        public FormReturnInput(string productName, int maxQty)
        {
            _productName = productName;
            _maxQty = maxQty;

            InitializeComponent();
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
