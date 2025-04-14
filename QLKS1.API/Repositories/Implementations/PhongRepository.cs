using System.Data; 
using Dapper; 
using QLKS1.API.Repositories.Interfaces;

public class PhongRepository : IPhongRepository 
{
    private readonly IDbConnection _db;

    public PhongRepository(IDbConnection db)
{
    _db = db;
}

public async Task<List<Phong>> GetAllPhong()
{
    var phongList = (await _db.QueryAsync<Phong>(
        "sp_GetAllPhong", commandType: CommandType.StoredProcedure)).ToList();

    return phongList;
}

public async Task<Phong> GetPhongByIdAsync(int IDPhong)
{
    var parameters = new DynamicParameters();
    parameters.Add("@IDPhong", IDPhong);

    var phong = await _db.QueryFirstOrDefaultAsync<Phong>(
        "sp_GetPhongById", parameters, commandType: CommandType.StoredProcedure);

    return phong;
}

}