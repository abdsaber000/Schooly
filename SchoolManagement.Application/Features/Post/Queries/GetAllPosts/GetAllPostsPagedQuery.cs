using System;
using MediatR;
using SchoolManagement.Application.Features.Pagination;
using SchoolManagement.Application.Features.Post.Dtos;
using SchoolManagement.Application.Shared;
using SchoolManagement.Domain.Interfaces.IRepositories;
using SchoolManagement.Infrastructure.Repositories;

namespace SchoolManagement.Application.Features.Post.Queries.GetAllPosts;
using Post = Domain.Entities.Post;
public class GetAllPostsPagedQuery : IRequest<PagedResult<GetAllPostsDto>>
{
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 10;
    public Guid ClassRoomId { get; set; } = Guid.Empty;
}

public class GetAllPostsPagedQueryHandler : IRequestHandler<GetAllPostsPagedQuery, PagedResult<GetAllPostsDto>>
{
    private readonly IPostRepositry _postRepository;

    public GetAllPostsPagedQueryHandler(IPostRepositry postRepository)
    {
        _postRepository = postRepository;
    }

    public async Task<PagedResult<GetAllPostsDto>> Handle(GetAllPostsPagedQuery request, CancellationToken cancellationToken)
    {
        var posts = await _postRepository.GetPagedAsync(request.Page, request.PageSize);
        if(request.ClassRoomId != Guid.Empty){
            posts = posts.Where(p => p.ClassRoom.Id == request.ClassRoomId).ToList();
        }
        var results = posts.Select(post => post.ToPostsDto()).OrderByDescending(p => p.CreatedAt).ToList();
        return new PagedResult<GetAllPostsDto>(){
            TotalItems = await _postRepository.GetTotalPostsCount(),
            Items = results,
            Page = request.Page,
            PageSize = request.PageSize
        };
    }
}
