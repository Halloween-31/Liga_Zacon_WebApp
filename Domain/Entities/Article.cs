namespace Domain.Entities;

public class Article
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public string? Text { get; set; }
    public DateTime CreatedAt { get; set; }
    public bool IsPublished { get; set; }
    public string? Tag { get; set; }
}

