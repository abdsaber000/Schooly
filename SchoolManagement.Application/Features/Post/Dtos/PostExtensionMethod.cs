using System;
using SchoolManagement.Application.Features.Post.Commands.CreatePost;
using SchoolManagement.Application.Features.Post.Queries.GetAllPosts;
using SchoolManagement.Domain.Entities;

namespace SchoolManagement.Application.Features.Post.Dtos;
using Post = Domain.Entities.Post;
using ClassRoom = Domain.Entities.ClassRoom;
static public class PostExtensionMethod
{
    public static Post ToPost(this CreatePostCommand command
        , ApplicationUser author
        , ClassRoom classRoom)
    {
        return new Post()
        {
            Content = command.Content,
            AuthorId = author.Id,
            Author = author,
            ClassRoom = classRoom
        };
    }

    public static GetAllPostsDto ToPostsDto(this Post post) {
        return new GetAllPostsDto(){
            Id = post.Id,
            Content = post.Content,
            CreatedAt = post.CreatedAt,
            ClassRoomId = post.ClassRoom.Id,
            AuthorId = post.Author.Id,
            AuthorName = post.Author.Name,
            ProfilePictureUrl = post.Author.ProfilePictureUrl,
            Comments = post.Comments.Select(comment => new CommentsDto()
            {
                Id = comment.Id,
                Content = comment.Content,
                CreatedAt = comment.CreatedAt,
                AuthorId = comment.AuthorId,
                AuthorName = comment.Author.Name,
                ProfilePictureUrl = comment.Author.ProfilePictureUrl
            })
            .OrderBy(c => c.CreatedAt)
            .ToList()
        };
    }

    
}
