namespace EmployeeShift_backend.Models;

public class Shift
{
    public int ShiftId { get; set; }

    public string ShiftType { get; set; } = null!;

    public string ShiftDays { get; set; } = null!;

    public string ShiftHours { get; set; } = null!;

    public ICollection<Employee> Employees { get; set; } = new List<Employee>();
}
