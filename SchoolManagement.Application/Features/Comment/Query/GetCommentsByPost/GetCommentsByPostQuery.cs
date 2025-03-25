using System;
using MediatR;
using SchoolManagement.Application.Features.Comment.Dtos;
using SchoolManagement.Application.Features.Pagination;
using SchoolManagement.Domain.Interfaces.IRepositories;

namespace SchoolManagement.Application.Features.Comment.Query.GetCommentsByPostQuery;
using Comment = Domain.Entities.Comment;
public class GetCommentsByPostQuery : IRequest<PagedResult<CommentDto>>
{
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 10;
    public int PostId { get; set; }

    public GetCommentsByPostQuery(int page, int pageSize, int postId)
    {
        Page = page;
        PageSize = pageSize;
        PostId = postId;
    }
}


public class GetCommentsByPostQueryHandler : IRequestHandler<GetCommentsByPostQuery, PagedResult<CommentDto>>
{
    private readonly ICommentRepositry _commentRepositry;
    public GetCommentsByPostQueryHandler(ICommentRepositry commentRepositry){
        _commentRepositry = commentRepositry;
    }
    public async Task<PagedResult<CommentDto>> Handle(GetCommentsByPostQuery request, CancellationToken cancellationToken)
    {
        var result = await _commentRepositry.GetByPostId(request.PostId, request.Page, request.PageSize);
        return new PagedResult<CommentDto>(){
            TotalItems = await _commentRepositry.GetByPostIdTotalCount(request.PostId),
            Items = result.Select(comment => comment.ToDto()),
            Page = request.Page,
            PageSize = request.PageSize
        };
    }
}