using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using SchoolManagement.Application.Features.Email.Command.SendEmail;
using SchoolManagement.Application.Services.ResponseService;
using System;
using System.Threading.Tasks;

namespace SchoolManagement.Api.Controllers
{
    [ApiController]
    [Route("api/email")]
    public class EmailController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IResponseService _responseService;
        private readonly IStringLocalizer<EmailController> _localizer;

        public EmailController(IMediator mediator, IResponseService responseService, IStringLocalizer<EmailController> localizer)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _responseService = responseService ?? throw new ArgumentNullException(nameof(responseService));
            _localizer = localizer;
        }

        [HttpPost("send")]
        public async Task<IActionResult> SendEmail([FromForm] SendEmailCommand command)
        {
            return _responseService.CreateResponse(await _mediator.Send(command));
        }
    }
}
