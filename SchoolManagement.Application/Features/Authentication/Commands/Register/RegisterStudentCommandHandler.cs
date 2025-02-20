using MediatR;
using Microsoft.Extensions.Localization;
using SchoolManagement.Application.Features.Authentication.Dtos;
using SchoolManagement.Application.Shared;
using SchoolManagement.Domain.Interfaces.IRepositories;

namespace SchoolManagement.Application.Features.Authentication.Commands.Register;

public class RegisterStudentCommandHandler : IRequestHandler<RegisterStudentCommand, Result<string>>
{
    private readonly IStudentRepository _studentRepository;
    private readonly IStringLocalizer<RegisterStudentCommand> _localizer;
    public RegisterStudentCommandHandler(IStudentRepository studentRepository, IStringLocalizer<RegisterStudentCommand> localizer)
    {
        _studentRepository = studentRepository;
        _localizer = localizer;
    }

    public async Task<Result<string>> Handle(RegisterStudentCommand request, CancellationToken cancellationToken)
    {
        var existUser = await _studentRepository.GetStudentByEmail(request.StudentEmail);
        if (existUser != null)
        {
            return Result<string>.Failure(_localizer["Email already exists"]);
        }
        var student = request.ToStudent();
        await _studentRepository.AddStudent(student, request.Password);
        return Result<string>.SuccessMessage(_localizer["Student created successfully"]);
    }
}