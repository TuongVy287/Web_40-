using System.Data;
using Dapper;
using System.Data.SqlClient;
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
    // public async Task<bool> CreateNhanVienAsync(NhanVienCreateDTO dto)
    // {
    //     var parameters = new DynamicParameters();
    //     parameters.Add("@HoTen", dto.HoTen);
    //     parameters.Add("@GioiTinh", dto.GioiTinh);
    //     parameters.Add("@ChucDanh", dto.ChucDanh);
    //     parameters.Add("@SoDienThoai", dto.SoDienThoai);
    //     parameters.Add("@Email", dto.Email);
    //     parameters.Add("@CaLamViec", dto.CaLamViec);
    //     parameters.Add("@QuyenTruyCap", dto.QuyenTruyCap);
    //     parameters.Add("@NgayTao", dto.NgayTao ?? DateTime.Now);
    //     parameters.Add("@Luong", dto.Luong);
    //     parameters.Add("@TaiKhoan", dto.TaiKhoan);
    //     parameters.Add("@MatKhau", dto.MatKhau);
    //     parameters.Add("@Xoa", dto.Xoa);

    //     var result = await _db.ExecuteAsync("sp_InsertNhanVien", parameters, commandType: CommandType.StoredProcedure);
    //     return result > 0;
    // }

    public async Task<bool> SuaNhanVienAsync(NhanVien nhanVien)
    {
        try
        {
            var parameters = new DynamicParameters();
            parameters.Add("@MaNV", nhanVien.MaNV);
            parameters.Add("@HoTen", nhanVien.HoTen);
            parameters.Add("@GioiTinh", nhanVien.GioiTinh);
            parameters.Add("@ChucDanh", nhanVien.ChucDanh);
            parameters.Add("@SoDienThoai", nhanVien.SoDienThoai);
            parameters.Add("@Email", nhanVien.Email);
            parameters.Add("@CaLamViec", nhanVien.CaLamViec);
            parameters.Add("@Luong", nhanVien.Luong);

            await _db.ExecuteAsync(
                "spAPI_NhanVien_Sua",
                parameters,
                commandType: CommandType.StoredProcedure
            );

            return true;
        }
        catch (SqlException ex)
        {
            // Xử lý lỗi RAISERROR
            if (ex.Message.Contains("Không tìm thấy nhân viên"))
            {
                return false;
            }

            throw;
        }
    }
    public async Task<bool> XoaNhanVienAsync(string maNV)
    {
        try
        {
            var parameters = new DynamicParameters();
            parameters.Add("@MaNV", maNV);

            var rowsAffected = await _db.ExecuteAsync(
                "spAPI_NhanVien_Xoa",
                parameters,
                commandType: CommandType.StoredProcedure
            );

            return rowsAffected > 0;
        }
        catch (SqlException ex)
        {
            if (ex.Number == 50001) // Mã lỗi từ THROW
            {
                return false;
            }
            throw; // Nếu là lỗi khác, ném lỗi ra ngoài
        }


    }

    public async Task<NhanVien> GetByMaNVAsync(string maNV)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@MaNV", maNV.Trim());

        return await _db.QueryFirstOrDefaultAsync<NhanVien>(
            "spAPI_NhanVien_Login",
            parameters,
            commandType: CommandType.StoredProcedure
        );
    }

    public async Task<bool> ChangePasswordAsync(string maNV, string currentPassword, string newPassword)
    {
        var user = await GetByMaNVAsync(maNV);
        if (user == null) return false;

        if (!BCrypt.Net.BCrypt.Verify(currentPassword, user.MatKhau))
            return false;

        string hashedNewPassword = BCrypt.Net.BCrypt.HashPassword(newPassword);

        var parameters = new DynamicParameters();
        parameters.Add("@MaNV", maNV.Trim());
        parameters.Add("@MatKhauMoi", hashedNewPassword);

        var result = await _db.ExecuteScalarAsync(
            "spAPI_NhanVien_Update_MatKhau",
            parameters,
            commandType: CommandType.StoredProcedure
        );

        return Convert.ToBoolean(result);
    }





}
