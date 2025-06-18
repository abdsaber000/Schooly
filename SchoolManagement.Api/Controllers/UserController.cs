using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using SchoolManagement.Application.Features.ClassRoom.Queries.GetClassRoomsByUsertId;
using SchoolManagement.Application.Services.ResponseService;

namespace SchoolManagement.Api.Controllers;

[Authorize(AuthenticationSchemes = "Bearer")]
[EnableRateLimiting("ApiPolicy")]
[ApiController]
[Authorize(AuthenticationSchemes = "Bearer")]
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
    [Route("classrooms")]
    public async Task<IActionResult> GetClassRoomsByUserId()
    {
        return _responseService.CreateResponse(await _mediator.Send(new GetClassRoomsByUserIdCommand()));
    }
}