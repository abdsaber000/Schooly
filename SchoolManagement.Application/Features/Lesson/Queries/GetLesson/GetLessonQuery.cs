using MediatR;
using SchoolManagement.Application.Features.Lesson.Dtos;
using SchoolManagement.Application.Shared;
using SchoolManagement.Domain.Interfaces.IRepositories;
using SchoolManagement.Domain.Entities;
namespace SchoolManagement.Application.Features.Lesson.Queries.GetLesson;

public class GetLessonQuery : IRequest<Result<LessonDto>>
{
    public string Id { get;}
    public GetLessonQuery(string id)
    {
        Id = id;
    }
}

public class GetLessonQueryHandler : IRequestHandler<GetLessonQuery , Result<LessonDto>>
{
    private readonly ILessonRepository _lessonRepository;

    public GetLessonQueryHandler(ILessonRepository lessonRepository)
    {
        _lessonRepository = lessonRepository;
    }

    public async Task<Result<LessonDto>> Handle(GetLessonQuery request, CancellationToken cancellationToken)
    {
        var lesson = await _lessonRepository.GetLessonById(request.Id);
    
        if (lesson == null)
        {
            return Result<LessonDto>.Failure("Lesson not found.");
        }

        var lessonDto = lesson.ToLessonDto();
        return Result<LessonDto>.Success(lessonDto);
    }
}