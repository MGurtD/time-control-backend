namespace TimeControl.Dtos;

public class LoginForm
{
    public string Username { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string? Enterprise { get; set; }

}

public enum LoginFormResult {
    Ok,
    NotFound,
    NotFoundInEnterpise,
    IncorrectPassword
}