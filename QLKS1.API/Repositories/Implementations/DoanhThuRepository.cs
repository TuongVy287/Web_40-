using System.Data;
using Dapper;
using QLKS1.API.Repositories.Interfaces;


public class DoanhThuRepository : IDoanhThuRepository
{
    private readonly IDbConnection _db;

    public DoanhThuRepository(IDbConnection db)
    {
        _db = db;
    }

    public async Task<IEnumerable<DoanhThu>> GetAllDoanhThu()
    {
        var doanhthuList = (await _db.QueryAsync<DoanhThu>(
        "sp_GetAllDoanhThu", commandType: CommandType.StoredProcedure)).ToList();

    return doanhthuList;
    }

    public async Task<DoanhThu?> GetDoanhThuByDayAsync(DateTime date)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@Date", date);

        var doanhthu = await _db.QueryFirstOrDefaultAsync<DoanhThu>(
            "sp_GetDoanhThuByDay", parameters, commandType: CommandType.StoredProcedure);

        return doanhthu;
    }
    
}
