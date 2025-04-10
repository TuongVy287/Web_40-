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

    public async Task<List<NhanVien>> GetAllNhanVienAsync()
    {
        var result = await _db.QueryAsync<NhanVien>(
            "spAPI_NhanVien_GetAll", commandType: CommandType.StoredProcedure);

        return result.ToList();
    }
    public async Task<bool> CreateNhanVienAsync(NhanVienCreateDTO dto)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@HoTen", dto.HoTen);
        parameters.Add("@GioiTinh", dto.GioiTinh);
        parameters.Add("@ChucDanh", dto.ChucDanh);
        parameters.Add("@SoDienThoai", dto.SoDienThoai);
        parameters.Add("@Email", dto.Email);
        parameters.Add("@CaLamViec", dto.CaLamViec);
        parameters.Add("@QuyenTruyCap", dto.QuyenTruyCap);
        parameters.Add("@NgayTao", dto.NgayTao ?? DateTime.Now);
        parameters.Add("@Luong", dto.Luong);
        parameters.Add("@TaiKhoan", dto.TaiKhoan);
        parameters.Add("@MatKhau", dto.MatKhau);
        parameters.Add("@Xoa", dto.Xoa);

        var result = await _db.ExecuteAsync("sp_InsertNhanVien", parameters, commandType: CommandType.StoredProcedure);
        return result > 0;
    }

    public async Task<bool> UpdateNhanVienAsync(NhanVienUpdateDTO dto)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@MaNhanVien", dto.MaNhanVien);
        parameters.Add("@HoTen", dto.HoTen);
        parameters.Add("@GioiTinh", dto.GioiTinh);
        parameters.Add("@ChucDanh", dto.ChucDanh);
        parameters.Add("@SoDienThoai", dto.SoDienThoai);
        parameters.Add("@Email", dto.Email);
        parameters.Add("@CaLamViec", dto.CaLamViec);
        parameters.Add("@QuyenTruyCap", dto.QuyenTruyCap);
        parameters.Add("@NgayTao", dto.NgayTao ?? DateTime.Now);
        parameters.Add("@Luong", dto.Luong);
        parameters.Add("@TaiKhoan", dto.TaiKhoan);
        parameters.Add("@MatKhau", dto.MatKhau);
        parameters.Add("@Xoa", dto.Xoa);

        var result = await _db.ExecuteAsync("sp_UpdateNhanVien", parameters, commandType: CommandType.StoredProcedure);
        return result > 0;
    }

}
