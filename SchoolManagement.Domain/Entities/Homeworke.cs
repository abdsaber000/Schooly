namespace SchoolManagement.Domain.Entities;

public class HomeWork
{
    public Guid Id { get; set; }
    public Guid lessonId { get; set; }
    public Guid classRoomId { get; set; }
    public string teacherId { get; set; }
    public string fileName { get; set; }
}