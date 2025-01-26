using MediatR;
using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Application.Features.Authentication.Commands.Login;
using SchoolManagement.Application.Features.Authentication.Commands.Register;
using SchoolManagement.Infrastructure.DbContext;

namespace SchoolManagement.Api.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthenticationController : ControllerBase
{
    private readonly IMediator _mediator;
    public AuthenticationController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    [Route("register")]
    public async Task<IActionResult> Register([FromBody] RegisterStudentCommand command)
    {
        return Ok(await _mediator.Send(command));
    }

    [HttpPost]
    [Route("login")]
    public async Task<IActionResult> Login([FromBody] LoginUserCommand command)
    {
        return Ok(await _mediator.Send(command));
    }
}