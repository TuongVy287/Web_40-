using System.Data;
using Dapper;
using QLKS1.API.Repositories.Interfaces;


public class ChiPhiRepository : IChiPhiRepository
{
    private readonly IDbConnection _db;

    public ChiPhiRepository(IDbConnection db)
    {
        _db = db;
    }

    public async Task<IEnumerable<ChiPhi>> GetAllChiPhi()
    {
        var chiphiList = (await _db.QueryAsync<ChiPhi>(
        "sp_GetAllChiPhi", commandType: CommandType.StoredProcedure)).ToList();

        return chiphiList;
    }

    public async Task<ChiPhi?> GetChiPhiByDateAsync(DateTime Ngay)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@Ngay", Ngay);

        var chiPhi = await _db.QueryFirstOrDefaultAsync<ChiPhi>(
            "sp_GetChiPhiByDate", parameters, commandType: CommandType.StoredProcedure);

        return chiPhi;
    }
    
}
