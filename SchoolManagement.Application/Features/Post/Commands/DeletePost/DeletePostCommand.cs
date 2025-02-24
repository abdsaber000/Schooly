using System;
using MediatR;
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

    public DeletePostCommandHandler(IPostRepositry postRepository)
    {
        _postRepository = postRepository;
    }

    public async Task<Result<string>> Handle(DeletePostCommand request, CancellationToken cancellationToken)
    {
        var post = await _postRepository.GetPostById(request.Id);
        if(post == null)
        {
            return Result<string>.Failure("Post not found.");
        }
        await _postRepository.DeletePost(post);

        return Result<string>.Success("Post deleted.");
    }
}
