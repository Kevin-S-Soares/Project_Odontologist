namespace Server.Models;

public class User
{
    public long Id { get; set; } = 1L;
    public string Name { get; set; } = string.Empty;
    public string NormalizedName { get; set; } = string.Empty;
    public string ProfilePictureUrl { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public DateTime LastLogin { get; set; }
    public DateTime? VerifiedAt { get; set; } = null;
    public Role Role { get; set; }
    public long? ContextId { get; set; }
}