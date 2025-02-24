using System;
using SchoolManagement.Application.Features.Post.Commands.CreatePost;
using SchoolManagement.Application.Features.Post.Queries.GetAllPosts;
using SchoolManagement.Domain.Entities;

namespace SchoolManagement.Application.Features.Post.Dtos;
using Post = Domain.Entities.Post;
static public class PostExtensionMethod
{
    public static Post ToPost(this CreatePostCommand command
        , ApplicationUser author)
    {
        return new Post()
        {
            Content = command.Content,
            AuthorId = author.Id,
            Author = author
        };
    }

    public static GetAllPostsDto ToPostsDto(this Post post) {
        return new GetAllPostsDto(){
            Id = post.Id,
            Content = post.Content,
            CreatedAt = post.CreatedAt,
            Comments = post.Comments.Select(comment => new CommentsDto(){
                Id = comment.Id,
                Content = comment.Content,
                CreatedAt = comment.CreatedAt
            })
            .OrderBy(c => c.CreatedAt)
            .ToList()
        };
    }

    
}
