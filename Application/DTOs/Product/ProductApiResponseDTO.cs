using System.Text.Json.Serialization;

namespace Liga_Zacon_WebApp.ApiContracts.Product;

public class ProductApiResponseDTO
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
    [JsonPropertyName("rating")]
    public Rating Rating { get; set; } = null!;

    public Domain.Entities.Product ToEntity()
    {
        return new Domain.Entities.Product()
        {
            Title = Title,
            Price = Price,
            Description = Description,
            Category = Category,
            Image = Image,
            Rate = Rating.Rate,
            Count = Rating.Count,
        };
    }
}

public class Rating
{
    [JsonPropertyName("rate")]
    public double Rate { get; set; }
    [JsonPropertyName("count")]
    public int Count { get; set; }
}

/*
"id": 1,
"title": "Fjallraven - Foldsack No. 1 Backpack, Fits 15 Laptops",
"price": 109.95,
"description": "Your perfect pack for everyday use and walks in the forest. Stash your laptop (up to 15 inches) in the padded sleeve, your everyday",
"category": "men's clothing",
"image": "https://fakestoreapi.com/img/81fPKd-2AYL._AC_SL1500_t.png",
"rating": {
 "rate": 3.9,
 "count": 120
}
*/