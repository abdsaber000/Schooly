using System;
using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Http;
using SchoolManagement.Application.Features.Pagination;
using SchoolManagement.Application.Features.Post.Dtos;
using SchoolManagement.Application.Services.AuthenticationService;
using SchoolManagement.Application.Shared;
using SchoolManagement.Domain.Entities;
using SchoolManagement.Domain.Interfaces.IRepositories;
using SchoolManagement.Infrastructure.Repositories;

namespace SchoolManagement.Application.Features.Post.Queries.GetAllPosts;

using Post = Domain.Entities.Post;
using ClassRoom = Domain.Entities.ClassRoom;
public class GetAllPostsPagedQuery : IRequest<PagedResult<GetAllPostsDto>>
{
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 10;
    public Guid ClassRoomId { get; set; } = Guid.Empty;
}

public class GetAllPostsPagedQueryHandler : IRequestHandler<GetAllPostsPagedQuery, PagedResult<GetAllPostsDto>>
{
    private readonly IPostRepositry _postRepository;
    private readonly IStudentClassRoomRepository _studentClassRoomRepository;
    private readonly IClassRoomRepository _classRoomRepository;
    private readonly IHttpContextAccessor _contextAccessor;
    private readonly IUserAuthenticationService _authenticationService;

    public GetAllPostsPagedQueryHandler(
        IPostRepositry postRepository,
        IStudentClassRoomRepository studentClassRoomRepository,
        IClassRoomRepository classRoomRepository,
        IHttpContextAccessor contextAccessor,
        IUserAuthenticationService authenticationService)
    {
        _postRepository = postRepository;
        _studentClassRoomRepository = studentClassRoomRepository;
        _classRoomRepository = classRoomRepository;
        _contextAccessor = contextAccessor;
        _authenticationService = authenticationService;

    }

    public async Task<PagedResult<GetAllPostsDto>> Handle(GetAllPostsPagedQuery request, CancellationToken cancellationToken)
    {
        var user = await _authenticationService.GetCurrentUserAsync(_contextAccessor);
        var userClaim = _contextAccessor.HttpContext?.User;
        if (userClaim == null)
        {
            throw new UnauthorizedAccessException("User is not authenticated.");
        }
        var posts = await _postRepository.GetAllPosts();
        if (request.ClassRoomId != Guid.Empty)
        {
            posts = posts.Where(p => (p.ClassRoom != null)
                && p.ClassRoom.Id == request.ClassRoomId).ToList();
        }
        else
        {
            var classRoomIds = await GetClassRoomIdsAsync(user.Id, userClaim);
            if ((classRoomIds is null) || (classRoomIds.Count == 0))
            {
                return new PagedResult<GetAllPostsDto>()
                {
                    TotalItems = 0,
                    Items = new List<GetAllPostsDto>(),
                    Page = request.Page,
                    PageSize = request.PageSize
                };
            }
            posts = posts.Where(p => (p.ClassRoom != null)
                && classRoomIds.Contains(p.ClassRoom.Id.ToString())).ToList();
        }
        var results = posts
                .Skip((request.Page - 1) * request.PageSize)    
                .Take(request.PageSize)
                .Select(post => post.ToPostsDto())
                .OrderByDescending(p => p.CreatedAt).ToList();
        return new PagedResult<GetAllPostsDto>()
        {
            TotalItems = posts.Count,
            Items = results,
            Page = request.Page,
            PageSize = request.PageSize
        };
    }


    private async Task<List<string>> GetClassRoomIdsAsync(string userId, ClaimsPrincipal userClaim)
    {
        List<ClassRoom> classRooms = new List<ClassRoom>();
        if (userClaim.IsInRole(Roles.Student))
        {
            classRooms = await _studentClassRoomRepository.GetAllClassRoomsByStudentId(userId);
        }
        else if (userClaim.IsInRole(Roles.Teacher))
        {
            classRooms = await _classRoomRepository.GetAllClassRoomsByTeacherId(userId);
        }
        return classRooms.Select(c => c.Id.ToString()).ToList();
    }
}
