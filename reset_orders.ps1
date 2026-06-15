$connString = "Server=LAPTOP-OE1EAKJF\SQLEXPRESS;Database=QuanLyCuaHangTapHoa;Trusted_Connection=True;TrustServerCertificate=True"
$conn = New-Object System.Data.SqlClient.SqlConnection($connString)
$conn.Open()
$cmd = $conn.CreateCommand()
$cmd.CommandText = "UPDATE DonDatHangs SET TrangThaiDonHang = 0; UPDATE SanPhams SET TrangThaiSanPham = 1 WHERE SoLuongTon > 0;"
$rowsAffected = $cmd.ExecuteNonQuery()
Write-Output "Da cap nhat $rowsAffected dong trong co so du lieu ve trang thai Cho duyet!"
$conn.Close()
