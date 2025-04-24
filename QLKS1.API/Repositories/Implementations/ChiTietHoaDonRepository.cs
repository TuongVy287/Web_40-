// using System.Data;
// using Dapper;
// using QLKS1.API.Repositories.Interfaces;


<<<<<<< HEAD
public class ChiTietHoaDonRepository : IChiTietHoaDonRepository
{
    private readonly IDbConnection _db;
=======
// public class ChiTietHoaDonRepository : IDatPhongRepository
// {
//     private readonly IDbConnection _db;
>>>>>>> d5525ff5ce4a394c36053099ff9a1a84f60265f3

//     public ChiTietHoaDonRepository(IDbConnection db)
//     {
//         _db = db;
//     }

//     public async Task<IEnumerable<ChiTietHoaDon>> GetAllChiTietHoaDon()
//     {
//         var chitiethoadonList = (await _db.QueryAsync<ChiTietHoaDon>(
//         "sp_GetAllChiTietHoaDon", commandType: CommandType.StoredProcedure)).ToList();

//         return chitiethoadonList;
//     }

//     public async Task<ChiTietHoaDon?> GetChiTietHoaDonByIdAsync(int MaHoaDon)
//     {
//         var parameters = new DynamicParameters();
//         parameters.Add("@MaHoaDon", MaHoaDon);

//         var chiTietHoaDon = await _db.QueryFirstOrDefaultAsync<ChiTietHoaDon>(
//             "sp_GetChiTietHoaDonById", parameters, commandType: CommandType.StoredProcedure);

//         return chiTietHoaDon;
//     }
// }
