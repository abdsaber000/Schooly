using System;

namespace SchoolManagement.Domain.Entities;

public class Post
{
    public int Id {get; set;}
    public string Content {get; set;}
    public string AuthorId {get; set;}
    public ApplicationUser Author {get; set;}
    public ICollection<Comment> Comments {get; } = new List<Comment>();
    public ClassRoom ClassRoom {get; set;} = null!;
    public DateTime CreatedAt {get; set;} = DateTime.Now;
}
