using System.Text.Json.Serialization;

namespace Domain.Entities;

public class Product
{
    [JsonPropertyName("id")]
    public int Id { get; set; }
    [JsonPropertyName("title")]
    public string Title { get; set; } = null!;
    [JsonPropertyName("price")]
    public decimal Price { get; set; }
    [JsonPropertyName("description")]
    public string Description { get; set; } = null!;
    [JsonPropertyName("category")]
    public string Category { get; set; } = null!;
    [JsonPropertyName("image")]
    public string Image { get; set; } = null!;
    [JsonPropertyName("rate")]
    public double Rate { get; set; }
    [JsonPropertyName("count")]
    public int Count { get; set; }
}