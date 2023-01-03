using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace TimeControl.Models;

public class LoginForm
{
    public string Username { get; set; } = null!;
    public string Password { get; set; } = null!;

    [BsonRepresentation(BsonType.ObjectId)]
    public string? Enterprise { get; set; }

}

public enum LoginFormResult {
    Ok,
    NotFound,
    NotFoundInEnterpise,
    IncorrectPassword
}