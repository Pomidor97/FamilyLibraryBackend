using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

public class FamilyMetadata
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; } = string.Empty;

    [BsonElement("FamilyName")]
    public string FamilyName { get; set; } = string.Empty;

    public string Category { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public string FileName { get; set; } = string.Empty;
    public string FilePath { get; set; } = string.Empty;
}