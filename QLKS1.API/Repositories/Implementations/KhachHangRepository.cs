using System.Data;
using Dapper;
using QLKS1.API.Repositories.Interfaces;

public class KhachHangRepository : IKhachHangRepository
{
    private readonly IDbConnection _db;

    public KhachHangRepository(IDbConnection db)
    {
        _db = db;
    }

    public async Task<List<KhachHang>> GetAllKhachHang()
    {



        var kh = (await _db.QueryAsync<KhachHang>(
            "sp_GetKhachHangByI", commandType: CommandType.StoredProcedure)).ToList();

        return kh;
    }

    public async Task<KhachHang> GetKhachHangByIdAsync(int maKH)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@MaKH", maKH);


        var kh = await _db.QueryFirstOrDefaultAsync<KhachHang>(
            "sp_GetKhachHangById", parameters, commandType: CommandType.StoredProcedure);

        return kh;
    }

}
