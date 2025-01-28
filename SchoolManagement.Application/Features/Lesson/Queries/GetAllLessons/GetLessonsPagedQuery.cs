namespace SchoolManagement.Application.Features.Lesson.Queries.GetAllLessons;

public class GetAllLessonsQuery : Pa
{
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}