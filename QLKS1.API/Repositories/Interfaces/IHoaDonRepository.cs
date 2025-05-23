
namespace QLKS1.API.Repositories.Interfaces
{
    public interface IHoaDonRepository
    {
        Task<IEnumerable<HoaDon>> GetAllHoaDon();
        Task<HoaDon?> GetHoaDonByIdAsync(int IDHoaDon);

    Task<HoaDonResponsee> ThemHoaDonAsync(HoaDonRequestt request);
        // Task<HoaDon> CreateAsync(HoaDon hoaDon);
        // Task<HoaDon?> UpdateAsync(int IDHoaDon, HoaDon hoaDon);
        // Task<bool> DeleteAsync(int IDHoaDon);
    }
}