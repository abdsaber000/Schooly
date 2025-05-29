using System;

namespace SchoolManagement.Application.Features.Comment.Command.UpdateComment;

public class UpdateCommentCommandDto
{
    public int Id { get; set; }
    public string Content { get; set; } = string.Empty;
    public string AuthorId { get; set; } = string.Empty;
}
