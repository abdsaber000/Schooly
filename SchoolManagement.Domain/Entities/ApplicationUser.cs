using Microsoft.AspNetCore.Identity;
using SchoolManagement.Domain.Enums;
using SchoolManagement.Domain.Enums.User;

namespace SchoolManagement.Domain.Entities;

// TPT (Table Per Type)
public class ApplicationUser : IdentityUser
{
    public string Name { get; set; }
    public Gender? Gender { get; set; }
    public DateOnly? DateOfBarith { get; set; }
    public ICollection<Post> Posts { get; set; } = new List<Post>();
    public ICollection<Comment> Comments { get; set; } = new List<Comment>();
    public virtual Role Role {get; set;} 
}