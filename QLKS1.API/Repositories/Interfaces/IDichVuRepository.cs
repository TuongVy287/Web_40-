
namespace QLKS1.API.Repositories.Interfaces
{
    public interface IDichVuRepository
    {
        Task<IEnumerable<DichVu>> GetAllDichVu();
        Task<DoanhThu?> GetDichVuByIdAsync(int IDDichVu);
        // Task<HoaDon> CreateAsync(HoaDon hoaDon);
        // Task<HoaDon?> UpdateAsync(int IDHoaDon, HoaDon hoaDon);
        // Task<bool> DeleteAsync(int IDHoaDon);
    }
}