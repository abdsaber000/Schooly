using Microsoft.AspNetCore.Identity;
using SchoolManagement.Domain.Enums;

namespace SchoolManagement.Domain.Entities;

// TPT (Table Per Type)
public class ApplicationUser : IdentityUser  
{
    public string? ProfilePhoto { get; set; } 
    public Gender Gender { get; set; }
    public DateTime DateOfBirth { get; set; }
}