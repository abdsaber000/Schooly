using System;
using MediatR;
using SchoolManagement.Application.Features.Student.Commands.UpdateStudent;
using SchoolManagement.Application.Shared;
using SchoolManagement.Domain.Enums;

namespace SchoolManagement.Application.Features.Student.Commands.DeleteStudent;

public class UpdateParentRequest
{
    public string? ParentName { get; set; }
    public Relation? Relation { get; set; }
    public string? Job { get; set; }
    public string? Phone1 { get; set; }
    public string? Phone2 { get; set; }
}

public class UpdateStudentCommandRequest : IRequest<Result<UpdateStudentCommandDto>>
{
    required public string Id { get; set; }
    public string? Name { get; set; }
    public string? Email { get; set; }
    public string? PhoneNumber { get; set; }
    public string? ProfilePictureUrl { get; set; }
    public UpdateParentRequest? Parent { get; set; }
    public string? Address { get; set; }
    public DateOnly? DateOfJoining { get; set; }
    public Department? Department { get; set; }
    public Grade? Grade { get; set; }

}
