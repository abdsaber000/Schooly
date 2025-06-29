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
        await _lessonRepository.MarkExpiredLessonsAsCompleted();
        var user = _httpContextAccessor.HttpContext?.User;
        var userId = user.FindFirst(ClaimTypes.NameIdentifier).Value;
        var upcomingLessons = new List<Domain.Entities.Lesson>();
        var totalCount = 0;
        
        if (request.classRoomId != null && request.classRoomId != Guid.Empty)
        {
            totalCount = await _lessonRepository
                .GetTotalCountAsyncByClassRoomId(request.classRoomId, request.Status , cancellationToken);

            upcomingLessons = await _lessonRepository
                .GetPagedAsyncByClassRoomId(request.Page, request.PageSize, request.classRoomId, request.Status, cancellationToken);
        }
        
        else if (user.IsInRole(Roles.Student))
        {
            var studentClassrooms = await _studentClassRoomRepository
                .GetAllClassRoomsByStudentId(userId);
            
            var classroomsIds = studentClassrooms.Select(c => c.Id).ToList();
            
            upcomingLessons = await _lessonRepository
                .GetLessonsByClassRoomsIds(classroomsIds, request.Page, request.PageSize , request.Status);
            
            totalCount = await _lessonRepository
                .GetTotalCountLessonsByClassRoomsIds(classroomsIds , request.Status);
        }
        
        else if (user.IsInRole(Roles.Teacher))
        {
            upcomingLessons = await _lessonRepository
                .GetLessonsByTeacherId(request.Page, request.PageSize, userId , request.Status);
            
            totalCount = await _lessonRepository.GetTotalCountAsyncByTeacherId(userId , request.Status , cancellationToken);
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