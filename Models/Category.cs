using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace LearningOrganizerApi.Models;

public class Category
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }
    public string Name { get; set; } = null!;
    public string? BelongsTo { get; set; } = null!;
}
// var example = 
// {
//     "Id": "5e8f8f8f8f8f8f8f8f8f8f8f",
//     "Name": "Category 1",
//     "Description": "This is a category",
//     "Image": "link de imagen",
//     "RelatedCategories": ["5e8f8f8f8f8f8f8f8f8f8f8f", "5e8f8f8f8f8f8f8f8f8f8f8f"]
// }