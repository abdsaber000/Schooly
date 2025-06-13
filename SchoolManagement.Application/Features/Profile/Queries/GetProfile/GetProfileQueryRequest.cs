using System;
using MediatR;
using SchoolManagement.Application.Shared;

namespace SchoolManagement.Application.Features.Profile.Queries.GetProfile;

public class GetProfileQueryRequest : IRequest<Result<GetProfileQueryDto>>
{

}
