namespace EmployeeShift_backend.Models;

public class Employee
{
    public int EmployeeId { get; set; }

    public string Name { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public DateOnly Birthday { get; set; }

    public string Email { get; set; } = null!;

    public string PhoneNumber { get; set; } = null!;

    public string Address { get; set; } = null!;

    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    
    public DateOnly HireDate { get; set; }

    public string Position { get; set; } = null!;

    public string Status { get; set; } = null!;
    
    public float PayPerHour { get; set; }

    public int ManagerId { get; set; }

    public int ShiftId { get; set; }

    public ICollection<Event> Events { get; set; } = new List<Event>();

    public Manager Manager { get; set; } = null!;

    public Shift Shift { get; set; } = null!;
}
