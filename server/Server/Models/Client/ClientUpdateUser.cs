namespace Server.Models.Client;

public class ClientUpdateUser
{
    public long Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string ProfilePictureUrl { get; set; } = string.Empty;
    public Role Role { get; set; }
}