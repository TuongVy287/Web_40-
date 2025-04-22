public interface IUserRepository
{
    Task<User> GetUserByIdAsync(int id);
    Task<string> GetUserRoleAsync(string username);
}
