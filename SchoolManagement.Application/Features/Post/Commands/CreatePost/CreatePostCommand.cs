using System;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using SchoolManagement.Application.Features.Post.Dtos;
using SchoolManagement.Application.Shared;
using SchoolManagement.Domain.Entities;
using SchoolManagement.Domain.Interfaces.IRepositories;

namespace SchoolManagement.Application.Features.Post.Commands.CreatePost;

public class CreatePostCommand : IRequest<Result<string>>
{
    [Required]
    [MinLength(1, ErrorMessage = "Content must be at least 1 character")]
    public string Content { get; set; } = string.Empty;
    [Required]
    public Guid ClassRoomId { get; set; } = Guid.Empty;
}

public class CreatePostCommandHandler : IRequestHandler<CreatePostCommand, Result<string>>
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IPostRepositry _postRepositry;
    private readonly IClassRoomRepository _classRoomRepository;

    public CreatePostCommandHandler(IHttpContextAccessor httpContextAccessor
        , UserManager<ApplicationUser> userManager
        , IPostRepositry postRepositry
        , IClassRoomRepository classRoomRepository)
    {
        _httpContextAccessor = httpContextAccessor;
        _userManager = userManager;
        _postRepositry = postRepositry;
        _classRoomRepository = classRoomRepository;
    }

   
    public async Task<Result<string>> Handle(CreatePostCommand request, CancellationToken cancellationToken)
    {
        var classRoom = await _classRoomRepository.GetByIdAsync(request.ClassRoomId);
        if(classRoom == null){
            return Result<string>.Failure("Class Room id is not found.", HttpStatusCode.NotFound);
        }

        var user = _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User).Result;
        var post = request.ToPost(user, classRoom);
        await _postRepositry.CreatePost(post);
        classRoom.Posts.Add(post);
        await _classRoomRepository.Update(classRoom);
        return Result<string>.SuccessMessage("Post created successfully");
    }
}
