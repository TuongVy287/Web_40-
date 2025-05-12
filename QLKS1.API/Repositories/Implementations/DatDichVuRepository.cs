using System.Data;
using Dapper;
using QLKS1.API.Repositories.Interfaces;
public class DatDichVuRepository : IDatDichVuRepository
{
    private readonly IDbConnection _db;

    public DatDichVuRepository(IDbConnection db)
    {
        _db = db;
    }

    public async Task<bool> DatDichVuAsync(DatDichVu datDichVu)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@IDKhachHang", datDichVu.IDKhachHang);
        parameters.Add("@IDPhong", datDichVu.IDPhong);
        parameters.Add("@IDDichVu", datDichVu.IDDichVu);
        parameters.Add("@SoLan", datDichVu.SoLan);
        parameters.Add("@NgaySuDung", datDichVu.NgaySuDung);

        var result = await _db.ExecuteAsync("spAPI_DatDichVu_Add", parameters, commandType: CommandType.StoredProcedure);
        return result > 0;
    }

    public async Task<decimal> TinhTienDichVuAsync(int idKhachHang, int idPhong)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@IDKhachHang", idKhachHang);
        parameters.Add("@IDPhong", idPhong);

        return await _db.ExecuteScalarAsync<decimal>(
            "spAPI_DatDichVu_TinhTien", parameters, commandType: CommandType.StoredProcedure);
    }
}
