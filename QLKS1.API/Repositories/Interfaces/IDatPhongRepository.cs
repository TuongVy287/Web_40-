
namespace QLKS1.API.Repositories.Interfaces
{
    public interface IDatPhongRepository
    {
        Task<IEnumerable<DatPhong>> GetAllDatPhong();
        Task<DatPhong?> GetDatPhongByIdAsync(int IDDatPhong);
        // Task<HoaDon> CreateAsync(HoaDon hoaDon);
        // Task<HoaDon?> UpdateAsync(int IDHoaDon, HoaDon hoaDon);
        // Task<bool> DeleteAsync(int IDHoaDon);
    }
}