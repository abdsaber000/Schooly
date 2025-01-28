using MediatR;
using SchoolManagement.Application.Features.Pagination;
using SchoolManagement.Domain.Interfaces.IRepositories;

namespace SchoolManagement.Application.Features.Lesson.Queries.GetAllLessons;

public class GetLessonsPagedQueryHandler :IRequestHandler<GetLessonsPagedQuery , PagedResult<Domain.Entities.Lesson>>
{
    private readonly IGenericRepository<Domain.Entities.Lesson> _lessonRepository;

    public GetLessonsPagedQueryHandler(IGenericRepository<Domain.Entities.Lesson> lessonRepository)
    {
        _lessonRepository = lessonRepository;
    }
    
    public async Task<PagedResult<Domain.Entities.Lesson>> Handle(GetLessonsPagedQuery request, CancellationToken cancellationToken)
    {
        var totalCount = await _lessonRepository.GetTotalCountAsync(cancellationToken);
        
        var lessons = await _lessonRepository
            .GetPagedAsync(request.Page, request.PageSize, cancellationToken);
        
        return new PagedResult<Domain.Entities.Lesson>
        {
            TotalItems = totalCount,
            Page = request.Page,
            PageSize = request.PageSize,
            Items = lessons
        };
    }
}