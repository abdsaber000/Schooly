using System;
using System.ComponentModel.DataAnnotations;
using MediatR;
using Microsoft.IdentityModel.Tokens;
using SchoolManagement.Application.Features.Post.Dtos;
using SchoolManagement.Application.Features.Post.Queries.GetAllPosts;
using SchoolManagement.Application.Shared;
using SchoolManagement.Domain.Interfaces.IRepositories;

namespace SchoolManagement.Application.Features.Post.Queries.GetPostsByAuthor;
using Post = Domain.Entities.Post;
public class GetPostsByAuthorQuery : IRequest<Result<List<GetAllPostsDto>>>
{
    [Required]
    public string AuthorId { get; set; } = string.Empty;

    public GetPostsByAuthorQuery(string authorId)
    {
        AuthorId = authorId;
    }
}

public class GetPostsByAuthorQueryHandler : IRequestHandler<GetPostsByAuthorQuery, Result<List<GetAllPostsDto>>>
{
    private readonly IPostRepositry _postRepository;

    public GetPostsByAuthorQueryHandler(IPostRepositry postRepository)
    {
        _postRepository = postRepository;
    }

    public async Task<Result<List<GetAllPostsDto>>> Handle(GetPostsByAuthorQuery request, CancellationToken cancellationToken)
    {
        var posts = await _postRepository.GetPostsByAuthor(request.AuthorId);
        if (posts.IsNullOrEmpty())
        {
            return Result<List<GetAllPostsDto>>.Failure("No Posts Found.");
        }

        return Result<List<GetAllPostsDto>>.Success(posts.Select(post => post.ToPostsDto()).ToList());
    }
}
