namespace EmployeeShift_backend.DTOs;

public class AddEventDTO
{
    public int EmployeeId { get; set; }
     
    public string EmployeeStatus { get; set; }
    public string EventTitle { get; set; }
    
    public DateTime EventDate { get; set; } // Only the date
    public TimeSpan EventTime { get; set; } // Only the time
}