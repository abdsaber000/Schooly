using System;
using System.Net;
using MediatR;
using Microsoft.AspNetCore.Http;
using SchoolManagement.Application.Services.AuthenticationService;
using SchoolManagement.Application.Shared;
using SchoolManagement.Domain.Interfaces.IRepositories;

namespace SchoolManagement.Application.Features.Post.Commands.DeletePost;

public class DeletePostCommand : IRequest<Result<string>>
{
    public int Id {get; set;}
    public DeletePostCommand(int id){
        Id = id;
    }
}

public class DeletePostCommandHandler : IRequestHandler<DeletePostCommand, Result<string>>
{
    private readonly IPostRepositry _postRepository;
    private readonly IHttpContextAccessor _contextAccessor;
    private readonly IUserAuthenticationService _authenticationService;

    public DeletePostCommandHandler(
        IPostRepositry postRepository,
        IHttpContextAccessor contextAccessor,
        IUserAuthenticationService authenticationService)
    {
        _postRepository = postRepository;
        _contextAccessor = contextAccessor;
        _authenticationService = authenticationService;
    }

    public async Task<Result<string>> Handle(DeletePostCommand request, CancellationToken cancellationToken)
    {
        var post = await _postRepository.GetPostById(request.Id);
        if(post == null)
        {
            return Result<string>.Failure("Post not found.");
        }

        var user = await _authenticationService.GetCurrentUserAsync(_contextAccessor);
        if (user.Id != post.AuthorId)
        {
            return Result<string>.Failure("User not authorized to delete post.", HttpStatusCode.Forbidden);    
        }

        await _postRepository.DeletePost(post);

        return Result<string>.Success("Post deleted.");
    }
}
