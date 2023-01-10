using TimeControl.Models;

namespace TimeControl.Dtos;

public class TimePeriodReadDto
{
    public TimePeriodReadDto(TimePeriod timePeriod)
    {
        this.Id = timePeriod.Id;
        this.CreatedOn = timePeriod.CreatedOn.HasValue ? timePeriod.CreatedOn.Value : DateTime.Now;
        this.UpdatedAt = timePeriod.UpdatedAt;
        this.UserId = timePeriod.UserId;
    }

    public string? Id { get; set; }
    public DateTime CreatedOn { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public string? UserId { get; set; }

    public string MonthId
    {
        get {
            return $"{CreatedOn.ToString("MM/yyyy")}";
        }
    }

    public string DayId
    {
        get {
            return $"{CreatedOn.ToString("dd/MM/yyyy")}";
        }
    }

    public int Seconds
    {
        get {
            if (UpdatedAt.HasValue) {
                var difference = UpdatedAt.Value - CreatedOn;
                return ((int)difference.TotalSeconds);
            }
            else {
                return 0;
            }
        }
    }

}