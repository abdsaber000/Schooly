using System;
using System.Net;
using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using SchoolManagement.Application.Shared;
using SchoolManagement.Domain.Entities;
using SchoolManagement.Domain.Interfaces.IRepositories;

namespace SchoolManagement.Application.Features.Comment.Command.CreateComment;
using Comment = Domain.Entities.Comment;
public class CreateCommentCommand : IRequest<Result<string>>
{
    public string Content { get; set; } = string.Empty;
    public int PostId { get; set; } 
}

public class CreateCommentCommandHandler : IRequestHandler<CreateCommentCommand, Result<string>>
{
    private readonly ICommentRepositry _commentRepositry;
    private readonly IPostRepositry _postRepositry;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly UserManager<ApplicationUser> _userManager;
    public CreateCommentCommandHandler(ICommentRepositry commentRepositry
        , IPostRepositry postRepositry
        , IHttpContextAccessor httpContextAccessor
        , UserManager<ApplicationUser> userManager)
    {
        _commentRepositry = commentRepositry;
        _postRepositry = postRepositry;
        _httpContextAccessor = httpContextAccessor;
        _userManager = userManager;
    }
    public async Task<Result<string>> Handle(CreateCommentCommand request, CancellationToken cancellationToken)
    {
        var userIdClaim = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier);

        if (userIdClaim is null)
        {
            return Result<string>.Failure("User is not authenticated.", HttpStatusCode.Unauthorized);
        }
        var userId = userIdClaim.Value;
        var user = await _userManager.FindByIdAsync(userId);
        if(user is null){
            return Result<string>.Failure("User is not found.", HttpStatusCode.NotFound);
        }
        var post = await _postRepositry.GetPostById(request.PostId);

        if(post is null)
        {
            return Result<string>.Failure("Post not found.", HttpStatusCode.NotFound);
        }

        var comment = new Comment()
        {
            Content = request.Content,
            Author = user,
            AuthorId = user.Id,
            Post = post,
            PostId = post.Id
        };
        await _commentRepositry.AddAsync(comment);
        
        post.Comments.Add(comment);
        await _postRepositry.UpdatePost(post);
        return Result<string>.SuccessMessage("Comment is added successfully.");
    }
}
