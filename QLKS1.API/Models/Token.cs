public class Token
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string JwtToken { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime ExpiresAt { get; set; }
}
