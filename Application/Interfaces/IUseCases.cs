using System.Collections.Generic;
using QuanLyCuaHangTapHoa.Domain;

namespace QuanLyCuaHangTapHoa.Application.Interfaces
{
    public interface IProductUseCase
    {
        List<SanPham> SearchProducts(string keyword, string category);
        List<SanPham> GetAllProducts();
        SanPham GetProductById(string id);
        void AddProduct(SanPham product);
        void UpdateProduct(SanPham product);
        void DeleteProduct(string id);
        void UpdateInventory(string productId, int newQuantity, TrangThaiSanPham status);
    }

    public interface IOrderUseCase
    {
        DonDatHang CreateOrder(string khachHangId, List<(string productId, int quantity)> items);
        void ApproveOrder(string orderId);
        void CancelOrRejectOrder(string orderId, bool isReject);
        List<DonDatHang> GetAllOrders();
        List<DonDatHang> GetOrdersByCustomer(string khachHangId);
        void CheckAndCancelExpiredOrders();
        DonDatHang GetById(string orderId);
        List<DonDatHang> GetByStatus(TrangThaiDonHang status);
    }

    public interface IInvoiceUseCase
    {
        HoaDon CreateInvoice(string orderId, string nhanVienId);
        HoaDon CreatePOSInvoice(string nhanVienId, List<(string productId, int quantity)> items);
        void ReturnProduct(string invoiceId, string productId, int returnQty, string reason);
        List<HoaDon> GetAllInvoices();
        HoaDon GetInvoiceById(string id);
    }

    public interface IAccountUseCase
    {
        TaiKhoan Login(string username, string password);
        List<TaiKhoan> GetAllAccounts();
        void CreateAccount(NguoiDung user, TaiKhoan account, string role);
        void UpdateAccount(NguoiDung user, TaiKhoan account);
        void LockAccount(string currentUserId, string targetUserId);
        void UnlockAccount(string targetUserId);
    }
}
