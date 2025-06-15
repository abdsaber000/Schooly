using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using SchoolManagement.Application.Features.FaceRecognition.Commands.RegisterFace;
using SchoolManagement.Application.Services.ResponseService;
using SchoolManagement.Application.Features.FaceRecognition.Commands.VerifyFace;
using SchoolManagement.Application.Features.FaceRecognition.ResetFace;

namespace SchoolManagement.Api.Controllers
{
    [Route("api/face-recognition")]
    [EnableRateLimiting("ApiPolicy")]
    [ApiController]
    public class FaceRecognitionController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IResponseService _responseService;
        public FaceRecognitionController(IMediator mediator
                        , IResponseService responseService)
        {
            _mediator = mediator;
            _responseService = responseService;
        }

        [HttpPost]
        [Route("register-face")]
        public async Task<IActionResult> RegisterFace([FromForm] RegisterFaceCommand command)
        {
            return _responseService.CreateResponse(await _mediator.Send(command));
        }

        [HttpPost]
        [Route("verify-face")]
        public async Task<IActionResult> VerifyFace([FromForm] VerifyFaceCommand request)
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

        [HttpDelete("{id}")]
        public async Task<IActionResult> ResetFace(string id)
        {
            var command = new ResetFaceCommand(id);
            return _responseService.CreateResponse(await _mediator.Send(command));
        }
    }
}
