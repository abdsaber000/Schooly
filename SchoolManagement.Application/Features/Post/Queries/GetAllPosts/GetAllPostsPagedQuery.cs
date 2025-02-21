using System;
using MediatR;
using SchoolManagement.Application.Features.Pagination;
using SchoolManagement.Application.Shared;
using SchoolManagement.Domain.Interfaces.IRepositories;
using SchoolManagement.Infrastructure.Repositories;

namespace SchoolManagement.Application.Features.Post.Queries.GetAllPosts;
using Post = Domain.Entities.Post;
public class GetAllPostsPagedQuery : IRequest<PagedResult<Post>>
{
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}

public class GetAllPostsPagedQueryHandler : IRequestHandler<GetAllPostsPagedQuery, PagedResult<Post>>
{
    private readonly IPostRepositry _postRepository;

    public GetAllPostsPagedQueryHandler(IPostRepositry postRepository)
    {
        _postRepository = postRepository;
    }

    public async Task<PagedResult<Post>> Handle(GetAllPostsPagedQuery request, CancellationToken cancellationToken)
    {
        var posts = await _postRepository.GetPagedAsync(request.Page, request.PageSize);
        return new PagedResult<Post>(){
            TotalItems = await _postRepository.GetTotalPostsCount(),
            Items = posts,
            Page = request.Page,
            PageSize = request.PageSize
        };
    }
}
