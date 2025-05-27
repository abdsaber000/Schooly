using MediatR;
using Microsoft.Extensions.Localization;
using SchoolManagement.Application.Shared;
using SchoolManagement.Domain.Interfaces.IRepositories;

namespace SchoolManagement.Application.Features.Lesson.Command.DeleteLesson;

public class DeleteLessonCommand : IRequest<Result<string>>
{
    public Guid Id { get; set; }

    public DeleteLessonCommand(Guid id)
    {
        Id = id;
    }
}

public class DeleteLessonCommandHandeler : IRequestHandler<DeleteLessonCommand , Result<string>>
{
    private readonly ILessonRepository _lessonRepository;
    private readonly IStringLocalizer<DeleteLessonCommandHandeler> _localizer;
    public DeleteLessonCommandHandeler(ILessonRepository lessonRepository, IStringLocalizer<DeleteLessonCommandHandeler> localizer)
    {
        _lessonRepository = lessonRepository;
        _localizer = localizer;
    }

    public async Task<Result<string>> Handle(DeleteLessonCommand request, CancellationToken cancellationToken)
    {
        var lesson = await _lessonRepository.GetByIdAsync(request.Id);
        if (lesson is null)
        {
            return Result<string>.Failure(_localizer["Lesson not found."]);
        }
        await _lessonRepository.Delete(lesson);
        
        return Result<string>.SuccessMessage(_localizer["Lesson deleted successfully"]);
    }
}