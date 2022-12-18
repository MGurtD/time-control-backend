namespace TimeControl.Models;

public class Role : BaseModel
{
    public string Name { get; set; } = null!;
    public bool IsSuperAdmin { get; set; } = false;
}