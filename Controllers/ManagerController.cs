using EmployeeShift_backend.DTOs;
using Microsoft.AspNetCore.Mvc;
using EmployeeShift_backend.Services.ServicesInterfaces;

namespace EmployeeShift_backend.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ManagerController : ControllerBase
{
    private readonly IManagerService _managerService;
    
    public ManagerController(IManagerService managerService)
    {
        _managerService = managerService;
    }

    [HttpGet("get-all-managers")]
    public async Task<IActionResult> GetAllManagers()
    {
        var allManagers = await _managerService.getAllManagersAsync();
        if (allManagers == null)
        {
            return NotFound("No managers found");
        }
        return Ok(allManagers);
    }
    
    [HttpGet("get-manager-by-id/{id:int}")]
    public async Task<IActionResult> GetManagerById(int id)
    {
        var manager = await _managerService.getManagerByIdAsync(id);
        if (manager == null)
        {
            return NotFound("Manager not found");
        }
        return Ok(manager);
    }

    [HttpGet("manager-login-authentication/{username}/{password}")]
    public async Task<IActionResult> ManagerLoginAuthentication(string username, string password)
    {
        var managerid = await _managerService.ManagerLoginAuthentication(username, password);
        if (managerid == 0)
        {
            return NotFound("Manager not found");
        }
        return Ok(managerid);
    }

    [HttpGet("get-employees-by-manager-id/{managerId:int}")]
    public async Task<IActionResult> GetEmployeesByManagerId(int managerId)
    {
        var employees = await _managerService.GetEmployeesByManagerId(managerId);
        if (employees.Count == 0)
        {
            return NotFound("No employees found for this manager");
        } 
        return Ok(employees);
    }
    
    [HttpPut("edit-manager-info/{managerId:int}")]
    public async Task<IActionResult> EditManagerInfo(int managerId, [FromBody] ManagerDTO managerDTO)
    {
        var result = await _managerService.EditManagerInfo(managerId, managerDTO);
        if (result)
        {
            return Ok("Manager info updated successfully");
        }
        return BadRequest("Manager info not updated");
    }
}