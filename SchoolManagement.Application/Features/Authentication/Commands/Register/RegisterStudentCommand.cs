using System.ComponentModel.DataAnnotations;
using MediatR;
using SchoolManagement.Application.Shared;
using SchoolManagement.Domain.Enums;

namespace SchoolManagement.Application.Features.Authentication.Commands.Register;

public class RegisterStudentCommand : IRequest<Result<string>>
{
    [Required(ErrorMessage = "Student name is required")]
    public string StudentName { get; set; } = string.Empty;

    [Required(ErrorMessage =  "Email is required")]
    [EmailAddress(ErrorMessage = "Invalid email format")]
    public string StudentEmail { get; set; } = string.Empty;
    
    [MinLength(6 , ErrorMessage = "Password must be at least 6 characters")]
    public string Password { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "DateOfBirth is required")]
    public DateOnly DateOfBirth { get; set; }
    
    [Required(ErrorMessage = "Gender is required")]
    public Gender Gender { get; set; }

    [Required(ErrorMessage = "Address is required")]
    public string Address { get; set; } = string.Empty;
    [Required(ErrorMessage = "Date Of Joining is required")]
    public DateOnly DateOfJoining { get; set; }
    [Required(ErrorMessage = "Department is required")]
    public Department Department { get; set; }
    [Required(ErrorMessage = "Grade is required")]
    public Grade Grade { get; set; }

    [Required(ErrorMessage = "Parent name is required")]
    public string ParentName { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "Parent relation is required")]
    public Relation ParentRelation { get; set; }

    [Required(ErrorMessage = "Parent job is required")]
    public string ParentJob { get; set; } = string.Empty;

    [Required(ErrorMessage = "Parent phone is required")]
    public string ParentPhone1 { get; set; } = string.Empty;
    public string? ParentPhone2 { get; set; }
}