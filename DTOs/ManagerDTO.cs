using System;
using System.ComponentModel.DataAnnotations;

namespace EmployeeShift_backend.DTOs;

public class ManagerDTO
{
    [Required, StringLength(50)]
    public string Name { get; set; } = null!;

    [Required, StringLength(50)]
    public string LastName { get; set; } = null!;

    [Required]
    public DateOnly Birthday { get; set; }

    [Required, EmailAddress]
    public string Email { get; set; } = null!;

    [Required, Phone]
    public string PhoneNumber { get; set; } = null!;

    [Required, StringLength(30)]
    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;

    [StringLength(100)]
    public string Address { get; set; } = null!;
}

