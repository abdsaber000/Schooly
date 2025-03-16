using System;

namespace SchoolManagement.Application.Features.Post.Queries.GetAllPosts;


public class CommentsDto
{
    public int Id {get; set;}
    public string Content {get; set;} = string.Empty;
    public DateTime CreatedAt {get; set;}
}

public class GetAllPostsDto
{
    public int Id {get; set;}
    public string Content {get; set;} = string.Empty;
    public Guid ClassRoomId {get; set;}
    public string AuthorId {get; set;} = string.Empty;
    public string AuthorName {get; set;} = string.Empty;
    public ICollection<CommentsDto> Comments {get; set;} = new List<CommentsDto>();
    public DateTime CreatedAt {get; set;}
}
