using System;
using System.ComponentModel.DataAnnotations;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Application.Shared;
using SchoolManagement.Domain.Interfaces.IRepositories;
using SchoolManagement.Infrastructure.Repositories;

namespace SchoolManagement.Application.Features.Post.Commands.UpdatePost;
using Post = Domain.Entities.Post;
public class UpdatePostCommand : IRequest<Result<Post>>
{
    [Required]
    public int Id {get; set;}
    [Required]
    public string Content {get; set;} = string.Empty;
}

public class UpdatePostCommandHandler : IRequestHandler<UpdatePostCommand, Result<Post>>
{
    private readonly IPostRepositry _postRepository;

    public UpdatePostCommandHandler(IPostRepositry postRepository)
    {
        _postRepository = postRepository;
    }

    public async Task<Result<Post>> Handle(UpdatePostCommand request, CancellationToken cancellationToken)
    {
        var post = await _postRepository.GetPostById(request.Id);
        if (post == null)
        {
            return Result<Post>.Failure("Post not found.");
        }

        post.Content = request.Content;
        await _postRepository.UpdatePost(post);

        return Result<Post>.Success(post);
    }
}
