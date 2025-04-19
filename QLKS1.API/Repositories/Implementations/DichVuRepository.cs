using System.Data;
using Dapper;
using QLKS1.API.Repositories.Interfaces;


public class DichVuRepository : IDichVuRepository
{
    private readonly IDbConnection _db;

    public DichVuRepository(IDbConnection db)
    {
        _db = db;
    }

    public async Task<IEnumerable<DichVu>> GetAllDichVu()
    {
        var dichvuList = (await _db.QueryAsync<DichVu>(
        "sp_GetAllDichVu", commandType: CommandType.StoredProcedure)).ToList();

    return dichvuList;
    }

    public async Task<DoanhThu?> GetDichVuByIdAsync(int IDDichVu)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@IDDichVu", IDDichVu);
    
        var doanhThu = await _db.QueryFirstOrDefaultAsync<DoanhThu>(
            "sp_GetDichVuById", parameters, commandType: CommandType.StoredProcedure);
    
        return doanhThu;
    }

}
