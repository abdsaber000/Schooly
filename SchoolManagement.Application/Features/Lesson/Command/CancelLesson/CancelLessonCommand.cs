using MediatR;
using SchoolManagement.Application.Shared;

namespace SchoolManagement.Application.Features.Lesson.Command.CancelLesson;

public class CancelLessonCommand : IRequest<Result<string>>
{
    public Guid LessonId { get; set; }
    public CancelLessonCommand(Guid lessonId)
    {
        LessonId = lessonId;
    }
}