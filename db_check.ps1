$connString = "Server=LAPTOP-OE1EAKJF\SQLEXPRESS;Database=QuanLyCuaHangTapHoa;Trusted_Connection=True;TrustServerCertificate=True"
$conn = New-Object System.Data.SqlClient.SqlConnection($connString)
$conn.Open()
$cmd = $conn.CreateCommand()
$cmd.CommandText = "SELECT MaDon, NgayDat, TrangThaiDonHang, MaNguoiDungKhachHang FROM DonDatHangs"
$reader = $cmd.ExecuteReader()
while ($reader.Read()) {
    $maDon = $reader["MaDon"]
    $ngayDat = $reader["NgayDat"]
    $status = $reader["TrangThaiDonHang"]
    $customer = $reader["MaNguoiDungKhachHang"]
    Write-Output "MaDon: $maDon | NgayDat: $ngayDat | Status: $status | Customer: $customer"
}
$conn.Close()
