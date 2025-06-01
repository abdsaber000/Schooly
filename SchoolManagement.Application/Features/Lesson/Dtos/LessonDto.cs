using SchoolManagement.Domain.Enums;

namespace SchoolManagement.Application.Features.Lesson.Dtos;

public class LessonDto
{
    public Guid Id { get; set; } 
    public string TeacherId { get; set; } = string.Empty;
    public string TeacherName { get; set; }
    public Guid classRoomId { get; set; } 
    public string Title { get; set; } = string.Empty;
    public string Grade { get; set; } = string.Empty;
    public string Subject { get; set; } = string.Empty;
    public LessonType LessonType { get; set; }
    public DateOnly Date { get; set; }
    public TimeOnly From { get; set; }
    public TimeOnly To { get; set; }
    public LessonStatus Status { get; set; }
}