
namespace QLKS1.API.Repositories.Interfaces
{
    public interface IDoanhThuRepository
    {
        Task<IEnumerable<DoanhThu>> GetAllDoanhThu();
        Task<DoanhThu?> GetDoanhThuByDayAsync(DateTime Date);
        // Task<HoaDon> CreateAsync(HoaDon hoaDon);
        // Task<HoaDon?> UpdateAsync(int IDHoaDon, HoaDon hoaDon);
        // Task<bool> DeleteAsync(int IDHoaDon);
    }
}