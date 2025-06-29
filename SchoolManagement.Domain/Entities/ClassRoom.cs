using Microsoft.EntityFrameworkCore;

namespace SchoolManagement.Domain.Entities;

public class ClassRoom
{
    public Guid Id { get; set; }
    public string Grade { get; set; }
    public string Subject { get; set; }
    public string? TeacherId { get; set; }
    public Teacher? Teacher { get; set; } = null!; 
    public ICollection<StudentClassRoom> StudentClassRooms { get; set; }
    public ICollection<Post> Posts {get; set;} = new List<Post>();
}