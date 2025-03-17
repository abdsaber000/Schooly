using MediatR;
using SchoolManagement.Application.Features.Lesson.Dtos;
using SchoolManagement.Application.Features.Pagination;
using SchoolManagement.Domain.Interfaces.IRepositories;

namespace SchoolManagement.Application.Features.Lesson.Queries.GetAllLessons;

public class GetLessonsPagedQueryHandler :IRequestHandler<GetLessonsPagedQuery , PagedResult<LessonDto>>
{
    private readonly ILessonRepository _lessonRepository;

    public GetLessonsPagedQueryHandler(ILessonRepository lessonRepository)
    {
        _lessonRepository = lessonRepository;
    }

    public async Task<PagedResult<LessonDto>> Handle(GetLessonsPagedQuery request, CancellationToken cancellationToken)
    {
        var totalCount = await _lessonRepository.GetTotalCountAsyncByClassRoomsId(request.classRoomId ,cancellationToken);
        
        var lessons = await _lessonRepository
            .GetPagedAsyncByClassRoomsId(request.Page, request.PageSize , request.classRoomId, cancellationToken);

        var lessonsDtos = new List<LessonDto>();
        foreach (var lesson in lessons)
        {
            var lessonDto = lesson.ToLessonDto();
            lessonsDtos.Add(lessonDto);
        }
        return new PagedResult<LessonDto>
        {
            TotalItems = totalCount,
            Page = request.Page,
            PageSize = request.PageSize,
            Items = lessonsDtos
        };
    }
}