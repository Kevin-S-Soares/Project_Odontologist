namespace Server.Models.Client;

public class ClientHashOperation
{
    public long UserId { get; set; }
    public string Hash { get; set; } = string.Empty;
    public Operation Operation { get; set; }
}