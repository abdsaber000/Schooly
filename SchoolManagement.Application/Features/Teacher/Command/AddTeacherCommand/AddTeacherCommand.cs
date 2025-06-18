using System.ComponentModel.DataAnnotations;
using MediatR;
using SchoolManagement.Application.Shared;
using SchoolManagement.Domain.Enums;

namespace SchoolManagement.Application.Features.Teacher.Command.AddTeacherCommand;

public class AddTeacherCommand : IRequest<Result<string>>
{
    [Required]
    public string name { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "Phone number is required")]
    [RegularExpression(@"^(010|011|012|015)\d{8}$", ErrorMessage = "PhoneNumberInvalid")]
    public string PhoneNumber { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "DateOfBirth is required")]
    public DateOnly DateOfBirth { get; set; }
    
    [Required(ErrorMessage =  "Email is required")]
    [EmailAddress(ErrorMessage = "Invalid email format")]
    public string Email { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "Gender is required")]
    public Gender Gender { get; set; }
    
    [Required]
    [MinLength(6 , ErrorMessage = "Password must be at least 6 characters")]
    public string Password { get; set; } = string.Empty;
}