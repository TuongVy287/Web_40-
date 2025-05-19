using System.Data;
using Dapper;
using QLKS1.API.Repositories.Interfaces;

public class ThongKeService : IThongKeService
{
    private readonly IDbConnection _db;

    // Đổi tên constructor thành ThongKeService
    public ThongKeService(IDbConnection db)
    {
        _db = db;
    }

    public async Task<ThongKe> GetThongKeNgayHomNayAsync()
    {
        var result = await _db.QueryFirstOrDefaultAsync<ThongKe>(
            "spAPI_Overview",
            commandType: CommandType.StoredProcedure
        );

        return result;
    }
     public async Task<IEnumerable<ThongKeSoLuongNguoiResponse>> GetSoLuongNguoiTheoThangHoacNamAsync(string nam)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@Nam", nam);

        var result = await _db.QueryAsync<ThongKeSoLuongNguoiResponse>(
            "sp_TinhSoLuongNguoiTheoThang",
            parameters,
            commandType: CommandType.StoredProcedure
        );

        return result;
    }
}

