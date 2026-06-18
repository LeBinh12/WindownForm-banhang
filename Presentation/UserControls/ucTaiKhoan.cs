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
    public partial class ucTaiKhoan : UserControl
    {
        private readonly IAccountUseCase _accountUseCase;
        private readonly TaiKhoan _currentUser;

        // Parameterless constructor for Visual Studio Designer
        public ucTaiKhoan()
        {
            InitializeComponent();
        }

        public ucTaiKhoan(IAccountUseCase accountUseCase, TaiKhoan currentUser)
        {
            _accountUseCase = accountUseCase;
            _currentUser = currentUser;

            InitializeComponent();
            if (_currentUser != null && _currentUser.NguoiDung is Admin)
            {
                LoadData();
            }
        }

        private void LoadData()
        {
            if (_accountUseCase == null) return;
            try
            {
                var list = _accountUseCase.GetAllAccounts();

                // Filters
                string q = txtSearch.Text.Trim().ToLower();
                string roleFilter = cbRoleSearch.SelectedValue?.ToString() ?? "Tất cả";

                var filtered = list.Where(a => 
                    (string.IsNullOrEmpty(q) || a.TenDangNhap.ToLower().Contains(q) || a.MaNguoiDung.ToLower().Contains(q) || (a.NguoiDung?.HoTen ?? "").ToLower().Contains(q)) &&
                    (roleFilter == "Tất cả" || 
                     (roleFilter == "Admin" && a.NguoiDung is Admin) ||
                     (roleFilter == "NhanVien" && a.NguoiDung is NhanVien) ||
                     (roleFilter == "KhachHang" && a.NguoiDung is KhachHang))
                ).ToList();

                var viewList = filtered.Select(tk => new TaiKhoanViewModel
                {
                    MaNguoiDung = tk.MaNguoiDung,
                    TenDangNhap = tk.TenDangNhap,
                    HoTen = tk.NguoiDung?.HoTen,
                    Email = tk.NguoiDung?.Email,
                    SoDienThoai = tk.NguoiDung?.SoDienThoai,
                    VaiTro = tk.NguoiDung is Admin ? "Admin" : (tk.NguoiDung is NhanVien ? "Nhân viên" : "Khách hàng"),
                    TrangThai = MapTrangThaiText(tk.TrangThaiTaiKhoan),
                    RawAccount = tk
                }).ToList();

                dgvAccounts.DataSource = null;
                dgvAccounts.DataSource = viewList;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải tài khoản: " + ex.Message);
            }
        }

        private string MapTrangThaiText(TrangThaiTaiKhoan status)
        {
            return status switch
            {
                TrangThaiTaiKhoan.HoatDong => "🟢 Hoạt động",
                TrangThaiTaiKhoan.BiKhoa => "🔴 Bị khóa",
                _ => status.ToString()
            };
        }

        private void BtnReset_Click(object sender, EventArgs e)
        {
            txtSearch.Clear();
            cbRoleSearch.SelectedIndex = 0;
            LoadData();
        }

        private void BtnAddNew_Click(object sender, EventArgs e)
        {
            if (_currentUser == null || _accountUseCase == null) return;
            Form parent = this.FindForm();
            using (var form = new FormTaiKhoanInput(_currentUser.MaNguoiDung))
            {
                var result = ThemeHelper.ShowCustomDialog(parent, form);
                if (result == DialogResult.OK)
                {
                    try
                    {
                        _accountUseCase.CreateAccount(form.User, form.Account, form.Role);
                        Toast.Show("Tạo tài khoản mới thành công!", "success");
                        LoadData();
                    }
                    catch (Exception ex)
                    {
                        Toast.Show(ex.Message, "danger");
                    }
                }
            }
        }

        private void DgvAccounts_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || _currentUser == null || _accountUseCase == null) return;

            var selectedItem = dgvAccounts.Rows[e.RowIndex].DataBoundItem as TaiKhoanViewModel;
            if (selectedItem == null) return;

            // EDIT ACTION
            if (dgvAccounts.Columns[e.ColumnIndex].Name == "colEdit")
            {
                Form parent = this.FindForm();
                using (var form = new FormTaiKhoanInput(_currentUser.MaNguoiDung, selectedItem.RawAccount))
                {
                    var result = ThemeHelper.ShowCustomDialog(parent, form);
                    if (result == DialogResult.OK)
                    {
                        try
                        {
                            _accountUseCase.UpdateAccount(form.User, form.Account);
                            Toast.Show("Cập nhật thông tin tài khoản thành công!", "success");
                            LoadData();
                        }
                        catch (Exception ex)
                        {
                            Toast.Show(ex.Message, "danger");
                        }
                    }
                }
            }

            // TOGGLE LOCK ACTION
            if (dgvAccounts.Columns[e.ColumnIndex].Name == "colLock")
            {
                string userId = selectedItem.MaNguoiDung;
                if (userId == _currentUser.MaNguoiDung)
                {
                    Toast.Show("Không thể tự khóa tài khoản của chính mình!", "warning");
                    return;
                }

                Form parent = this.FindForm();
                var acc = selectedItem.RawAccount;

                if (acc.TrangThaiTaiKhoan == TrangThaiTaiKhoan.HoatDong)
                {
                    bool confirm = ThemeHelper.ShowConfirmDialog(parent, "Khóa tài khoản", $"Bạn có chắc chắn muốn KHÓA tài khoản '{acc.TenDangNhap}'?");
                    if (confirm)
                    {
                        try
                        {
                            _accountUseCase.LockAccount(_currentUser.MaNguoiDung, userId);
                            Toast.Show("Đã khóa tài khoản thành công!", "success");
                            LoadData();
                        }
                        catch (Exception ex)
                        {
                            Toast.Show(ex.Message, "danger");
                        }
                    }
                }
                else
                {
                    bool confirm = ThemeHelper.ShowConfirmDialog(parent, "Mở khóa tài khoản", $"Xác nhận MỞ KHÓA tài khoản '{acc.TenDangNhap}'?");
                    if (confirm)
                    {
                        try
                        {
                            _accountUseCase.UnlockAccount(userId);
                            Toast.Show("Đã mở khóa tài khoản thành công!", "success");
                            LoadData();
                        }
                        catch (Exception ex)
                        {
                            Toast.Show(ex.Message, "danger");
                        }
                    }
                }
            }
        }

        private class TaiKhoanViewModel
        {
            public string MaNguoiDung { get; set; }
            public string TenDangNhap { get; set; }
            public string HoTen { get; set; }
            public string Email { get; set; }
            public string SoDienThoai { get; set; }
            public string VaiTro { get; set; }
            public string TrangThai { get; set; }
            public TaiKhoan RawAccount { get; set; }
        }
    }
}
