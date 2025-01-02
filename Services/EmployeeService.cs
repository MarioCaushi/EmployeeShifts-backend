using EmployeeShift_backend.Data;
using EmployeeShift_backend.DTOs;
using EmployeeShift_backend.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeeShift_backend.Services.ServicesInterfaces;

public class EmployeeService : IEmployeeService
{
    private readonly EmployeeShiftDbContext _context;

    public EmployeeService(EmployeeShiftDbContext context, IEventService eventService)
    {
        _context = context;
    }

    public async Task<Employee?> GetEmployeeById(int employeeId)
    {
        return await _context.Employees
            .Include(e => e.Shift)  // Include the Shift data in the query
            .SingleOrDefaultAsync(e => e.EmployeeId == employeeId);
    }


    public async Task<bool> DeleteEmployee(int employeeId)
    {
        var employee = await _context.Employees.FindAsync(employeeId);

        if (employee == null)
        {
            return false;
        }
        
        _context.Employees.Remove(employee);
        await _context.SaveChangesAsync();

        return true; 
    }

    public async Task<bool> AddEmployee(AddEmployeeDTO employeeToAdd)
    {
        if (employeeToAdd == null)
        {
            return false;
        }

        bool exists = false;

        int randomNumber = 0; //Used to generate a random number for the employee id
        
        Random rnd = new Random();
        
        do
        {
            randomNumber = rnd.Next(10000000, 100000000);  // Generate a random number between 10,000,000 and 99,999,999
            var employee = await _context.Employees.FindAsync(randomNumber); 
    
            if (employee == null)
            {
                exists = true;  
            }
        }
        while (!exists); 
        
        DateOnly currentDayOnly = DateOnly.FromDateTime(DateTime.Today);
        string status = "Not Active";

        var matchingShiftId = _context.Shifts
            .Where(shift => shift.ShiftType == employeeToAdd.ShiftType
                            && shift.ShiftDays == employeeToAdd.ShiftDays
                            && shift.ShiftHours == employeeToAdd.ShiftHours)
            .Select(shift => shift.ShiftId)
            .FirstOrDefault();
        
        if(matchingShiftId == null)
            return false;

        Employee employeeAdded = new Employee()
        {
            EmployeeId = randomNumber,
            Name = employeeToAdd.Name,
            LastName = employeeToAdd.LastName,
            HireDate = currentDayOnly,
            Position = employeeToAdd.Position,
            Status = status,
            Birthday = employeeToAdd.Birthday,
            Email = employeeToAdd.Email,
            PhoneNumber = employeeToAdd.PhoneNumber,
            Address = employeeToAdd.Address,
            ManagerId = employeeToAdd.ManagerId,
            ShiftId = matchingShiftId,
            PayPerHour = employeeToAdd.PayPerHour
        };
        
        await _context.Employees.AddAsync(employeeAdded);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> EditEmployee(EditEmployeeDTO employeeToEdit)
    {
        if(employeeToEdit == null)
            return false;
        
        var matchingShiftId = _context.Shifts
            .Where(shift => shift.ShiftType == employeeToEdit.ShiftType
                            && shift.ShiftDays == employeeToEdit.ShiftDays
                            && shift.ShiftHours == employeeToEdit.ShiftHours)
            .Select(shift => shift.ShiftId)
            .FirstOrDefault();
        
        if(matchingShiftId == null)
            return false;
        
        var employee = await _context.Employees.FindAsync(employeeToEdit.EmployeeId);

        if (employee == null)
            return false;

        employee.Name = employeeToEdit.Name;
        employee.LastName = employeeToEdit.LastName;
        employee.Birthday = employeeToEdit.Birthday;
        employee.Email = employeeToEdit.Email;
        employee.PhoneNumber = employeeToEdit.PhoneNumber;
        employee.Address = employeeToEdit.Address;
        employee.Position = employeeToEdit.Position;
        employee.ShiftId = matchingShiftId;
        employee.PayPerHour = employeeToEdit.PayPerHour;
        
        _context.Employees.Update(employee);
        await _context.SaveChangesAsync();
        return true;
    }
    
}