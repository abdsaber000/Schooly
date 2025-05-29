using System.Net;
using MediatR;
using Microsoft.AspNetCore.Http;
using SchoolManagement.Application.Services.AuthenticationService;
using SchoolManagement.Application.Shared;
using SchoolManagement.Domain.Interfaces.IRepositories;

namespace SchoolManagement.Application.Features.Comment.Command.DeleteComment;

public class DeleteCommentCommand : IRequest<Result<string>>
{
    public int Id { get; set; }
}

public class DeleteCommentCommandHandler : IRequestHandler<DeleteCommentCommand, Result<string>>
{
    private readonly ICommentRepositry _commentRepository;
    private readonly IHttpContextAccessor _contextAccessor;
    private readonly IUserAuthenticationService _authenticationService;
    public DeleteCommentCommandHandler(
        ICommentRepositry commentRepository,
        IHttpContextAccessor contextAccessor,
        IUserAuthenticationService authenticationService)
    {
        _commentRepository = commentRepository;
        _contextAccessor = contextAccessor;
        _authenticationService = authenticationService;
    }
    public async Task<Result<string>> Handle(DeleteCommentCommand request, CancellationToken cancellationToken)
    {
        var comment = await _commentRepository.GetByIdAsync(request.Id);
        if (comment == null)
        {
            return Result<string>.Failure("Comment does not exist.", HttpStatusCode.NotFound);
        }
        var user = await _authenticationService.GetCurrentUserAsync(_contextAccessor);
        if (user.Id != comment.AuthorId)
        {
            return Result<string>.Failure("You are not authorized to delete this comment.", HttpStatusCode.Unauthorized);
        }
        await _commentRepository.Delete(comment);
        return Result<string>.SuccessMessage("Comment deleted successfully.");
    }
}