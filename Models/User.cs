using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace TimeControl.Models;

public class User : BaseModel
{
    public string Username { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string Surnames { get; set; } = null!;
    public string IdentificationNumber { get; set; } = null!;
    public string Phone { get; set; } = null!;
    public string Photo { get; set; } = null!;
    public Role? Role { get; set; }

    [BsonRepresentation(BsonType.ObjectId)]
    public string? EnterpriseId { get; set; }

}