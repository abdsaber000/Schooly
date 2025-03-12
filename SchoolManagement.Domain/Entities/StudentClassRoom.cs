namespace SchoolManagement.Domain.Entities;

public class StudentClassRoom  // many to many realtion
{
    public string StudentId { get; set; }
    public Guid ClassRoomId { get; set; } 

    // Navigation properties
    public Student Student { get; set; }
    public ClassRoom ClassRoom { get; set; }
}