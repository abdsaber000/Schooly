using Microsoft.EntityFrameworkCore;

namespace SchoolManagement.Domain.Entities;

public class ClassRoom
{
    public Guid ClassRoomId { get; set; }
    public string Grade { get; set; }
    public string Subject { get; set; }
    public ICollection<StudentClassRoom> StudentClassRooms { get; set; }
}