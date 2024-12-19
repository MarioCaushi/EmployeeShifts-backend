using EmployeeShift_backend.DTOs;
using EmployeeShift_backend.Services.ServicesInterfaces;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeShift_backend.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EventController : ControllerBase
{
    private readonly IEventService _eventService;
    
    public EventController(IEventService eventService)
    {
        _eventService = eventService;
    }

    [HttpGet("get-events-of-employee/{employeeId:int}")]
    public async Task<IActionResult> GetEventsOfEmployee(int employeeId)
    {
        var EmployeeEvents = await _eventService.GetEventsByEmployeeId(employeeId);
        if (EmployeeEvents.Count == 0)
        {
            return NotFound("No events found for this employee");
        }
        return Ok(EmployeeEvents);
    }

    [HttpPost("get-filtered-events/{all:bool}")]
    public async Task<IActionResult> GetFilteredEvents(bool all, [FromBody] InsightsEventDTO filter)
    {
        if (filter == null)
        {
            return BadRequest("Filter data is required.");
        }

        var filteredEvents = await _eventService.GetEventsByFilter(all, filter);
        if (filteredEvents == null || filteredEvents.Count == 0)
        {
            return Ok(new List<InsightsEventDTO>()); // Return an empty list to indicate successful query but no results
        }

        return Ok(filteredEvents);
    }
    
    [HttpPost("add-event")]
    public async Task<IActionResult> AddEvent([FromBody] AddEventDTO eventToAdd)
    {
        var result = await _eventService.AddEvent(eventToAdd);
        if (result)
        {
            return Ok("Event added successfully");
        }
        return BadRequest("Event not added");
    }
    
    
    [HttpDelete("delete-event-by-employeeId/{emplpyeeId:int}")]
    public async Task<IActionResult> DeleteEventsByEmployeeId(int emplpyeeId)
    {
        var result = await _eventService.DeleteEventsByEmployeeId(emplpyeeId);
        if (result)
        {
            return Ok("Event deleted successfully");
        }
        return BadRequest("Event not deleted");
    }
    
}