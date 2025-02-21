using System;

namespace SchoolManagement.Domain.Entities;

public class Post
{
    public int Id {get; set;}
    public string Content {get; set;}
    public string AuthorId {get; set;}
    public ApplicationUser Author {get; set;}
    public List<Comment> Comments {get; set;} = new List<Comment>();
    public DateTime CreatedAt {get; set;} = DateTime.Now;
}
