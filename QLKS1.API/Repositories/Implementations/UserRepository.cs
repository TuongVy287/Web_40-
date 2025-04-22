using System.Data;
using Dapper;

public class UserRepository : IUserRepository
{
    private readonly IDbConnection _db;

    public UserRepository(IDbConnection db)
    {
        _db = db;
    }

    public async Task<User> GetUserByIdAsync(int id)
    {
        var param = new DynamicParameters();
        param.Add("@Id", id);

        var user = await _db.QueryFirstOrDefaultAsync<User>(
            "sp_GetUserById", param, commandType: CommandType.StoredProcedure);

        return user;
    }

    public async Task<string> GetUserRoleAsync(string username)
    {
        var param = new DynamicParameters();
        param.Add("@Username", username);

        var role = await _db.QueryFirstOrDefaultAsync<string>(
            "sp_GetUserRole", param, commandType: CommandType.StoredProcedure);

        return role;
    }
}
