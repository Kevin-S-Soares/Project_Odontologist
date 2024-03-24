using Microsoft.EntityFrameworkCore;

namespace Server.Models;

[Index(nameof(Hash), nameof(UserId), nameof(Operation))]
public class HashStorage
{
    public Guid Id { get; set; }
    public string Hash { get; set; } = string.Empty;
    public Guid UserId { get; set; }
    public User User { get; set; } = default!;
    public Operation Operation { get; set; }
    public DateTime CreatedAt { get; set; }
    public string? Details { get; set; }
}