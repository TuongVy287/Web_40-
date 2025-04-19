using System.Data;
using Dapper;
using QLKS1.API.Repositories.Interfaces;


public class DatPhongRepository : IDatPhongRepository
{
    private readonly IDbConnection _db;

    public DatPhongRepository(IDbConnection db)
    {
        _db = db;
    }

    public async Task<IEnumerable<DatPhong>> GetAllDatPhong()
    {
        var datphongList = (await _db.QueryAsync<DatPhong>(
        "sp_GetAllDatPhong", commandType: CommandType.StoredProcedure)).ToList();

    return datphongList;
    }

    public async Task<DatPhong?> GetDatPhongByIdAsync(int IDDatPhong)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@IDDatPhong", IDDatPhong);
    
        var datPhong = await _db.QueryFirstOrDefaultAsync<DatPhong>(
            "sp_GetDatPhongById", parameters, commandType: CommandType.StoredProcedure);
    
        return datPhong;
    }

}

