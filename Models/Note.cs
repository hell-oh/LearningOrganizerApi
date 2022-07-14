using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace LearningOrganizerApi.Models;

public class Note 
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string Learning { get; set; } = null!;
    public string UserId { get; set; } = null!;
    public bool Public { get; set; }  = true;
}
