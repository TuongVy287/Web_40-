using System.Data;
using Dapper;
using QLKS1.API.Repositories.Interfaces;


public class DashboardPhongRepository : IDashboardPhongRepository
{
    private readonly IDbConnection _db;

    public DashboardPhongRepository(IDbConnection db)
    {
        _db = db;
    }

    public async Task<IEnumerable<ChiTietHoaDon>> GetAllChiTietHoaDon()
    {
        var chitiethoadonList = (await _db.QueryAsync<ChiTietHoaDon>(
        "sp_GetAllChiTietHoaDon", commandType: CommandType.StoredProcedure)).ToList();

        return chitiethoadonList;
    }

    public async Task<IEnumerable<DashboardPhong>> GetDashboardPhongAsync(DateTime? ngayBatDau, DateTime? ngayKetThuc)
{
    var parameters = new DynamicParameters();
    parameters.Add("@NgayBatDau", ngayBatDau);
    parameters.Add("@NgayKetThuc", ngayKetThuc);

    var result = await _db.QueryAsync<DashboardPhong>(
        "spAPI_DashboardPhong",
        parameters,
        commandType: CommandType.StoredProcedure
    );

    return result;
}
}
