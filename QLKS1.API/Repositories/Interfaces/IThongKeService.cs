namespace QLKS1.API.Repositories.Interfaces
{

    public interface IThongKeService
    {
        Task<ThongKe> GetThongKeNgayHomNayAsync();

            Task<IEnumerable<ThongKeSoLuongNguoiResponse>> GetSoLuongNguoiTheoThangHoacNamAsync(string nam);

    }
}