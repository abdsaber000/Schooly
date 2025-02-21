using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Localization;
using SchoolManagement.Application.Features.Lesson.Dtos;
using SchoolManagement.Application.Shared;
using SchoolManagement.Domain.Interfaces.IRepositories;

namespace SchoolManagement.Application.Features.Lesson.Command.CreateLesson;

public class CreateLessonCommandHandler : IRequestHandler<CreateLessonCommand , Result<string>>
{
    private readonly ILessonRepository _lessonRepository;
    private readonly IStringLocalizer<CreateLessonCommandHandler> _localizer;
    private readonly IHttpContextAccessor _httpContextAccessor;
    public CreateLessonCommandHandler(ILessonRepository lessonRepository, IStringLocalizer<CreateLessonCommandHandler> localizer, IHttpContextAccessor httpContextAccessor)
    {
        _lessonRepository = lessonRepository;
        _localizer = localizer;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<Result<string>> Handle(CreateLessonCommand request, CancellationToken cancellationToken)
    {
        var userIdClaim = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier);

        if (userIdClaim is null)
        {
            return Result<string>.Failure("User is not authenticated.");
        }
        var teacherId = userIdClaim.Value;
        var lesson = request.ToLesson();
        lesson.TeacherId = teacherId;
        
        await _lessonRepository.CreateLesson(lesson);
        await _lessonRepository.SaveChanges();
        
        return Result<string>.SuccessMessage(_localizer["Lesson created successfully"]);
    }
}