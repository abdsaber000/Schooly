using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Http;
using SchoolManagement.Application.Features.Lesson.Dtos;
using SchoolManagement.Application.Features.Pagination;
using SchoolManagement.Domain.Entities;
using SchoolManagement.Domain.Interfaces.IRepositories;

namespace SchoolManagement.Application.Features.Lesson.Queries.GetAllLessons;

public class GetLessonsPagedQueryHandler :IRequestHandler<GetLessonsPagedQuery , PagedResult<LessonDto>>
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IStudentClassRoomRepository _studentClassRoomRepository;
    private readonly ILessonRepository _lessonRepository;
    public GetLessonsPagedQueryHandler(ILessonRepository lessonRepository, IHttpContextAccessor httpContextAccessor, IStudentClassRoomRepository studentClassRoomRepository)
    {
        _lessonRepository = lessonRepository;
        _httpContextAccessor = httpContextAccessor;
        _studentClassRoomRepository = studentClassRoomRepository;
    }

    public async Task<PagedResult<LessonDto>> Handle(GetLessonsPagedQuery request, CancellationToken cancellationToken)
    {
        var user = _httpContextAccessor.HttpContext?.User;
        var userId = user.FindFirst(ClaimTypes.NameIdentifier).Value;
        var upcomingLessons = new List<Domain.Entities.Lesson>();
        var totalCount = 0;
        if (request.classRoomId != null && request.classRoomId != Guid.Empty)
        {
            totalCount = await _lessonRepository
                .GetTotalCountAsyncByClassRoomsId(request.classRoomId, cancellationToken);

            upcomingLessons = await _lessonRepository
                .GetPagedAsyncByClassRoomsId(request.Page, request.PageSize, request.classRoomId, cancellationToken);
        }
        else if (user.IsInRole(Roles.Student))
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