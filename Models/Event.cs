namespace EmployeeShift_backend.Models;

public class Event
{
    public int EventId { get; set; }

    public string Title { get; set; } = null!;

    public string? Description { get; set; }

    public DateTime EventDate { get; set; }

    public TimeSpan EventTime { get; set; }

    public int? EmployeeId { get; set; }

    public Employee? Employee { get; set; }
}
