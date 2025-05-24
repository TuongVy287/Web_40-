
namespace QLKS1.API.Repositories.Interfaces
{
    public interface IChiTietHoaDonRepository
    {
        Task<IEnumerable<ChiTietHoaDon>> GetAllChiTietHoaDon();
        Task<IEnumerable<ChiTietHoaDon>> GetChiTietHoaDonByIdAsync(int maHoaDon);
        Task<int> ThemChiTietHoaDonAsync(ChiTietHoaDon chiTietHoaDon);

        // Task<HoaDon> CreateAsync(HoaDon hoaDon);
        // Task<HoaDon?> UpdateAsync(int IDHoaDon, HoaDon hoaDon);
        // Task<bool> DeleteAsync(int IDHoaDon);
    }
}