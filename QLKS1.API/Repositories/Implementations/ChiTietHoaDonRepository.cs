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

     public async Task<IEnumerable<ChiTietHoaDon>> GetChiTietHoaDonByIdAsync(int maHoaDon)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@IDHoaDon", maHoaDon);

        var chiTietHoaDons = await _db.QueryAsync<ChiTietHoaDon>(
            "spAPI_ChiTietHoaDon_Select", 
            parameters, 
            commandType: CommandType.StoredProcedure);

        return chiTietHoaDons;
    }


   public async Task<int> ThemChiTietHoaDonAsync(ChiTietHoaDon chiTietHoaDon)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@IDHoaDon", chiTietHoaDon.IDHoaDon);
        parameters.Add("@IDDichVu", chiTietHoaDon.IDDichVu);
        parameters.Add("@SoLuong", chiTietHoaDon.SoLuong);
        parameters.Add("@DVT", chiTietHoaDon.DVT);

        var result = await _db.ExecuteAsync(
            "spAPI_ChiTietHoaDon_Them",
            parameters,
            commandType: CommandType.StoredProcedure
        );

        return result; // Trả về số lượng dòng bị ảnh hưởng
    }

    
}
