using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace TimeControl.Models;

public class TimePeriod : BaseModel
{
    [BsonRepresentation(BsonType.ObjectId)]
    public string UserId { get; set; } = null!;

}