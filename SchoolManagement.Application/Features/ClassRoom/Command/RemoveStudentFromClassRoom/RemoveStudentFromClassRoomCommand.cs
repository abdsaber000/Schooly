using System.ComponentModel.DataAnnotations;
using MediatR;
using Microsoft.Extensions.Localization;
using SchoolManagement.Application.Features.ClassRoom.Command.AssignStudentToClassRoom;
using SchoolManagement.Application.Shared;
using SchoolManagement.Domain.Entities;
using SchoolManagement.Domain.Interfaces.IRepositories;

namespace SchoolManagement.Application.Features.ClassRoom.Command.RemoveStudentFromClassRoom;

public class RemoveStudentFromClassRoomCommand: IRequest<Result<string>>
{
    [Required]
    public string StudentId { get; set; } = string.Empty;
    [Required]
    public Guid ClassRoomId { get; set; }
}

public class RemoveStudentFromClassRoomCommandHandler : IRequestHandler<RemoveStudentFromClassRoomCommand , Result<string>>
{
    private readonly IStudentClassRoomRepository _studentClassRoomRepository;
    private readonly IStringLocalizer<AssignStudentToClassRoomCommandHandler> _localizer;

    public RemoveStudentFromClassRoomCommandHandler(IStudentClassRoomRepository studentClassRoomRepository, IStringLocalizer<AssignStudentToClassRoomCommandHandler> localizer)
    {
        _studentClassRoomRepository = studentClassRoomRepository;
        _localizer = localizer;
    }

    public async Task<Result<string>> Handle(RemoveStudentFromClassRoomCommand request, CancellationToken cancellationToken)
    {
        var existingAssignment = await _studentClassRoomRepository
            .GetStudentClassRoomAsync(request.StudentId, request.ClassRoomId);
      
        if (existingAssignment is null) {
            return Result<string>.Failure(_localizer["Student is already not assigned"]);
        }
        await _studentClassRoomRepository.Delete(existingAssignment);
        return Result<string>.SuccessMessage(_localizer["Student delete from classroom successfully"]);
    }
}