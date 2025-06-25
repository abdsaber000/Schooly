using System;
using SchoolManagement.Application.Features.Comment.Command.CreateComment;
using SchoolManagement.Application.Features.Comment.Command.UpdateComment;

namespace SchoolManagement.Application.Features.Comment.Dtos;
using Comment = Domain.Entities.Comment;
static public class CommentExtensionMethod
{
    private static readonly string _domainPrefix = "https://scholly.runasp.net/api/upload/";
    private static string? HandleUrl(string? imageUrl)
    {
        if (string.IsNullOrEmpty(imageUrl))
        {
            return imageUrl;
        }
        return _domainPrefix + imageUrl;
    }
    static public CommentDto ToDto(this Comment comment)
    {
        return new CommentDto()
        {
            Id = comment.Id,
            Content = comment.Content,
            CreatedAt = comment.CreatedAt,
            AuthorId = comment.AuthorId,
            AuthorName = comment.Author.Name,
            AuthorEmail = comment.Author.Email ?? "",
            ProfilePictureUrl = HandleUrl(comment.Author.ProfilePictureUrl)
        };
    }

    static public UpdateCommentCommandDto ToUpdateCommentDto(this Comment comment)
    {

        return new UpdateCommentCommandDto()
        {
            Id = comment.Id,
            Content = comment.Content,
            AuthorId = comment.AuthorId
        };
    }
    
    static public CreateCommentCommandDto ToCreateCommentDto(this Comment comment)
    {
        return new CreateCommentCommandDto()
        {
            Id = comment.Id,
            Content = comment.Content,
            AuthorId = comment.AuthorId,
            AuthorName = comment.Author.Name,
            CreatedAt = comment.CreatedAt
        };
    }
}
