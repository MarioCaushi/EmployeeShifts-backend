namespace EmployeeShift_backend.DTOs;

public class AddEmployeeDTO
{
    public string Name { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public DateOnly Birthday { get; set; }

    public string Email { get; set; } = null!;

    public string PhoneNumber { get; set; } = null!;

    public string Address { get; set; } = null!;
    
    public string Position { get; set; } = null!;
    
    public string ShiftType { get; set; } = null!;
    public string ShiftDays { get; set; } = null!;
    public string ShiftHours { get; set; } = null!;
    
    public float PayPerHour { get; set; }
    
    public int ManagerId { get; set; }

}
