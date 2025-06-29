using MediatR;
using Microsoft.Extensions.Localization;
using SchoolManagement.Application.Features.Lesson.Dtos;
using SchoolManagement.Application.Shared;
using SchoolManagement.Domain.Interfaces.IRepositories;

namespace SchoolManagement.Application.Features.Lesson.Command.UpdateLeeson;

public class UpdateLessonCommandHandler:IRequestHandler<UpdateLessonCommand , Result<string>>
{
    private readonly ILessonRepository _lessonRepository;
    private readonly IStringLocalizer<UpdateLessonCommandHandler> _localizer;
    
    public UpdateLessonCommandHandler(ILessonRepository lessonRepository, IStringLocalizer<UpdateLessonCommandHandler> localizer)
    {
        _lessonRepository = lessonRepository;
        _localizer = localizer;
    }

    public async Task<Result<string>> Handle(UpdateLessonCommand request, CancellationToken cancellationToken)
    {
        var lesson = await _lessonRepository.GetByIdAsync(request.Id);
        if (lesson is null)
        {
            return Result<string>.Failure(_localizer["Lesson not found."]);
        }
        var updatedLesson = request.ToUpdatedLesson();
        updatedLesson.TeacherId = lesson.TeacherId;
        await _lessonRepository.Update(updatedLesson);
        
        return Result<string>.SuccessMessage(_localizer["Lesson updated successfully"]);
    }
}