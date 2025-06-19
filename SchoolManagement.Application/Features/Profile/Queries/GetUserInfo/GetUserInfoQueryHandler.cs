using System;
using MediatR;
using Microsoft.AspNetCore.Identity;
using SchoolManagement.Application.Features.Profile.Dtos;
using SchoolManagement.Application.Shared;
using SchoolManagement.Domain.Entities;

namespace SchoolManagement.Application.Features.Profile.Queries.GetUserInfo;

public class GetUserInfoQueryHandler : IRequestHandler<GetUserInfoQueryRequest, Result<GetUserInfoQueryDto>>
{
    private readonly UserManager<ApplicationUser> _userManager;

    public GetUserInfoQueryHandler(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task<Result<GetUserInfoQueryDto>> Handle(GetUserInfoQueryRequest request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByIdAsync(request.Id);
        if (user == null)
        {
            return Result<GetUserInfoQueryDto>.Failure("User not found");
        }

        return Result<GetUserInfoQueryDto>.Success(user.ToUserInfoQueryDto());
    }
}