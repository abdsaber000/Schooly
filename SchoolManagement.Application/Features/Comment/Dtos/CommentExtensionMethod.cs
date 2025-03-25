using System;

namespace SchoolManagement.Application.Features.Comment.Dtos;
using Comment = Domain.Entities.Comment;
static public class CommentExtensionMethod
{
    static public CommentDto ToDto(this Comment comment)
    {
        return new CommentDto()
        {
            Id = comment.Id,
            Content = comment.Content,
            CreatedAt = comment.CreatedAt,
            AuthorId = comment.AuthorId,
            AuthorName = comment.Author.Name,
            AuthorEmail = comment.Author.Email ?? ""
        };
    }
}
