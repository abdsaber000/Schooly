using MediatR;
using Microsoft.Extensions.Localization;
using SchoolManagement.Application.Features.Lesson.Dtos;
using SchoolManagement.Application.Shared;
using SchoolManagement.Domain.Interfaces.IRepositories;

namespace SchoolManagement.Application.Features.Lesson.Command.CreateLesson;

public class CreateLessonCommandHandler : IRequestHandler<CreateLessonCommand , Result<string>>
{
    private readonly ILessonRepository _lessonRepository;
    private readonly IStringLocalizer<CreateLessonCommandHandler> _localizer;
    public CreateLessonCommandHandler(ILessonRepository lessonRepository, IStringLocalizer<CreateLessonCommandHandler> localizer)
    {
        _lessonRepository = lessonRepository;
        _localizer = localizer;
    }

    public async Task<Result<string>> Handle(CreateLessonCommand request, CancellationToken cancellationToken)
    {
        var lesson = request.ToLesson();
        await _lessonRepository.CreateLesson(lesson);
        await _lessonRepository.SaveChanges();
        
        return Result<string>.SuccessMessage(_localizer["Lesson created successfully"]);
    }
}