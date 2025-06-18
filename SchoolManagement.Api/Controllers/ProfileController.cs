using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using SchoolManagement.Application.Features.Profile.Commands.UpdateProfile;
using SchoolManagement.Application.Features.Profile.Queries.GetProfile;
using SchoolManagement.Application.Services.ResponseService;

namespace SchoolManagement.Api.Controllers
{
    [EnableRateLimiting("ApiPolicy")]
    [Route("api/profile")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class ProfileController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IResponseService _responseService;

        public ProfileController(IMediator mediator, IResponseService responseService)
        {
            _mediator = mediator;
            _responseService = responseService;
        }


        [HttpGet]
        public async Task<IActionResult> GetProfile()
        {
            return _responseService.CreateResponse(await _mediator.Send(new GetProfileQueryRequest()));
        }

        [HttpPut]
        public async Task<IActionResult> UpdateProfile([FromBody] UpdateProfileCommandRequest request)
        {
            return _responseService.CreateResponse(await _mediator.Send(request));   
        }
    }
}
