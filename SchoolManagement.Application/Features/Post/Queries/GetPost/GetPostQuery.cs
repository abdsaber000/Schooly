using System;
using System.ComponentModel.DataAnnotations;
using MediatR;
using SchoolManagement.Application.Features.Post.Dtos;
using SchoolManagement.Application.Features.Post.Queries.GetAllPosts;
using SchoolManagement.Application.Shared;
using SchoolManagement.Domain.Interfaces.IRepositories;

namespace SchoolManagement.Application.Features.Post.Queries.GetPost;
using Post = Domain.Entities.Post;

public class GetPostQuery : IRequest<Result<GetAllPostsDto>>
{
    [Required]
    public int Id {get; set;}

    public GetPostQuery(int id)
    {
        Id = id;
    }
}

public class GetPostQueryHandler : IRequestHandler<GetPostQuery, Result<GetAllPostsDto>>
{
    private readonly IPostRepositry _postRepository;

    public GetPostQueryHandler(IPostRepositry postRepository)
    {
        _postRepository = postRepository;
    }

    public async Task<Result<GetAllPostsDto>> Handle(GetPostQuery request, CancellationToken cancellationToken)
    {
        var post = await _postRepository.GetPostById(request.Id);
        if (post == null)
        {
            return Result<GetAllPostsDto>.Failure("Post not found.");
        }

        return Result<GetAllPostsDto>.Success(post.ToPostsDto());
    }
}
