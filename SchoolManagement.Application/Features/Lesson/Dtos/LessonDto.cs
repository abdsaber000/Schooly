using SchoolManagement.Domain.Enums;

namespace SchoolManagement.Application.Features.Lesson.Dtos;

public class LessonDto
{
    public string Id { get; set; } = string.Empty;
    public string TeacherId { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Grade { get; set; } = string.Empty;
    public string Subject { get; set; } = string.Empty;
    public LessonType LessonType { get; set; }
    public DateOnly Date { get; set; }
    public TimeOnly From { get; set; }
    public TimeOnly To { get; set; }
}