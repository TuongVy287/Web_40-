using System.Data;
using Dapper;
using QLKS1.API.Repositories.Interfaces;

public class CheckinRepository : ICheckinRepository
{
    private readonly IDbConnection _db;

    public CheckinRepository(IDbConnection db)
    {
        _db = db;
    }

    public IEnumerable<CheckinList> GetGuestCheckins()
    {
        var sql = "EXEC dbo.spAPI_Guest_Checkin";
        return _db.Query<CheckinList>(sql);
    }

}
