
namespace QLKS1.API.Repositories.Interfaces
{
    public interface IChiTietHoaDonRepository
    {
        Task<IEnumerable<ChiTietHoaDon>> GetAllChiTietHoaDon();
        Task<ChiTietHoaDon?> GetChiTietHoaDonByIdAsync(int MaHoaDon);
            Task<decimal> ThemChiTietHoaDonAsync(int idHoaDon, int idDichVu, int soLuong);

        // Task<HoaDon> CreateAsync(HoaDon hoaDon);
        // Task<HoaDon?> UpdateAsync(int IDHoaDon, HoaDon hoaDon);
        // Task<bool> DeleteAsync(int IDHoaDon);
    }
}