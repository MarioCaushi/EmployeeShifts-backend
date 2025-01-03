using System;
using System.ComponentModel.DataAnnotations;

namespace EmployeeShift_backend.DTOs;

public class ManagerDTO
{
    public string Name { get; set; } 

    public string LastName { get; set; } 

    public DateOnly Birthday { get; set; }
    
    public string Email { get; set; } 
    
    public string PhoneNumber { get; set; } 
    
    public string Username { get; set; } 

    public string Password { get; set; } 
    
    public string Address { get; set; } 
}

