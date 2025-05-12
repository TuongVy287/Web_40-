public interface IDatDichVuRepository
{
    Task<bool> DatDichVuAsync(DatDichVu datDichVu);
    Task<decimal> TinhTienDichVuAsync(int idKhachHang, int idPhong);
}
