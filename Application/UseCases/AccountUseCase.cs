using QuanLyCuaHangTapHoa.Application.Interfaces;
using QuanLyCuaHangTapHoa.Domain;
using QuanLyCuaHangTapHoa.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace QuanLyCuaHangTapHoa.Application.UseCases
{
    public class AccountUseCase : IAccountUseCase
    {
        private readonly IAccountRepository _accountRepository;

        public AccountUseCase(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public TaiKhoan Login(string username, string password)
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
                throw new ArgumentException("Tên đăng nhập và mật khẩu không được để trống.");

            var account = _accountRepository.GetByUsername(username);
            if (account == null)
                return null;

            // Business Rule SR-03: BiKhoa account cannot log in
            if (account.TrangThaiTaiKhoan == TrangThaiTaiKhoan.BiKhoa)
                throw new InvalidOperationException("Tài khoản này đã bị khóa. Vui lòng liên hệ quản trị viên.");

            // Verify bcrypt hash password
            bool isValid = BCrypt.Net.BCrypt.Verify(password, account.MatKhau);
            if (!isValid)
                return null;

            return account;
        }

        public List<TaiKhoan> GetAllAccounts()
        {
            return _accountRepository.GetAll();
        }

        public void CreateAccount(NguoiDung user, TaiKhoan account, string role)
        {
            if (string.IsNullOrWhiteSpace(account.TenDangNhap))
                throw new ArgumentException("Tên đăng nhập không được trống.");
            if (string.IsNullOrWhiteSpace(account.MatKhau))
                throw new ArgumentException("Mật khẩu không được trống.");
            if (string.IsNullOrWhiteSpace(user.MaNguoiDung))
                throw new ArgumentException("Mã người dùng không được trống.");
            if (string.IsNullOrWhiteSpace(user.HoTen))
                throw new ArgumentException("Họ tên không được trống.");

            // Business Rule SR-02: TenDangNhap must be unique
            var existingAcc = _accountRepository.GetByUsername(account.TenDangNhap);
            if (existingAcc != null)
                throw new InvalidOperationException("Tên đăng nhập đã tồn tại.");

            // Verify unique email and phone
            var allUsers = _accountRepository.GetAll().Select(tk => tk.NguoiDung).ToList();
            if (allUsers.Any(u => u.Email.Equals(user.Email, StringComparison.OrdinalIgnoreCase)))
                throw new InvalidOperationException("Email đã được sử dụng.");
            if (allUsers.Any(u => u.SoDienThoai.Equals(user.SoDienThoai)))
                throw new InvalidOperationException("Số điện thoại đã được sử dụng.");

            // Verify unique MaNhanVien / MaKhachHang depending on role
            if (user is NhanVien nv)
            {
                if (allUsers.OfType<NhanVien>().Any(n => n.MaNhanVien.Equals(nv.MaNhanVien, StringComparison.OrdinalIgnoreCase)))
                    throw new InvalidOperationException("Mã nhân viên đã tồn tại.");
            }
            else if (user is KhachHang kh)
            {
                if (allUsers.OfType<KhachHang>().Any(k => k.MaKhachHang.Equals(kh.MaKhachHang, StringComparison.OrdinalIgnoreCase)))
                    throw new InvalidOperationException("Mã khách hàng đã tồn tại.");
            }

            // Hash password using bcrypt
            account.MatKhau = BCrypt.Net.BCrypt.HashPassword(account.MatKhau);
            account.MaNguoiDung = user.MaNguoiDung;

            _accountRepository.Add(user, account);
        }

        public void UpdateAccount(NguoiDung user, TaiKhoan account)
        {
            var existingAcc = _accountRepository.GetByUserId(user.MaNguoiDung);
            if (existingAcc == null)
                throw new KeyNotFoundException("Không tìm thấy tài khoản tương ứng.");

            // If a new password is provided, hash it, otherwise preserve the old one
            if (!string.IsNullOrWhiteSpace(account.MatKhau) && account.MatKhau != existingAcc.MatKhau)
            {
                existingAcc.MatKhau = BCrypt.Net.BCrypt.HashPassword(account.MatKhau);
            }

            existingAcc.TenDangNhap = account.TenDangNhap;
            existingAcc.TrangThaiTaiKhoan = account.TrangThaiTaiKhoan;

            // Update user properties
            var existingUser = existingAcc.NguoiDung;
            existingUser.HoTen = user.HoTen;
            existingUser.Email = user.Email;
            existingUser.SoDienThoai = user.SoDienThoai;
            existingUser.DiaChi = user.DiaChi;

            if (user is NhanVien nv && existingUser is NhanVien existingNv)
            {
                existingNv.ChucVu = nv.ChucVu;
            }
            else if (user is KhachHang kh && existingUser is KhachHang existingKh)
            {
                existingKh.MaKhachHang = kh.MaKhachHang;
            }

            _accountRepository.Update(existingUser, existingAcc);
        }

        public void LockAccount(string currentUserId, string targetUserId)
        {
            // Business Rule SR-05: Admin cannot self-lock
            if (currentUserId == targetUserId)
                throw new InvalidOperationException("Không thể tự khóa tài khoản của chính mình khi đang đăng nhập.");

            _accountRepository.UpdateAccountState(targetUserId, TrangThaiTaiKhoan.BiKhoa);
        }

        public void UnlockAccount(string targetUserId)
        {
            _accountRepository.UpdateAccountState(targetUserId, TrangThaiTaiKhoan.HoatDong);
        }
    }
}
