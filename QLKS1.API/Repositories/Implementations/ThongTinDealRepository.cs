using System.Data;
using Dapper;
using System.Collections.Generic;
using System.Threading.Tasks;

public class ThongTinDealRepository : IThongTinDealRepository
{
    private readonly IDbConnection _db;

    public ThongTinDealRepository(IDbConnection db)
    {
        _db = db;
    }

    public async Task<IEnumerable<ThongTinDeal>> GetAllThongTinDealAsync()
    {
        var result = await _db.QueryAsync<ThongTinDeal>(
            "spAPI_ThongTinDeal",
            commandType: CommandType.StoredProcedure
        );

        return result;
    }
}