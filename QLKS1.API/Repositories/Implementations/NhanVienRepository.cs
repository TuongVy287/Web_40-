using System.Data;
using Dapper;
using QLKS1.API.Repositories.Interfaces;

public class NhanVienRepository : INhanVienRepository
{
    private readonly IDbConnection _db;

    public NhanVienRepository(IDbConnection db)
    {
        _db = db;
    }

    public async Task<IEnumerable<NhanVien>> GetAllAsync()
    {
        var result = await _db.QueryAsync<NhanVien>(
             "sp_GetAllNhanVien", commandType: CommandType.StoredProcedure);

        return result.ToList();
    }

    public async Task<List<NhanVien>> GetAllNhanVienAsync()
    {
        var result = await _db.QueryAsync<NhanVien>(
            "sp_GetAllNhanVien", commandType: CommandType.StoredProcedure);

        return result.ToList();
    }

    public Task<NhanVien> GetByIdAsync(int maNV)
    {
        throw new NotImplementedException();
    }

    public async Task<NhanVien> GetNhanVienByIdAsync(int maNV)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@MaNV", maNV);

        var nv = await _db.QueryFirstOrDefaultAsync<NhanVien>(
            "sp_GetNhanVienById", parameters, commandType: CommandType.StoredProcedure);

        return nv;
    }
}
