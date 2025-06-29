using System;
using System.Net;
using MediatR;
using Microsoft.AspNetCore.Identity;
using SchoolManagement.Application.Features.Student.Commands.UpdateStudent;
using SchoolManagement.Application.Features.Student.Dtos;
using SchoolManagement.Application.Shared;
using SchoolManagement.Domain.Entities;
using SchoolManagement.Domain.Enums;
using SchoolManagement.Domain.Interfaces.IRepositories;

namespace SchoolManagement.Application.Features.Student.Commands.DeleteStudent;
using Student = Domain.Entities.Student;
public class UpdateStudentCommandHandler : IRequestHandler<UpdateStudentCommandRequest, Result<UpdateStudentCommandDto>>
{
    private readonly IStudentRepository _studentRepository;
    private readonly UserManager<ApplicationUser> _userManager;
    public UpdateStudentCommandHandler(
        IStudentRepository studentRepository,
        UserManager<ApplicationUser> userManager)
    {
        _studentRepository = studentRepository;
        _userManager = userManager;
    }
    public async Task<Result<UpdateStudentCommandDto>> Handle(UpdateStudentCommandRequest request, CancellationToken cancellationToken)
    {
        var student = await _studentRepository.GetStudentByIdAsync(request.Id);
        if (student == null)
        {
            return Result<UpdateStudentCommandDto>.Failure("User is not found.", HttpStatusCode.NotFound);
        }
        await MapRequestToStudent(request, student);
        await _studentRepository.Update(student);
        return Result<UpdateStudentCommandDto>.Success(student.ToUpdateStudentDto());
    }

    private async Task MapRequestToStudent(UpdateStudentCommandRequest request, Student student)
    {
        if (request.Name != null)
        {
            student.Name = request.Name;
        }
        if (request.Email != null)
        {
            student.Email = request.Email;
            student.UserName = request.Email;
            await _userManager.SetEmailAsync(student, request.Email);
            await _userManager.SetUserNameAsync(student, request.Email);    
        }
        if (request.PhoneNumber != null)
        {
            student.PhoneNumber = request.PhoneNumber;
        }
        if (request.ProfilePictureUrl != null)
        {
            student.ProfilePictureUrl = request.ProfilePictureUrl;
        }
        if (request.Address != null)
        {
            student.Address = request.Address;
        }
        if (request.DateOfJoining != null)
        {
            student.DateOfJoining = (DateOnly)request.DateOfJoining;
        }
        if (request.Department != null)
        {
            student.Department = (Department)request.Department;
        }
        if (request.Grade != null)
        {
            student.Grade = (Grade)request.Grade;
        }
        if (request.Gender != null)
        {
            student.Gender = (Gender)request.Gender;
        }
        if (request.Parent != null)
            {
                
                if (request.Parent.ParentName != null)
                {
                    student.Parent.ParentName = request.Parent.ParentName;
                }
                if (request.Parent.Relation != null)
                {
                    student.Parent.Relation = (Relation)request.Parent.Relation;
                }
                if (request.Parent.Job != null)
                {
                    student.Parent.Job = request.Parent.Job;
                }
                if (request.Parent.Phone1 != null)
                {
                    student.Parent.Phone1 = request.Parent.Phone1;
                }
                if (request.Parent.Phone2 != null)
                {
                    student.Parent.Phone2 = request.Parent.Phone2;
                }
            }
    }
}
