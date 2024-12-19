using EmployeeShift_backend.Data;
using EmployeeShift_backend.DTOs;
using EmployeeShift_backend.Models;
using EmployeeShift_backend.Services.ServicesInterfaces;
using Microsoft.CodeAnalysis.Emit;
using Microsoft.EntityFrameworkCore;

namespace EmployeeShift_backend.Services;

public class ManagerService : IManagerService
{
    private readonly EmployeeShiftDbContext _context;

    public ManagerService(EmployeeShiftDbContext context)
    {
        _context = context;
    }

    public async Task<Manager?> getManagerByIdAsync(int managerId)
    {
        return await _context.Managers.FindAsync(managerId);
    }

    public async Task<ICollection<Manager>?> getAllManagersAsync()
    {
        return await _context.Managers.ToListAsync();
    }

    public async Task<int?> ManagerLoginAuthentication(string username, string password)
    {
        return await _context.Managers.Where(manager => manager.Username == username && manager.Password == password).Select(manager => manager.ManagerId).FirstOrDefaultAsync();
    }

    public async Task<bool> EditManagerInfo(int managerId, ManagerDTO managerDTO)
    {
        var manager = await _context.Managers.FindAsync(managerId);
        
        if (managerDTO == null ||  manager == null)
        {
            return false;
        }
            var password = managerDTO.Password;

            if (password.Trim().ToLower() == "" || password.Length < 8 ||
                password == manager.Password)
            {
                return false;
            }
                manager.Password = password;
                
                manager.Name=managerDTO.Name;
                manager.LastName=managerDTO.LastName;
                manager.Birthday=managerDTO.Birthday;
                manager.Email=managerDTO.Email;
                manager.PhoneNumber=managerDTO.PhoneNumber;
                manager.Username=managerDTO.Username;
                manager.Address=managerDTO.Address;
                
                _context.Managers.Update(manager);
                await _context.SaveChangesAsync();
                
            return true;
        }

    public async Task<ICollection<ViewAllEmployeesDTO>?> GetEmployeesByManagerId(int managerId)
    {
        return await _context.Employees
            .Where(employee => employee.ManagerId == managerId).Include(e => e.Shift )
            .Select(employee => new ViewAllEmployeesDTO 
            {
                EmployeeId = employee.EmployeeId,
                Name = employee.Name,
                LastName = employee.LastName,
                Position = employee.Position,
                Status = employee.Status,
                Type = employee.Shift.ShiftType
            })
            .ToListAsync();
    }
    
}

