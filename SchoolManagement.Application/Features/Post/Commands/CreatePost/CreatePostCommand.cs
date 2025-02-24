using System;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using SchoolManagement.Application.Features.Post.Dtos;
using SchoolManagement.Application.Shared;
using SchoolManagement.Domain.Entities;
using SchoolManagement.Domain.Interfaces.IRepositories;

namespace SchoolManagement.Application.Features.Post.Commands.CreatePost;

public class CreatePostCommand : IRequest<Result<string>>
{
    [Required]
    public string Content { get; set; } = string.Empty;
}

public class CreatePostCommandHandler : IRequestHandler<CreatePostCommand, Result<string>>
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IPostRepositry _postRepositry;

    public CreatePostCommandHandler(IHttpContextAccessor httpContextAccessor
        , UserManager<ApplicationUser> userManager
        , IPostRepositry postRepositry)
    {
        _httpContextAccessor = httpContextAccessor;
        _userManager = userManager;
        _postRepositry = postRepositry;
    }

   
    public async Task<Result<string>> Handle(CreatePostCommand request, CancellationToken cancellationToken)
    {
        var user = _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User).Result;
        var post = request.ToPost(user);
        await _postRepositry.CreatePost(post);
        return Result<string>.SuccessMessage("Post created successfully");
    }
}
