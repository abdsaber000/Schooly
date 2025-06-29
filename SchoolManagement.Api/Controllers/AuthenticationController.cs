using MediatR;
using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using SchoolManagement.Application.Features.Authentication.Commands.Login;
using SchoolManagement.Application.Features.Authentication.Commands.Register;
using SchoolManagement.Application.Features.PasswordReset.Command.ForgetPassword;
using SchoolManagement.Application.Features.PasswordReset.Command.ResetPassword;
using SchoolManagement.Application.Features.PasswordReset.Command.VerifyCode;
using SchoolManagement.Application.Services.ResponseService;

namespace SchoolManagement.Api.Controllers;

[EnableRateLimiting("ApiPolicy")]
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
    public async Task<IActionResult> Login([FromBody] LoginUserCommand request)
    {
        var result = await _mediator.Send(request);

        if (!result.IsSuccess)
            return StatusCode((int)result.StatusCode, new { message = result.Message });

        var token = result.Token;

        var cookieOptions = new CookieOptions
        {
            HttpOnly = true,
            Secure = true, 
            SameSite = SameSiteMode.Strict,
            Expires = DateTimeOffset.UtcNow.AddDays(30)
        };

        Response.Cookies.Append("jwt_token", token, cookieOptions);
        
        return _responseService.CreateResponse(result);
    }

    
    
    [HttpPost("forgot-password")]
    public async Task<IActionResult> ForgotPassword([FromBody] ForgetPasswordCommand command)
    {
        return _responseService.CreateResponse(await _mediator.Send(command));
    }

    [HttpPost("verify-code")]
    public async Task<IActionResult> VerifyCode([FromBody] VerifyCodeCommand command)
    {
        return _responseService.CreateResponse(await _mediator.Send(command));
    }

    [HttpPost("reset-password")]
    public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordCommand command)
    {
        return _responseService.CreateResponse(await _mediator.Send(command));
    }
}