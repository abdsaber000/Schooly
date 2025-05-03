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
    private readonly IClassRoomRepository _classRoomRepository;
    public CreateLessonCommandHandler(ILessonRepository lessonRepository, IStringLocalizer<CreateLessonCommandHandler> localizer, IHttpContextAccessor httpContextAccessor, IClassRoomRepository classRoomRepository)
    {
        _lessonRepository = lessonRepository;
        _localizer = localizer;
        _httpContextAccessor = httpContextAccessor;
        _classRoomRepository = classRoomRepository;
    }

    public async Task<Result<string>> Handle(CreateLessonCommand request, CancellationToken cancellationToken)
    {
        var classRoom = await _classRoomRepository.GetByIdAsync(request.ClassRoomId);
        if (classRoom is null) {
            return Result<string>.Failure(_localizer["Classroom not found"]);
        }
        var isClassRoomAvailable =
            await _lessonRepository.IsClassRoomAvailable(request.ClassRoomId, request.Date, request.From, request.To);
        if (!isClassRoomAvailable)
        {
            return Result<string>.Failure(_localizer["Classroom is not available in this time"]);
        }
        var userIdClaim = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier);
        
        var teacherId = userIdClaim.Value;
        var lesson = request.ToLesson();
        lesson.TeacherId = teacherId;
        
        await _lessonRepository.AddAsync(lesson);
        
        return Result<string>.SuccessMessage(_localizer["Lesson created successfully"]);
    }
}