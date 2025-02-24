using System.ComponentModel.DataAnnotations;
using MediatR;
using SchoolManagement.Application.Shared;
using SchoolManagement.Domain.Interfaces.IRepositories;

namespace SchoolManagement.Application.Features.Comment.Command.UpdateComment;
using Comment = Domain.Entities.Comment;
public class UpdateCommentCommand : IRequest<Result<string>>
{
    [Required]
    public Comment Comment {get; set;}  = null!;
}

public class UpdateCommentCommandHandler : IRequestHandler<UpdateCommentCommand, Result<string>>
{
    private readonly ICommentRepositry _commentRepository;
    public UpdateCommentCommandHandler(ICommentRepositry commentRepository)
    {
        _commentRepository = commentRepository;
    }
    public async Task<Result<string>> Handle(UpdateCommentCommand request, CancellationToken cancellationToken)
    {
        await _commentRepository.Update(request.Comment);
        return Result<string>.SuccessMessage("Comment updated successfully.");
    }
}
