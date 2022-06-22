using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace LearningOrganizerApi.Models;

public class Learning
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }

    public string ContentName { get; set; } = null!;
    public string Category { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string Link { get; set; } = null!;
    public int Priority { get; set; } = 0;
    public int Difficulty { get; set; } = 0;
    public string EstimatedTime { get; set; } = null!;
    public bool Completed { get; set; } = false;

    // collección de notas
    // [BsonElement]
    // [JsonPropertyName("Notes")]
    // public List<string> notes { get; set; } = null!;



    // [BsonId]
    // [BsonRepresentation(BsonType.ObjectId)]
    // public string Id { get; set; }
    // public string Title { get; set; }
    // public string Author { get; set; }
    // public string Genre { get; set; }
    // public string Description { get; set; }
    // public string Image { get; set; }
    // public string Link { get; set; }
    // public string ISBN { get; set; }
    // public string Publisher { get; set; }
    // public string Language { get; set; }
    // public string Format { get; set; }
    // public string Pages { get; set; }
    // public string Year { get; set; }
    // public string Rating { get; set; }
    // public string RatingCount { get; set; }
    // public string Downloads { get; set; }
    // public string DownloadsCount { get; set; }
    // public string Price { get; set; }


}