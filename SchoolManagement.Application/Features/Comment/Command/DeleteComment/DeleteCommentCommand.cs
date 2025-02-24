using System.Net;
using MediatR;
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
    public DeleteCommentCommandHandler(ICommentRepositry commentRepository)
    {
        _commentRepository = commentRepository;
    }
    public async Task<Result<string>> Handle(DeleteCommentCommand request, CancellationToken cancellationToken)
    {
        var comment = await _commentRepository.GetByIdAsync(request.Id);
        if (comment == null)
        {
            return Result<string>.Failure("Comment does not exist.", HttpStatusCode.NotFound);
        }
        await _commentRepository.Delete(comment);
        return Result<string>.SuccessMessage("Comment deleted successfully.");
    }
}