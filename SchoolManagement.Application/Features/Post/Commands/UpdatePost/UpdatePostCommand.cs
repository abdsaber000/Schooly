using System;
using System.ComponentModel.DataAnnotations;
using System.Net;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Application.Features.Post.Dtos;
using SchoolManagement.Application.Services.AuthenticationService;
using SchoolManagement.Application.Shared;
using SchoolManagement.Domain.Interfaces.IRepositories;
using SchoolManagement.Infrastructure.Repositories;

namespace SchoolManagement.Application.Features.Post.Commands.UpdatePost;
using Post = Domain.Entities.Post;
public class UpdatePostCommand : IRequest<Result<UpdatePostCommandDto>>
{
    [Required]
    public int Id {get; set;}
    [Required]
    public string Content {get; set;} = string.Empty;
}

public class UpdatePostCommandHandler : IRequestHandler<UpdatePostCommand, Result<UpdatePostCommandDto>>
{
    private readonly IPostRepositry _postRepository;
    private readonly IHttpContextAccessor _contextAccessor;
    private readonly IUserAuthenticationService _authenticationService;

    public UpdatePostCommandHandler(
        IPostRepositry postRepository,
        IHttpContextAccessor contextAccessor,
        IUserAuthenticationService authenticationService)
    {
        _postRepository = postRepository;
        _contextAccessor = contextAccessor;
        _authenticationService = authenticationService;
    }

    public async Task<Result<UpdatePostCommandDto>> Handle(UpdatePostCommand request, CancellationToken cancellationToken)
    {
        var post = await _postRepository.GetPostById(request.Id);
        if (post == null)
        {
            return Result<UpdatePostCommandDto>.Failure("Post not found.");
        }

        var user = await _authenticationService.GetCurrentUserAsync(_contextAccessor);

        if (user.Id != post.AuthorId)
        {
            return Result<UpdatePostCommandDto>.Failure("User not authorized to update the post.", HttpStatusCode.Forbidden);
        }

        post.Content = request.Content;
        await _postRepository.UpdatePost(post);

        return Result<UpdatePostCommandDto>.Success(post.ToUpdatePostDto());
    }
}
