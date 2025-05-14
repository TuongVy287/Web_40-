using System.Data;
using Dapper;
using QLKS1.API.Repositories.Interfaces;
using System.Data.SqlClient;



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



    public async Task<BookingResponse> CreateBookingAsync(DatPhongRequest request)
    {

        var parameters = new DynamicParameters();


        parameters.Add("@MaKH", request.MaKH);
        parameters.Add("@TenPhong", request.TenPhong);
        parameters.Add("@NgayNhan", request.NgayNhan);
        parameters.Add("@NgayTra", request.NgayTra);
        parameters.Add("@SoLuongNguoi", request.SoLuongNguoi);

        var result = await _db.QueryFirstOrDefaultAsync<BookingResponse>(
            "spAPI_DatPhong_Them",
            parameters,
            commandType: CommandType.StoredProcedure
        );

        return result;

    }

    public async Task<BookingResponse?> CancelBookingAsync(int idDatPhong)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@IDDatPhong", idDatPhong);

        return await _db.QueryFirstOrDefaultAsync<BookingResponse>(
            "spAPI_HuyDatPhong",
            parameters,
            commandType: CommandType.StoredProcedure
        );
    }
    public async Task<IEnumerable<DatPhongTheoThang>> GetDatPhongTheoThangAsync(string thangChu)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@ThangChu", thangChu);

        var result = await _db.QueryAsync<DatPhongTheoThang>(
            "spAPI_GetDatPhongTheoThang",
            parameters,
            commandType: CommandType.StoredProcedure
        );

        return result;
    }


}

