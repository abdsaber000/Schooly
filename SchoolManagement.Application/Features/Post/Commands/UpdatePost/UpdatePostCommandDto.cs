using System;

namespace SchoolManagement.Application.Features.Post.Commands.UpdatePost;

public class UpdatePostCommentsDto
{
    public int Id {get; set;}
    public string Content {get; set;} = string.Empty;
    public string AuthorId {get; set;} = string.Empty;
    public string AuthorName {get; set;} = string.Empty;
    public string? ProfilePictureUrl {get; set;} = string.Empty;
    public DateTime CreatedAt { get; set; }
}


public class UpdatePostCommandDto
{
    public int Id { get; set; }
    public string Content { get; set; } = string.Empty;
    public Guid ClassRoomId { get; set; }
    public string AuthorId { get; set; } = string.Empty;
    public string AuthorName { get; set; } = string.Empty;
    public string? ProfilePictureUrl { get; set; }
    public ICollection<UpdatePostCommentsDto> Comments { get; set; } = new List<UpdatePostCommentsDto>();
    public DateTime CreatedAt { get; set; }
}
