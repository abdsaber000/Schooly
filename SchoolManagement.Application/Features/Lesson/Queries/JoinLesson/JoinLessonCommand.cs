using MediatR;
using Microsoft.Extensions.Localization;
using SchoolManagement.Application.Services.AgoraService;
using SchoolManagement.Application.Shared;
using SchoolManagement.Domain.Interfaces.IRepositories;

namespace SchoolManagement.Application.Features.Lesson.Queries.JoinLesson;

public class JoinLessonCommand : IRequest<Result<JoinLessonDto>>
{
    public string Id { get; }

    public JoinLessonCommand(string id)
    {
        Id = id;
    }
}

public class JoinLessonCommandHandler : IRequestHandler<JoinLessonCommand, Result<JoinLessonDto>>
{
    private readonly ILessonRepository _lessonRepository;
    private readonly IAgoraService _agoraService;
    private readonly IStringLocalizer<JoinLessonCommandHandler> _localizer;
    
    public JoinLessonCommandHandler(ILessonRepository lessonRepository, IAgoraService agoraService, IStringLocalizer<JoinLessonCommandHandler> localizer)
    {
        _lessonRepository = lessonRepository;
        _agoraService = agoraService;
        _localizer = localizer;
    }

    public async Task<Result<JoinLessonDto>> Handle(JoinLessonCommand request, CancellationToken cancellationToken)
    {
        var lesson = await _lessonRepository.GetLessonById(request.Id);
        
        if (lesson is null)
        {
            return Result<JoinLessonDto>.Failure(_localizer["Lesson not found."]);
        }

        var now = DateTime.Now;
        
        var lessonStartTime = lesson.Date.ToDateTime(lesson.From);
        var lessonEndTime = lesson.Date.ToDateTime(lesson.To);

        if (now < lessonStartTime.AddMinutes(-5))
        {
            return Result<JoinLessonDto>.Failure(_localizer["You can only join 5 minutes before the lesson starts."]);
        }
        if (now > lessonEndTime)
        {
            return Result<JoinLessonDto>.Failure(_localizer["You cannot join , the lesson has ended."]);
        }

        var token =  _agoraService.GenerateToken(lesson.Title, "0", 7200); 
        return Result<JoinLessonDto>.Success(new JoinLessonDto { Token = token });
    }
}