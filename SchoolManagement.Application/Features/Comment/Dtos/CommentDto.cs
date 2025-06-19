using System;

namespace SchoolManagement.Application.Features.Comment.Dtos;


public class CommentDto
{
    public int Id {get; set;}
    public string Content {get; set;} = string.Empty;
    public string AuthorId {get; set;} = string.Empty;
    public string AuthorName {get; set;} = string.Empty;
    public string AuthorEmail {get; set;} = string.Empty;
    public string? ProfilePictureUrl { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
}
