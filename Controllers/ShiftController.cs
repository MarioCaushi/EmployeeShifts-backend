using EmployeeShift_backend.Services.ServicesInterfaces;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeShift_backend.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ShiftController : ControllerBase
{
    private readonly IShiftService _shiftService;
    
    public ShiftController(IShiftService shiftService)
    {
        _shiftService = shiftService;
    }

    [HttpGet("get-shift-options")]
    public async Task<IActionResult> GetShiftOptions()
    {
        try
        {
            var _shiftOptions = await _shiftService.GetShifts();

            if (_shiftOptions == null)
            {
                return NotFound("No shifts found");
            }

            return Ok(_shiftOptions);
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred while retrieving shift options: " + ex.Message);

            // Return a generic Internal Server Error status code
            return StatusCode(500, "An error occurred while processing your request. Please try again later.");
        }
    }

}