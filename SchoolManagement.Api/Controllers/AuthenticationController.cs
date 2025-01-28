using MediatR;
using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Application.Features.Authentication.Commands.Login;
using SchoolManagement.Application.Features.Authentication.Commands.Register;
using SchoolManagement.Application.Services.ResponseService;

namespace SchoolManagement.Api.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthenticationController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IResponseService _responseService;
    public AuthenticationController(IMediator mediator 
                    , IResponseService responseService)
    {
        _mediator = mediator;
        _responseService = responseService;
    }

    [HttpPost]
    [Route("register")]
    public async Task<IActionResult> Register([FromBody] RegisterStudentCommand command)
    {
        return _responseService.CreateResponse(await _mediator.Send(command));
    }

    [HttpPost]
    [Route("login")]
    public async Task<IActionResult> Login([FromBody] LoginUserCommand command)
    {
        return _responseService.CreateResponse(await _mediator.Send(command));
    }
}