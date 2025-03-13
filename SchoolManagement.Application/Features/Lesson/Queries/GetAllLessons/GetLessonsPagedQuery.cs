using System.ComponentModel.DataAnnotations;
using MediatR;
using SchoolManagement.Application.Features.Lesson.Dtos;
using SchoolManagement.Application.Features.Pagination;
using SchoolManagement.Application.Features.Student.Dtos;

namespace SchoolManagement.Application.Features.Lesson.Queries.GetAllLessons;

public class GetLessonsPagedQuery : IRequest<PagedResult<LessonDto>>
{
    [Required]
    public int Page { get; set; } = 1;
    [Required]
    public int PageSize { get; set; } = 10;
    [Required]
    public Guid classRoomId { get; set; }
}