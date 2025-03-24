using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SchoolManagement.Domain.Enums;

namespace SchoolManagement.Domain.Entities;

public class Lesson
{
    [Key]
    public Guid Id { get; set; }
    public string TeacherId { get; set; } = string.Empty;
    
    [ForeignKey("ClassRooms")] 
    public Guid ClassRoomId { get; set; }
    public ClassRoom ClassRoom { get; set; }
    public string Title { get; set; } = string.Empty;
    public LessonType LessonType { get; set; }
    public DateOnly Date { get; set; }
    public TimeOnly From { get; set; }
    public TimeOnly To { get; set; }
}