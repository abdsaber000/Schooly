using System;

namespace SchoolManagement.Domain.Entities;

public class Comment
{
    public int Id {get; set;}
    public string Content {get; set;}
    public string AuthorId {get; set;}
    public ApplicationUser Author {get; set;}
}
