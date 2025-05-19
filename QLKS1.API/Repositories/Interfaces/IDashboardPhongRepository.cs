
namespace QLKS1.API.Repositories.Interfaces
{
    public interface IDashboardPhongRepository
{
    Task<IEnumerable<DashboardPhong>> GetDashboardPhongAsync(DateTime? ngayBatDau, DateTime? ngayKetThuc);
}
}