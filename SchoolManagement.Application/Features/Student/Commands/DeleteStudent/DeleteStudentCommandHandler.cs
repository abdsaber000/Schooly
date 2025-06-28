using System;
using System.Net;
using MediatR;
using Microsoft.AspNetCore.Identity;
using SchoolManagement.Application.Shared;
using SchoolManagement.Domain.Entities;
using SchoolManagement.Domain.Interfaces.IRepositories;

namespace SchoolManagement.Application.Features.Student.Commands.DeleteStudent;

public class DeleteStudentCommandHandler : IRequestHandler<DeleteStudentCommandRequest, Result<string>>
{
    private readonly IStudentRepository _studentRepository;
    private readonly UserManager<ApplicationUser> _userManager;
    public DeleteStudentCommandHandler(
        IStudentRepository studentRepository,
        UserManager<ApplicationUser> userManager)
    {
        _studentRepository = studentRepository;
        _userManager = userManager;
    }
    public async Task<Result<string>> Handle(DeleteStudentCommandRequest request, CancellationToken cancellationToken)
    {
        var student = await _studentRepository.GetByIdAsync(request.Id);
        if (student == null)
        {
            return Result<string>.Failure("User is not found.", HttpStatusCode.NotFound);
        }
        await _userManager.DeleteAsync(student);
                
        return Result<string>.SuccessMessage("Student deleted successfully.");
    }
}
