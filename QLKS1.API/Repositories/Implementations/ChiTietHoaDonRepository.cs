using System.Data;
using Dapper;
using QLKS1.API.Repositories.Interfaces;


public class ChiTietHoaDonRepository : IChiTietHoaDonRepository
{
    private readonly IDbConnection _db;

    public ChiTietHoaDonRepository(IDbConnection db)
    {
        _db = db;
    }

    public async Task<IEnumerable<ChiTietHoaDon>> GetAllChiTietHoaDon()
    {
        var chitiethoadonList = (await _db.QueryAsync<ChiTietHoaDon>(
        "sp_GetAllChiTietHoaDon", commandType: CommandType.StoredProcedure)).ToList();

        return chitiethoadonList;
    }

    public async Task<ChiTietHoaDon?> GetChiTietHoaDonByIdAsync(int MaHoaDon)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@MaHoaDon", MaHoaDon);

        var chiTietHoaDon = await _db.QueryFirstOrDefaultAsync<ChiTietHoaDon>(
            "sp_GetChiTietHoaDonById", parameters, commandType: CommandType.StoredProcedure);

        return chiTietHoaDon;
    }
}
