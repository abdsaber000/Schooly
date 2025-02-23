using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Application.Features.Comment.Command.CreateComment;
using SchoolManagement.Application.Features.Comment.Query.GetAllQuery;
using SchoolManagement.Application.Services.ResponseService;

namespace SchoolManagement.Api.Controllers
{
    [Route("api/comment")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class CommentController : ControllerBase
    {

        private readonly IMediator _mediator;
        private readonly IResponseService _responseService;
        public CommentController(IMediator mediator, IResponseService responseService){
            _mediator = mediator;
            _responseService = responseService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllComments([FromQuery] GetAllCommentsPagedQuery query){
            return _responseService.CreateResponse(await _mediator.Send(query));
        }

        [HttpPost]
        public async Task<IActionResult> CreateComment([FromBody] CreateCommentCommand command){
            return _responseService.CreateResponse(await _mediator.Send(command));
        }
    }
}
