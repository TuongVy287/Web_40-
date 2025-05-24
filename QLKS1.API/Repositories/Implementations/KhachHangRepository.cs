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

    public async Task<KhachHangDatPhongViewModel> GetChiTietKhachHangAsync(int idKhachHang, int idDatPhong)
    {
        var result = await _db.QueryFirstOrDefaultAsync<KhachHangDatPhongViewModel>(
            "spAPI_KhachHang_Select",
            new { IDKhachHang = idKhachHang, IDDatPhong = idDatPhong },
            commandType: CommandType.StoredProcedure
        );

        return result;
    }

    public IEnumerable<KhachHang> GetGuestCheckins()
    {
        var sql = "EXEC dbo.spAPI_Guest_Checkin";
        return _db.Query<KhachHang>(sql);
    }

    

    public async Task<IEnumerable<GuestInfo>> GetGuestInfoAsync()
    {
        var result = await _db.QueryAsync<GuestInfo>(
            "spAPI_KhachHang_Select_Guest",
            commandType: CommandType.StoredProcedure
        );

        return result;
    }

    
    public async Task<int> ThemKhachHang(KhachHang khachHang)
{
    var parameters = new DynamicParameters();
    parameters.Add("@HoTen", khachHang.HoTen);
    parameters.Add("@GioiTinh", khachHang.GioiTinh);
    parameters.Add("@NgaySinh", khachHang.NgaySinh);
    parameters.Add("@SoDienThoai", khachHang.SoDienThoai);
    parameters.Add("@Email", khachHang.Email);
    parameters.Add("@CCCD", khachHang.CCCD);
    parameters.Add("@DiaChi", khachHang.DiaChi);

    var result = await _db.ExecuteAsync(
        "spAPI_KhachHang_Them",
        parameters,
        commandType: CommandType.StoredProcedure
    );

    return result; // Có thể trả về số lượng dòng bị ảnh hưởng
}
}
