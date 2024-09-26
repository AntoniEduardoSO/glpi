namespace GLPI.Core.Models;
public class Ticket
{
    public long Id { get; set; }
    public string Title { get; set; } = string.Empty;

    public long CategoryId { get; set; }
    public Category  Category{ get; set; } = null!;

    public int StatusId { get; set; }
    public Status Status { get; set; } = null!;

    public DateTime CreatedAt { get; set; } = DateTime.Now;

    public DateTime UpdatedAt { get; set; }

    public string UserId { get; set; } = string.Empty;
}