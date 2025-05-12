using System.Data;
using Dapper;
using QLKS1.API.Repositories.Interfaces;

public class HoaDonRepository : IHoaDonRepository
{
    private readonly IDbConnection _db;

    public HoaDonRepository(IDbConnection db)
    {
        _db = db;
    }

    public async Task<IEnumerable<HoaDon>> GetAllHoaDon()
    {
        var hoadonList = (await _db.QueryAsync<HoaDon>(
        "sp_GetAllPhong", commandType: CommandType.StoredProcedure)).ToList();

        return hoadonList;
    }

    public async Task<HoaDon?> GetHoaDonByIdAsync(int IDHoaDon)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@IDHoaDon", IDHoaDon);

        var hoaDon = await _db.QueryFirstOrDefaultAsync<HoaDon>(
            "sp_GetHoaDonById", parameters, commandType: CommandType.StoredProcedure);

        return hoaDon;
    }
    public async Task<HoaDonResponse?> CreateInvoiceAsync(HoaDonRequest request)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@IDKhachHang", request.IDKhachHang);
        parameters.Add("@IDDatPhong", request.IDDatPhong);
        parameters.Add("@IDPhong", request.IDPhong);
        parameters.Add("@PhuongThucThanhToan", request.PhuongThucThanhToan);

        return await _db.QueryFirstOrDefaultAsync<HoaDonResponse>(
            "spAPI_HoaDon_Them",
            parameters,
            commandType: CommandType.StoredProcedure
        );
    }


}
