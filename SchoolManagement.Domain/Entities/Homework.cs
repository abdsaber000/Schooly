using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolManagement.Domain.Entities;

public class HomeWork
{
    public Guid Id { get; set; }
    [ForeignKey("Lessons")]
    public Guid lessonId { get; set; }
    public Lesson Lesson { get; set; }
    public Guid classRoomId { get; set; }
    public string teacherId { get; set; }
    public string FileUrl { get; set; }
    public string fileName { get; set; }
    public DateTime FromDate { get; set; }
    public DateTime Deadline { get; set; }
}