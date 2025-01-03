using EmployeeShift_backend.Models;

namespace EmployeeShift_backend.Services.ServicesInterfaces;

public interface IShiftService
{
    //Method to get all possible combinations or options of shifts for employee registration
    public Task<Dictionary<string,List<string>>> GetShifts();
    
    //Method to get a shift by its id
    public Task<Shift?> GetShiftById(int shiftId);
}