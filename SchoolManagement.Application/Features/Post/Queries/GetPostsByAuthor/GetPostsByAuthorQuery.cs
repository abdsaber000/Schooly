using System;
using System.ComponentModel.DataAnnotations;
using MediatR;
using Microsoft.IdentityModel.Tokens;
using SchoolManagement.Application.Shared;
using SchoolManagement.Domain.Interfaces.IRepositories;

namespace SchoolManagement.Application.Features.Post.Queries.GetPostsByAuthor;
using Post = Domain.Entities.Post;
public class GetPostsByAuthorQuery : IRequest<Result<List<Post>>>
{
    [Required]
    public string AuthorId { get; set; } = string.Empty;

    public GetPostsByAuthorQuery(string authorId)
    {
        AuthorId = authorId;
    }
}

public class GetPostsByAuthorQueryHandler : IRequestHandler<GetPostsByAuthorQuery, Result<List<Post>>>
{
    private readonly IPostRepositry _postRepository;

    public GetPostsByAuthorQueryHandler(IPostRepositry postRepository)
    {
        _postRepository = postRepository;
    }

    public async Task<Result<List<Post>>> Handle(GetPostsByAuthorQuery request, CancellationToken cancellationToken)
    {
        var posts = await _postRepository.GetPostsByAuthor(request.AuthorId);
        if (posts.IsNullOrEmpty())
        {
            return Result<List<Post>>.Failure("No Posts Found.");
        }

        return Result<List<Post>>.Success(posts);
    }
}
