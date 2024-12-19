using EmployeeShift_backend.Data;
using EmployeeShift_backend.Services.ServicesInterfaces;
using Microsoft.EntityFrameworkCore;

namespace EmployeeShift_backend.Services;

public class ShiftService : IShiftService
{
    private readonly EmployeeShiftDbContext _context;

    public ShiftService(EmployeeShiftDbContext context)
    {
        _context = context;
    }

    public async Task<Dictionary<string,List<string>>?> GetShifts()
    {
        //This returns a list of all distinct shift types in the shift table
        var shiftTypes = await _context.Shifts
            .Select(shift => shift.ShiftType) 
            .Distinct() 
            .ToListAsync(); 
        
        //This returns a list of all distinct shift days in the shift table
        var shiftDays = await _context.Shifts
            .Select(shift =>shift.ShiftDays )
            .Distinct()
            .ToListAsync();
        
        //This returns a list of all distinct shift hours in the shift table
        var shiftTimes= await _context.Shifts
            .Select(shift => shift.ShiftHours)
            .Distinct()
            .ToListAsync();

        var employeePositions = await _context.Employees
            .Select(employee => employee.Position)
            .Distinct()
            .ToListAsync();
        
        
        if (!shiftTypes.Any() && !shiftDays.Any() && !shiftTimes.Any())
        {
            return null;
        }

        //If there are no employee positions, add some default positions
        if (employeePositions.Count == 0)
        {
            employeePositions.Add("Developer");
            employeePositions.Add("Team Lead");
            employeePositions.Add("Designer");
        }

        return new Dictionary<string, List<string>>
        {
            {"Employee Positions", employeePositions},
            {"Shift Types", shiftTypes},
            {"Shift Days", shiftDays},
            {"Shift Times", shiftTimes}
        };
    }
    
}