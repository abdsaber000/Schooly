namespace SchoolManagement.Application.Features.Lesson.Dtos;

public class ctiveLessonResponse
{
    public string LessonId { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public Guid ClassRoomId { get; set; }
    public List<string> ActiveUsers { get; set; } // List of user IDs in the lesson
}