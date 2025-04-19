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

    // public async Task<HoaDon> CreateAsync(HoaDon hoaDon)
    // {
    //     _db.HoaDons.Add(hoaDon);
    //     await _db.SaveChangesAsync();
    //     return hoaDon;
    // }

    // public async Task<HoaDon?> UpdateAsync(int id, HoaDon hoaDon)
    // {
    //     var existing = await _db.HoaDons.FindAsync(id);
    //     if (existing == null) return null;

    //     existing.IDKhachHang = hoaDon.IDKhachHang;
    //     existing.IDDatPhong = hoaDon.IDDatPhong;
    //     existing.TongTien = hoaDon.TongTien;
    //     existing.TrangThai = hoaDon.TrangThai;
    //     existing.PhuongThucThanhToan = hoaDon.PhuongThucThanhToan;
    //     existing.NgayTT = hoaDon.NgayTT;

    //     await _db.SaveChangesAsync();
    //     return existing;
    // }

    // public async Task<bool> DeleteAsync(int id)
    // {
    //     var hoaDon = await _db.HoaDons.FindAsync(id);
    //     if (hoaDon == null) return false;

    //     _db.HoaDons.Remove(hoaDon);
    //     await _db.SaveChangesAsync();
    //     return true;
    // }
}
