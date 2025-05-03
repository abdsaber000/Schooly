using System.ComponentModel.DataAnnotations;
using MediatR;
using SchoolManagement.Application.Shared;
using SchoolManagement.Domain.Enums;

namespace SchoolManagement.Application.Features.Lesson.Command.UpdateLeeson;

public class UpdateLessonCommand : IRequest<Result<string>>
{
    [Required]
    public Guid Id { get; set; }
    
    [Required]
    public Guid ClassRoomId { get; set; }
    
    [Required(ErrorMessage = "Title is required")]
    public string Title { get; set; } = string.Empty;

    [Required(ErrorMessage = "Lesson type is required")]
    public LessonType LessonType { get; set; }

    [Required(ErrorMessage = "Date is required")]
    public DateOnly Date { get; set; }

    [Required(ErrorMessage = "Start time is required")]
    public TimeOnly From { get; set; }

    [Required(ErrorMessage = "End time is required")]
    public TimeOnly To { get; set; }
}