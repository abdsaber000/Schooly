using MediatR;
using Microsoft.Extensions.Localization;
using SchoolManagement.Application.Shared;
using SchoolManagement.Domain.Interfaces.IRepositories;

namespace SchoolManagement.Application.Features.ClassRoom.Command.AssignTeacerToClassroom;

public class AssignTeacherToClassroomCommandHandler : IRequestHandler<AssignTeacherToClassroomCommand, Result<string>>
{
    private readonly IClassRoomRepository _classRoomRepository;
    private readonly IStringLocalizer<AssignTeacherToClassroomCommandHandler> _localizer;
    private readonly ITeacherRepository _teacherRepository;
    public AssignTeacherToClassroomCommandHandler(IClassRoomRepository classRoomRepository, IStringLocalizer<AssignTeacherToClassroomCommandHandler> localizer, ITeacherRepository teacherRepository)
    {
        _classRoomRepository = classRoomRepository;
        _localizer = localizer;
        _teacherRepository = teacherRepository;
    }

    public async Task<Result<string>> Handle(AssignTeacherToClassroomCommand request, CancellationToken cancellationToken)
    {
        var classroom = await _classRoomRepository.GetByIdAsync(request.ClassRoomId);
        if (classroom is null)
        {
            return Result<string>.Failure(_localizer["Classroom not found"]);
        }
        var teacher = await _teacherRepository.GetByIdAsync(request.TeacherId);
        if (teacher is null)
        {
            return Result<string>.Failure(_localizer["Teacher not found"]);
        }
        
        classroom.TeacherId = request.TeacherId;
        await _classRoomRepository.Update(classroom);
        return Result<string>.SuccessMessage(_localizer["Teacher assigned to classroom successfully"]);
    }
}