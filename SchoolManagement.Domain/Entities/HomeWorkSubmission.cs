using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolManagement.Domain.Entities;

public class HomeWorkSubmission
{
    public Guid Id { get; set; } = new Guid();

    [ForeignKey("HomeWork")]
    public Guid HomeWorkId { get; set; }
    public HomeWork HomeWork { get; set; }

    public string StudentId { get; set; }
    public ApplicationUser Student { get; set; }
    
    public string FileUrl { get; set; }
    public string FileName { get; set; }

    public DateTime SubmittedAt { get; set; }
}