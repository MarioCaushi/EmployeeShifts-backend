using EmployeeShift_backend.DTOs;
using EmployeeShift_backend.Models;

namespace EmployeeShift_backend.Services.ServicesInterfaces;

public interface IManagerService
{
     //Get Manager with a specific ID
     public Task<Manager?> getManagerByIdAsync(int managerId);
     
     //Get a list of all Managers
     public Task<ICollection<Manager>?> getAllManagersAsync();
     
     //Function to deal with Manager Log-in Authentication
     public Task<int?> ManagerLoginAuthentication(string username, string password);

     //Edit personal info of manager
     public Task<bool> EditManagerInfo(int managerId, ManagerDTO managerDTO);
     
     //Get a list of employees managed by a specifc manager
     public Task<ICollection<ViewAllEmployeesDTO>?> GetEmployeesByManagerId(int managerId);
}