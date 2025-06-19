using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using SchoolManagement.Application.Features.ClassRoom.Queries.GetClassRoomsByUsertId;
using SchoolManagement.Application.Features.Profile.Queries.GetUserInfo;
using SchoolManagement.Application.Services.ResponseService;

namespace SchoolManagement.Api.Controllers;

[EnableRateLimiting("ApiPolicy")]
[ApiController]
[Route("api/user")]
public class UserController : ControllerBase
{
    private readonly IResponseService _responseService;
    private readonly IMediator _mediator;
    public UserController(IResponseService responseService, IMediator mediator)
    {
        _responseService = responseService;
        _mediator = mediator;
    }

    [HttpGet]
    [Authorize(AuthenticationSchemes = "Bearer")]
    [Route("classrooms")]
    public async Task<IActionResult> GetClassRoomsByUserId()
    {
        return _responseService.CreateResponse(await _mediator.Send(new GetClassRoomsByUserIdCommand()));
    }

    [HttpGet("info/{id}")]
    public async Task<IActionResult> GetUserInfo(string id)
    {
        var result = await _mediator.Send(new GetUserInfoQueryRequest(id));
        return _responseService.CreateResponse(result);
    }
}