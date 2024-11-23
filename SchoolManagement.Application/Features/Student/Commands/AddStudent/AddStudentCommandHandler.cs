using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using SchoolManagement.Application.Features.Student.Dtos;
using SchoolManagement.Application.Shared;
using SchoolManagement.Domain.Entities;
using SchoolManagement.Domain.Interfaces.Repositories;

namespace SchoolManagement.Application.Features.Student.Commands.AddStudent;

public class AddStudentCommandHandler: IRequestHandler<AddStudentCommand , Result<string>>
{
    private readonly IStudentRepository _studentRepository;
    private readonly IStringLocalizer<AddStudentCommandHandler> _localizer;

    public AddStudentCommandHandler(IStudentRepository studentRepository, IStringLocalizer<AddStudentCommandHandler> localizer)
    {
        _studentRepository = studentRepository;
        _localizer = localizer;
    }

    public async Task<Result<string>> Handle(AddStudentCommand request, CancellationToken cancellationToken)
    {
        var student = request.ToStudent();
        await _studentRepository.AddStudent(student);

        return Result<string>.Success(_localizer["Student created successfully"]);
    }
}