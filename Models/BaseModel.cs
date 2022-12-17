using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace TimeControl.Models
{
    public class BaseModel
    {

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        public DateTime? CreatedOn { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}