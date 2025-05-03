using System.ComponentModel.DataAnnotations;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Extensions.Localization;
using Org.BouncyCastle.Asn1.Ocsp;
using SchoolManagement.Application.Shared;
using SchoolManagement.Domain.Entities;
using SchoolManagement.Domain.Interfaces.IRepositories;

namespace SchoolManagement.Application.Features.ClassRoom.Command.AssignStudentToClassRoom;

public class AssignStudentToClassRoomCommand : IRequest<Result<string>>
{
    [Required]
    public string StudentId { get; set; } = string.Empty;
    [Required]
    public Guid ClassRoomId { get; set; }
}

public class AssignStudentToClassRoomCommandHandler : IRequestHandler<AssignStudentToClassRoomCommand , Result<string>>
{
    private readonly IStudentClassRoomRepository _studentClassRoomRepository;
    private readonly IStringLocalizer<AssignStudentToClassRoomCommandHandler> _localizer;
    private readonly IStudentRepository _studentRepository;
    private readonly IClassRoomRepository _classRoomRepository;
    public AssignStudentToClassRoomCommandHandler(IStudentClassRoomRepository studentClassRoomRepository, IStringLocalizer<AssignStudentToClassRoomCommandHandler> localizer, IStudentRepository studentRepository, IClassRoomRepository classRoomRepository)
    {
        _studentClassRoomRepository = studentClassRoomRepository;
        _localizer = localizer;
        _studentRepository = studentRepository;
        _classRoomRepository = classRoomRepository;
    }

    public async Task<Result<string>> Handle(AssignStudentToClassRoomCommand request, CancellationToken cancellationToken)
    {
        var existingAssignment = await _studentClassRoomRepository
            .GetStudentClassRoomAsync(request.StudentId, request.ClassRoomId);
        var student = await _studentRepository.GetByIdAsync(request.StudentId);
        var classRoom = await _classRoomRepository.GetByIdAsync(request.ClassRoomId);
        if (student is null) {
            return Result<string>.Failure(_localizer["Student not found"]);
        }
        if (classRoom is null) {
            return Result<string>.Failure(_localizer["Classroom not found"]);
        }
        if (existingAssignment is not null) {
            return Result<string>.Failure(_localizer["Student is already assigned"]);
        }
        
        var studentClassRoom = new StudentClassRoom
        {
            StudentId = request.StudentId,
            ClassRoomId = request.ClassRoomId
        };
        await _studentClassRoomRepository.AddAsync(studentClassRoom);
        return Result<string>.SuccessMessage(_localizer["Student assigned to classroom successfully"]);
    }
}