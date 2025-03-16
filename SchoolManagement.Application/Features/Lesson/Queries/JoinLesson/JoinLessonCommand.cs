using MediatR;
using Microsoft.Extensions.Localization;
using SchoolManagement.Application.Services.AgoraService;
using SchoolManagement.Application.Services.EgyptTimeService;
using SchoolManagement.Application.Shared;
using SchoolManagement.Domain.Interfaces.IRepositories;

namespace SchoolManagement.Application.Features.Lesson.Queries.JoinLesson;

public class JoinLessonCommand : IRequest<Result<JoinLessonDto>>
{
    public Guid Id { get; }

    public JoinLessonCommand(Guid id)
    {
        Id = id;
    }
}

public class JoinLessonCommandHandler : IRequestHandler<JoinLessonCommand, Result<JoinLessonDto>>
{
    private readonly ILessonRepository _lessonRepository;
    private readonly IAgoraService _agoraService;
    private readonly IStringLocalizer<JoinLessonCommandHandler> _localizer;
    private readonly IEgyptTime _egyptTime;

    public JoinLessonCommandHandler(ILessonRepository lessonRepository, IAgoraService agoraService,
        IStringLocalizer<JoinLessonCommandHandler> localizer, IEgyptTime egyptTime)
    {
        _lessonRepository = lessonRepository;
        _agoraService = agoraService;
        _localizer = localizer;
        _egyptTime = egyptTime;
    }

    public async Task<Result<JoinLessonDto>> Handle(JoinLessonCommand request, CancellationToken cancellationToken)
    {
        var lesson = await _lessonRepository.GetByIdAsync(request.Id);

        if (lesson is null)
        {
            return Result<JoinLessonDto>.Failure(_localizer["Lesson not found."]);
        }
        var nowUtc = DateTime.UtcNow;
        
        var lessonStartTimeEgypt = lesson.Date.ToDateTime(lesson.From);
        var lessonEndTimeEgypt = lesson.Date.ToDateTime(lesson.To);
        
        var egyptTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Africa/Cairo");
        
        var nowEgypt = TimeZoneInfo.ConvertTimeFromUtc(nowUtc, egyptTimeZone);
        
        if (nowEgypt < lessonStartTimeEgypt.AddMinutes(-5))
        {
            return Result<JoinLessonDto>.Failure(_localizer["You can only join 5 minutes before the lesson starts."]);
        }
        if (nowEgypt > lessonEndTimeEgypt)
        {
            return Result<JoinLessonDto>.Failure(_localizer["You cannot join , the lesson has ended."]);
        }

        var token =  _agoraService.GenerateToken(lesson.Title, "0", 7200); 
        return Result<JoinLessonDto>.Success(new JoinLessonDto { Token = token });

    }
}