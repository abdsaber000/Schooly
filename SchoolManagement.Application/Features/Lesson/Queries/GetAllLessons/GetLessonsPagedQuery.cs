using MediatR;
using SchoolManagement.Application.Features.Lesson.Dtos;
using SchoolManagement.Application.Features.Pagination;
using SchoolManagement.Application.Features.Student.Dtos;

namespace SchoolManagement.Application.Features.Lesson.Queries.GetAllLessons;

public class GetLessonsPagedQuery : IRequest<PagedResult<LessonDto>>
{
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}