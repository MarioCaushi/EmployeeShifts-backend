using EmployeeShift_backend.Data;
using EmployeeShift_backend.DTOs;
using EmployeeShift_backend.Models;
using EmployeeShift_backend.Services.ServicesInterfaces;
using Microsoft.EntityFrameworkCore;

namespace EmployeeShift_backend.Services;

public class EventService : IEventService
{
    private readonly EmployeeShiftDbContext _context;
    
    public EventService(EmployeeShiftDbContext context)
        {
        _context = context;
        }

    public async Task<bool> AddEvent(AddEventDTO eventToAdd)
    {
        if (eventToAdd == null)
            return false;
        
        var employee = await _context.Employees.FindAsync(eventToAdd.EmployeeId);
        var employeeShift = await _context.Shifts.FindAsync(employee.ShiftId);
        
        if (employee == null || employeeShift == null)
            return false;
        
        var NewStatus = eventToAdd.EmployeeStatus == "Active" ? "Not Active" : "Active";
        
        var shiftTimes = employeeShift.ShiftHours.Split('-');

        var NewDescription = "Late";
        
        if (eventToAdd.EventTitle == "Came to Work")
        {
            
            var startTimeHour = int.Parse(shiftTimes[0]);
            var startTimeMinute = 0;
            var startTimeSeconds = 0;
        
            // Compare event time with start time
            if (eventToAdd.EventTime.Hours < startTimeHour 
                || (eventToAdd.EventTime.Hours == startTimeHour && eventToAdd.EventTime.Minutes < startTimeMinute)
                || (eventToAdd.EventTime.Hours == startTimeHour && eventToAdd.EventTime.Minutes == startTimeMinute && eventToAdd.EventTime.Seconds < startTimeSeconds))
            {
                NewDescription = "Early";
            }
            else if(eventToAdd.EventTime.Hours == startTimeHour && eventToAdd.EventTime.Minutes == startTimeMinute)
            {
                NewDescription = "On Time";
            }
        }
        else
        {
            var endTimeHour = int.Parse(shiftTimes[1]);
            var endTimeMinute = 0;
            var endTimeSeconds = 0;
        
            // Compare event time with start time
            if (eventToAdd.EventTime.Hours < endTimeHour 
                || (eventToAdd.EventTime.Hours == endTimeHour && eventToAdd.EventTime.Minutes < endTimeMinute)
                || (eventToAdd.EventTime.Hours == endTimeHour && eventToAdd.EventTime.Minutes == endTimeMinute && eventToAdd.EventTime.Seconds < endTimeSeconds))
            {
                NewDescription = "Early";
            }
            else if(eventToAdd.EventTime.Hours == endTimeHour && eventToAdd.EventTime.Minutes == endTimeMinute)
            {
                NewDescription = "On Time";
            }
        }
        
        
        Event eventAdded = new Event()
        {
            EmployeeId = eventToAdd.EmployeeId,
            EventDate = eventToAdd.EventDate,
            EventTime = eventToAdd.EventTime,
            Title = eventToAdd.EventTitle,
            Description = NewDescription
        };
        
        _context.Events.Add(eventAdded);
        await _context.SaveChangesAsync();
        
        employee.Status = NewStatus;
        _context.Employees.Update(employee);
        await _context.SaveChangesAsync();
        
        return true;
    }

    public async Task<bool> DeleteEventsByEmployeeId(int employeeId)
    {
        var eventsToDelete = await _context.Events.Where(e => e.EmployeeId == employeeId).ToListAsync();
        
        if (!eventsToDelete.Any())
            return false;
        
        _context.Events.RemoveRange(eventsToDelete);
        
        await _context.SaveChangesAsync();
        return true;
    }
    
    public async Task<bool> DeleteEventById(int eventId)
    {
        var eventToDelete =  await _context.Events.FindAsync(eventId);

        if (eventToDelete == null)
            return false;
        _context.Events.Remove(eventToDelete);
          await _context.SaveChangesAsync();
        return true;
    }

    public async Task<ICollection<Event>?> GetEventsByEmployeeId(int employeeId)
    {
        return  await _context.Events.Where(e => e.EmployeeId == employeeId).ToListAsync();
    }
    
public async Task<ICollection<InsightsEventDTO>?> GetEventsByFilter(bool all, InsightsEventDTO filter)
{
    // Check if the filter is null when all is false
    if (!all && filter == null)
    {
        throw new ArgumentNullException(nameof(filter), "Filter cannot be null when 'all' is false.");
    }

    List<InsightsEventDTO>? eventsList;
    if (all)
    {
        var queryResult = await _context.Events
            .Include(e => e.Employee) 
            .ToListAsync();

        eventsList = queryResult
            .Select(e => new InsightsEventDTO
            {
                EmployeeId = e.EmployeeId,
                EmployeeFullName = e.Employee != null ? e.Employee.Name + " " + e.Employee.LastName : "Unknown Employee",
                EventTitle = e.Title,
                EventDescription = e.Description ?? string.Empty,
                EventDate = e.EventDate.ToString(),
                EventTime = e.EventTime.ToString()
            })
            .ToList();
    
        return eventsList;
    }
    
        IQueryable<Event> query = _context.Events.Include(e => e.Employee);
        
        if (!string.IsNullOrEmpty(filter.EventTitle))
        {
            query = query.Where(e => e.Title.Contains(filter.EventTitle));
        }

        if (!string.IsNullOrEmpty(filter.EventDescription))
        {
            query = query.Where(e => e.Description.Contains(filter.EventDescription));
        }

        if (filter.EmployeeId.HasValue)
        {
            query = query.Where(e => e.EmployeeId == filter.EmployeeId.Value);
        }

        if (!string.IsNullOrEmpty(filter.EventDate))
        {
            if (DateTime.TryParse(filter.EventDate, out var parsedDate))
            {
                query = query.Where(e => e.EventDate.Date == parsedDate.Date);
            }
        }
        
        if (!string.IsNullOrEmpty(filter.EventTime))
        {
            if (TimeSpan.TryParse(filter.EventTime, out var parsedTime))
            {
                query = query.Where(e => e.EventTime == parsedTime);
            }
        }

        if (!string.IsNullOrEmpty(filter.EmployeeFullName))
        {
            query = query.Where(e => (e.Employee.Name + " " + e.Employee.LastName).Contains(filter.EmployeeFullName));
        }
        
        eventsList =  query
            .Select(e => new InsightsEventDTO
            {
                EmployeeId = e.EmployeeId,
                EmployeeFullName = e.Employee != null ? e.Employee.Name + " " + e.Employee.LastName : "Unknown Employee",
                EventTitle = e.Title,
                EventDescription = e.Description ?? string.Empty,
                EventDate = e.EventDate.ToString(),
                EventTime = e.EventTime.ToString()
            })
            .ToList();

        return eventsList;
    }

}