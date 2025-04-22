using System.Data;
using Dapper;
using QLKS1.API.Repositories.Interfaces;

public class TokenRepository : ITokenRepository
{
    private readonly IDbConnection _db;

    public TokenRepository(IDbConnection db)
    {
        _db = db;
    }

    public async Task SaveTokenAsync(Token token)
    {
        var sql = @"INSERT INTO Tokens (UserId, Token, CreatedAt, ExpiresAt)
                    VALUES (@UserId, @JwtToken, @CreatedAt, @ExpiresAt)";
        await _db.ExecuteAsync(sql, token);
    }
}
