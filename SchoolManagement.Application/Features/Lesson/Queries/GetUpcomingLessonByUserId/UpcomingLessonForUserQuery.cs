using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Http;
using SchoolManagement.Application.Features.Lesson.Dtos;
using SchoolManagement.Application.Features.Pagination;
using SchoolManagement.Domain.Entities;
using SchoolManagement.Domain.Interfaces.IRepositories;

namespace SchoolManagement.Application.Features.Lesson.Queries.GetUpcomingLessonByUserId;

public class UpcomingLessonForUserQuery : IRequest<PagedResult<LessonDto>>
{
    [Required]
    public int Page { get; set; } = 1;
    [Required]
    public int PageSize { get; set; } = 10;
}

public class UpcomingLessonForUserQueryHandler : IRequestHandler<UpcomingLessonForUserQuery , PagedResult<LessonDto>>
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IStudentClassRoomRepository _studentClassRoomRepository;
    private readonly IClassRoomRepository _classRoomRepository;
    private readonly ILessonRepository _lessonRepository;
    public UpcomingLessonForUserQueryHandler(IHttpContextAccessor httpContextAccessor, IStudentClassRoomRepository studentClassRoomRepository, IClassRoomRepository classRoomRepository, ILessonRepository lessonRepository)
    {
        _httpContextAccessor = httpContextAccessor;
        _studentClassRoomRepository = studentClassRoomRepository;
        _classRoomRepository = classRoomRepository;
        _lessonRepository = lessonRepository;
    }

    public async Task<PagedResult<LessonDto>> Handle(UpcomingLessonForUserQuery request, CancellationToken cancellationToken)
    {
        var user = _httpContextAccessor.HttpContext?.User;
        var userId = user.FindFirst(ClaimTypes.NameIdentifier).Value;
        var upcomingLessons = new List<Domain.Entities.Lesson>();
        var totalCount = 0;

        if (user.IsInRole(Roles.Student))
        {
            var studentClassrooms = await _studentClassRoomRepository
                .GetAllClassRoomsByStudentId(userId);
            
            var classroomIds = studentClassrooms.Select(c => c.Id).ToList();
            upcomingLessons = await _lessonRepository
                .GetUpcomingLessonsByClassRoomIds(classroomIds, request.Page, request.PageSize);
            totalCount = await _lessonRepository
                .GetTotalCountUpcomingLessonsByClassRoomIds(classroomIds);
        }
        else if (user.IsInRole(Roles.Teacher))
        {
            upcomingLessons = await _lessonRepository
                .GetUpcommingLessonsByTeacherId(request.Page, request.PageSize, userId);
            totalCount = await _lessonRepository.GetTotalCountAsyncByTeacherId(userId);
        }

        var upcomingLessonsDtos = upcomingLessons
            .Select(lesson => lesson.ToLessonDto())
            .ToList();
        return new PagedResult<LessonDto>
        {
            TotalItems = totalCount,
            Page = request.Page,
            PageSize = request.PageSize,
            Items = upcomingLessonsDtos
        };
    }
}