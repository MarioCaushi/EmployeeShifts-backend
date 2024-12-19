namespace EmployeeShift_backend.DTOs;

public class ViewAllEmployeesDTO
{
    public int EmployeeId { get; set; }

    public string Name { get; set; } = null!;

    public string LastName { get; set; } = null!;
    
    public string Position { get; set; } = null!;

    public string Status { get; set; } = null!;

    public string Type { get; set; } = null!;
}