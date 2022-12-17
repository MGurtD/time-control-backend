namespace TimeControl.Models;

public class Enterprise : BaseModel
{
    public string Name { get; set; } = null!;
    public string Logo { get; set; } = null!;
    public string Address { get; set; } = null!;
    public string PhoneNumber { get; set; } = null!;

}