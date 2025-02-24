using System;

namespace SchoolManagement.Application.Features.Comment.Query.GetAllQuery;

public class GetAllCommentsDto
{
    public int Id {get; set;}
    public string Content {get; set;} = string.Empty;
    public string AuthorId {get; set;} = string.Empty;
    public DateTime CreatedAt {get; set;}
}
