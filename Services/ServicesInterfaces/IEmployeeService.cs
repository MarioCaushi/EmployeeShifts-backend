using EmployeeShift_backend.DTOs;
using EmployeeShift_backend.Models;

namespace EmployeeShift_backend.Services.ServicesInterfaces;

public interface IEmployeeService
{
    //Method to get a specific employee based on their id
    public Task<Employee?> GetEmployeeById(int employeeId);
    
    //Method to edit an employees info
    public Task<bool> EditEmployee(EditEmployeeDTO employee);
    
    //Method to add an employee 
    public Task<bool> AddEmployee(AddEmployeeDTO employee);
    
    //Method to delete an employee
    public Task<bool> DeleteEmployee(int employeeId);
    
}