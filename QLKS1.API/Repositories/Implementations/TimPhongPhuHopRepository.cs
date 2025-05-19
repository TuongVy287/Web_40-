using System.Data;
using Dapper;
using QLKS1.API.Repositories.Interfaces;


public class TimPhongPhuHopRepository : ITimPhongPhuHopRepository
{
    private readonly IDbConnection _db;

    public TimPhongPhuHopRepository(IDbConnection db)
    {
        _db = db;
    }

   public async Task<IEnumerable<PhongPhuHop>> TimPhongAsync(DateTime checkIn, DateTime checkOut, int adults, int children)
{
    var parameters = new DynamicParameters();
    parameters.Add("@CheckIn", checkIn);
    parameters.Add("@CheckOut", checkOut);
    parameters.Add("@Adults", adults);
    parameters.Add("@Children", children);

    var result = await _db.QueryAsync<PhongPhuHop>(
        "sp_TimPhongPhuHop",
        parameters,
        commandType: CommandType.StoredProcedure
    );

    return result;
}
}
