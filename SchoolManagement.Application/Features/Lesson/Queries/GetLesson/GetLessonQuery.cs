using MediatR;
using SchoolManagement.Application.Shared;
using SchoolManagement.Domain.Interfaces.IRepositories;
using SchoolManagement.Domain.Entities;
namespace SchoolManagement.Application.Features.Lesson.Queries.GetLesson;

public class GetLessonQuery : IRequest<Result<Domain.Entities.Lesson>>
{
    public string Id { get;}
    public GetLessonQuery(string id)
    {
        Id = id;
    }
}

public class GetLessonQueryHandler : IRequestHandler<GetLessonQuery , Result<Domain.Entities.Lesson>>
{
    private readonly ILessonRepository _lessonRepository;

    public GetLessonQueryHandler(ILessonRepository lessonRepository)
    {
        _lessonRepository = lessonRepository;
    }

    public async Task<Result<Domain.Entities.Lesson>> Handle(GetLessonQuery request, CancellationToken cancellationToken)
    {
        var lesson = await _lessonRepository.GetLessonById(request.Id);

        if (lesson == null)
        {
            return Result<Domain.Entities.Lesson>.Failure("Lesson not found.");
        }
        
        return Result<Domain.Entities.Lesson>.Success(lesson);
    }
}