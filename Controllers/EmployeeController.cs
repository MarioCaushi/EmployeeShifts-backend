using EmployeeShift_backend.DTOs;
using EmployeeShift_backend.Services.ServicesInterfaces;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeShift_backend.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EmployeeController : ControllerBase
{
    private readonly IEmployeeService _employeeService;
    private readonly IEventService _eventService;
    
    public EmployeeController(IEmployeeService employeeService, IEventService eventService)
    {
        _employeeService = employeeService;
        _eventService = eventService;
    }
    
    [HttpGet("get-employee-by-id/{employeeId:int}")]
    public async Task<IActionResult> GetEmployeeById(int employeeId)
    {
        if (employeeId == null)
            return BadRequest("Employee Id is required");
        
        var employee = await _employeeService.GetEmployeeById(employeeId);
        if (employee == null)
            return NotFound("Employee not found");
        return Ok(employee);
    }

    [HttpPut("edit-employee-info")]
    public async Task<IActionResult> EditEmployeeInfo([FromBody] EditEmployeeDTO? employeeDTO)
    {
        if(employeeDTO == null)
            return BadRequest("Employee info is required");
        
        var IsEdited = await _employeeService.EditEmployee(employeeDTO);
        if(IsEdited)
            return Ok("Employee info updated successfully");
        return BadRequest("Employee info not updated");
    }
    
    [HttpPost("add-employee")]
    public async Task<IActionResult> AddEmployee([FromBody] AddEmployeeDTO? employeeDTO)
    {
        if(employeeDTO == null)
            return BadRequest("Employee info is required");
        
        var IsAdded = await _employeeService.AddEmployee(employeeDTO);
        if(IsAdded)
            return Ok("Employee added successfully");
        return BadRequest("Employee not added");
    }

    [HttpDelete("delete-employee/{employeeId:int}")]
    public async Task<IActionResult> DeleteEmployee(int employeeId)
    {
        var IsDeleted = await _employeeService.DeleteEmployee(employeeId);
        
        if (IsDeleted)
        {
            var eventsDeleted = await _eventService.DeleteEventsByEmployeeId(employeeId);

            if (eventsDeleted)
            {
                return Ok("Employee deleted successfully and their events");
            }
            return Ok("Employee deleted successfully but not their events");
        }
        return BadRequest("Employee not deleted");
    }

}