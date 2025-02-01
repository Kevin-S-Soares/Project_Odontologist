namespace Server.Models.Client;

public class ClientResetPassword
{
    public long UserId { get; set; }
    public string Hash { get; set; } = string.Empty;
    public Operation Operation { get; set; }
    public string Password { get; set; } = string.Empty;
}