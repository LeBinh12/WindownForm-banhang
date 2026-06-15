using System.Collections.Generic;
using QuanLyCuaHangTapHoa.Domain;

namespace QuanLyCuaHangTapHoa.Domain.Interfaces
{
    public interface IProductRepository
    {
        List<SanPham> GetAll();
        SanPham GetById(string id);
        void Add(SanPham product);
        void Update(SanPham product);
        void Delete(string id);
    }

    public interface IOrderRepository
    {
        List<DonDatHang> GetAll();
        DonDatHang GetById(string id);
        void Add(DonDatHang order);
        void Update(DonDatHang order);
    }

    public interface IInvoiceRepository
    {
        List<HoaDon> GetAll();
        HoaDon GetById(string id);
        void Add(HoaDon invoice);
        void Update(HoaDon invoice);
    }

    public interface IAccountRepository
    {
        List<TaiKhoan> GetAll();
        TaiKhoan GetByUsername(string username);
        TaiKhoan GetByUserId(string userId);
        void Add(NguoiDung user, TaiKhoan account);
        void Update(NguoiDung user, TaiKhoan account);
        void UpdateAccountState(string userId, TrangThaiTaiKhoan state);
    }
}
