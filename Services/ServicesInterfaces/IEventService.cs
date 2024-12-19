using EmployeeShift_backend.Models;
using EmployeeShift_backend.DTOs;

namespace EmployeeShift_backend.Services.ServicesInterfaces;

public interface IEventService
{
    //Method to get events of a certain employee
    public Task<ICollection<Event>?> GetEventsByEmployeeId(int employeeId);
     
    //Method to get events based on a filtered search
    public Task<ICollection<InsightsEventDTO>?> GetEventsByFilter(bool all, InsightsEventDTO filter ); 
        
    //Method to add an event for a specific employee
    public Task<bool> AddEvent(AddEventDTO eventToAdd);
    
    //Method to delete an event by event id 
    public Task<bool> DeleteEventById(int eventId); //This method will exist even though it is not used in the frontend
    
    //Method to delete all events of a particular employee
    public Task<bool> DeleteEventsByEmployeeId(int employeeId); //This happens when you delete an employee
}