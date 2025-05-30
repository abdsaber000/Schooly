using System.ComponentModel.DataAnnotations;
using System.Net;
using MediatR;
using Microsoft.AspNetCore.Http;
using SchoolManagement.Application.Services.AuthenticationService;
using SchoolManagement.Application.Shared;
using SchoolManagement.Domain.Interfaces.IRepositories;
using SchoolManagement.Application.Features.Comment.Dtos;
namespace SchoolManagement.Application.Features.Comment.Command.UpdateComment;
using Comment = Domain.Entities.Comment;
public class UpdateCommentCommand : IRequest<Result<UpdateCommentCommandDto>>
{
    public int Id { get; set; }
    [MinLength(1)]
    public string Content { get; set; } = string.Empty;
}

public class UpdateCommentCommandHandler : IRequestHandler<UpdateCommentCommand, Result<UpdateCommentCommandDto>>
{
    private readonly ICommentRepositry _commentRepository;
    private readonly IUserAuthenticationService _authenticationService;
    private readonly IHttpContextAccessor _contextAccessor;
    public UpdateCommentCommandHandler(
        ICommentRepositry commentRepository,
        IUserAuthenticationService authenticationService,
        IHttpContextAccessor contextAccessor)
    {
        _commentRepository = commentRepository;
        _authenticationService = authenticationService;
        _contextAccessor = contextAccessor;
    }
    public async Task<Result<UpdateCommentCommandDto>> Handle(UpdateCommentCommand request, CancellationToken cancellationToken)
    {
        var comment = await _commentRepository.GetByIdAsync(request.Id);
        if (comment is null)
        {
            return Result<UpdateCommentCommandDto>.Failure("Comment not found.", HttpStatusCode.NotFound);
        }

        var user = await _authenticationService.GetCurrentUserAsync(_contextAccessor);
        if (user.Id != comment.AuthorId)
        {
            return Result<UpdateCommentCommandDto>.Failure("You are not authorized to update this comment.", HttpStatusCode.Forbidden);
        }
        comment.Content = request.Content;
        await _commentRepository.Update(comment);
        return Result<UpdateCommentCommandDto>.Success(comment.ToUpdateCommentDto());
    }
}
