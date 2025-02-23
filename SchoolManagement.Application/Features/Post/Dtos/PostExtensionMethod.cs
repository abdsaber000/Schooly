using System;
using SchoolManagement.Application.Features.Post.Commands.CreatePost;
using SchoolManagement.Domain.Entities;

namespace SchoolManagement.Application.Features.Post.Dtos;

static public class PostExtensionMethod
{
    public static Domain.Entities.Post ToPost(this CreatePostCommand command
        , ApplicationUser author)
    {
        return new Domain.Entities.Post()
        {
            Content = command.Content,
            AuthorId = author.Id,
            Author = author
        };
    }
}
