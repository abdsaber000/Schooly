using System;
using MediatR;
using SchoolManagement.Application.Features.Pagination;
using SchoolManagement.Domain.Interfaces.IRepositories;

namespace SchoolManagement.Application.Features.Comment.Query.GetAllQuery;
using Comment = Domain.Entities.Comment;
public class GetAllCommentsPagedQuery : IRequest<PagedResult<Comment>>
{
    public int Page {get; set;} = 1;
    public int PageSize {get; set;} = 10;
}


public class GetAllCommentsQueryHandler : IRequestHandler<GetAllCommentsPagedQuery, PagedResult<Comment>>
{
    private readonly ICommentRepositry _commentRepositry;
    public GetAllCommentsQueryHandler(ICommentRepositry commentRepositry){
        _commentRepositry = commentRepositry;
    }
    public async Task<PagedResult<Comment>> Handle(GetAllCommentsPagedQuery request, CancellationToken cancellationToken)
    {
        var result = await _commentRepositry.GetPagedAsync(request.Page, request.PageSize);
        return new PagedResult<Comment>(){
            TotalItems = await _commentRepositry.GetTotalCountAsync(),
            Items = result,
            Page = request.Page,
            PageSize = request.PageSize
        };
    }
}