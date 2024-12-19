namespace EmployeeShift_backend.DTOs;

public class InsightsEventDTO
{
    public int? EmployeeId { get; set; }
    public string? EmployeeFullName { get; set; }
    
    public string? EventTitle { get; set; }
    public string? EventDescription { get; set; }
    public string? EventDate { get; set; }
    public string? EventTime { get; set; }
    
}