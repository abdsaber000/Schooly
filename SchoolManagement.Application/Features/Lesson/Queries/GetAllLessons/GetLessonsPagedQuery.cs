using System.ComponentModel.DataAnnotations;
using MediatR;
using SchoolManagement.Application.Features.Lesson.Dtos;
using SchoolManagement.Application.Features.Pagination;
using SchoolManagement.Application.Features.Student.Dtos;
using SchoolManagement.Domain.Enums;

namespace SchoolManagement.Application.Features.Lesson.Queries.GetAllLessons;

public class GetLessonsPagedQuery : IRequest<PagedResult<LessonDto>>
{
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 10;
    public Guid classRoomId { get; set; }
    public LessonStatus? Status { get; set; }
}