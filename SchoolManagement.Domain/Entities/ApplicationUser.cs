using Microsoft.AspNetCore.Identity;
using SchoolManagement.Domain.Enums;

namespace SchoolManagement.Domain.Entities;

// TPT (Table Per Type)
public class ApplicationUser : IdentityUser  
{
    public string  Name { get; set; }
    public Gender? Gender { get; set; }
    public DateOnly? DateOfBarith { get; set; }
}