using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Localization;
using SchoolManagement.Application.Services.AuthenticationService;
using SchoolManagement.Application.Shared;
using SchoolManagement.Domain.Enums;
using SchoolManagement.Domain.Interfaces.IRepositories;

namespace SchoolManagement.Application.Features.Lesson.Command.CancelLesson;

public class CancelLessonCommandHandler: IRequestHandler<CancelLessonCommand , Result<string>>
{
    private readonly ILessonRepository _lessonRepository;
    private readonly StringLocalizer<CancelLessonCommandHandler> _localizer;
    private readonly IUserAuthenticationService _authenticationService;
    private IHttpContextAccessor _contextAccessor;
    public CancelLessonCommandHandler(ILessonRepository lessonRepository, StringLocalizer<CancelLessonCommandHandler> localizer, IUserAuthenticationService authenticationService, IHttpContextAccessor contextAccessor)
    {
        _lessonRepository = lessonRepository;
        _localizer = localizer;
        _authenticationService = authenticationService;
        _contextAccessor = contextAccessor;
    }

    public async Task<Result<string>> Handle(CancelLessonCommand request, CancellationToken cancellationToken)
    {
        var lesson = await _lessonRepository.GetByIdAsync(request.LessonId);
        if (lesson is null)
        {
            return Result<string>.Failure(_localizer["Lesson not found."]);
        }
        
        if (lesson.LessonStatus == LessonStatus.Canceled)
        {
            return Result<string>.Failure(_localizer["Lesson is already cancelled."]);
        }
        
        var user = await _authenticationService.GetCurrentUserAsync(_contextAccessor);
        if (lesson.TeacherId != user.Id)
        {
            return Result<string>.Failure(_localizer["You are not authorized to cancel this lesson."]);
        }

        await _lessonRepository.CancelLessonByIdAsync(request.LessonId);
        return Result<string>.Success(_localizer["Lesson cancelled successfully."]);
    }
}