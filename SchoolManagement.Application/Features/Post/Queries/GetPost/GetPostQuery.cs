using System;
using System.ComponentModel.DataAnnotations;
using MediatR;
using SchoolManagement.Application.Shared;
using SchoolManagement.Domain.Interfaces.IRepositories;
using SchoolManagement.Infrastructure.Repositories;

namespace SchoolManagement.Application.Features.Post.Queries.GetPost;
using Post = Domain.Entities.Post;

public class GetPostQuery : IRequest<Result<Post>>
{
    [Required]
    public int Id {get; set;}

    public GetPostQuery(int id)
    {
        Id = id;
    }
}

public class GetPostQueryHandler : IRequestHandler<GetPostQuery, Result<Post>>
{
    private readonly IPostRepositry _postRepository;

    public GetPostQueryHandler(IPostRepositry postRepository)
    {
        _postRepository = postRepository;
    }

    public async Task<Result<Post>> Handle(GetPostQuery request, CancellationToken cancellationToken)
    {
        var post = await _postRepository.GetPostById(request.Id);
        if (post == null)
        {
            return Result<Post>.Failure("Post not found.");
        }

        return Result<Post>.Success(post);
    }
}
