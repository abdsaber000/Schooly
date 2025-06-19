using System;
using MediatR;
using SchoolManagement.Application.Shared;

namespace SchoolManagement.Application.Features.Profile.Queries.GetUserInfo;

public class GetUserInfoQueryRequest : IRequest<Result<GetUserInfoQueryDto>>
{
    public string Id { get; set; } = string.Empty;

    public GetUserInfoQueryRequest(string id)
    {
        Id = id;
    }
}

