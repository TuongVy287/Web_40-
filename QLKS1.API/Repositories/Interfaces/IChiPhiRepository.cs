
namespace QLKS1.API.Repositories.Interfaces
{
    public interface IChiPhiRepository
    {
        Task<IEnumerable<ChiPhi>> GetAllChiPhi();
        Task<ChiPhi?> GetChiPhiByDateAsync(DateTime Ngay);
        // Task<HoaDon> CreateAsync(HoaDon hoaDon);
        // Task<HoaDon?> UpdateAsync(int IDHoaDon, HoaDon hoaDon);
        // Task<bool> DeleteAsync(int IDHoaDon);
    }
}