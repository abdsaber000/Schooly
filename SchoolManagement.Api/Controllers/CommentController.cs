using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Application.Features.Comment.Command.CreateComment;
using SchoolManagement.Application.Features.Comment.Command.DeleteComment;
using SchoolManagement.Application.Features.Comment.Command.UpdateComment;
using SchoolManagement.Application.Features.Comment.Query.GetAllQuery;
using SchoolManagement.Application.Features.Comment.Query.GetCommentsByPostQuery;
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
        [Route("all")]
        public async Task<IActionResult> GetAllComments([FromQuery] GetAllCommentsPagedQuery query){
            return _responseService.CreateResponse(await _mediator.Send(query));
        }

        [HttpGet]
        [Route("post/{postId:int}")]
        public async Task<IActionResult> GetCommentsByPost([FromRoute] int postId, [FromQuery] int page = 1, [FromQuery] int pageSize = 10){
            return _responseService.CreateResponse(await _mediator.Send(new GetCommentsByPostQuery(page, pageSize, postId)));   
        }

        [HttpPost]
        public async Task<IActionResult> CreateComment([FromBody] CreateCommentCommand command){
            return _responseService.CreateResponse(await _mediator.Send(command));
        }

        [HttpPut]
        public async Task<IActionResult> UpdateComment([FromBody] UpdateCommentCommand command){
            return _responseService.CreateResponse(await _mediator.Send(command));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteComment([FromRoute] DeleteCommentCommand command){
            return _responseService.CreateResponse(await _mediator.Send(command));
        }
    }
}
