using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using SchoolManagement.Application.Features.Post.Commands.CreatePost;
using SchoolManagement.Application.Features.Post.Commands.DeletePost;
using SchoolManagement.Application.Features.Post.Commands.UpdatePost;
using SchoolManagement.Application.Features.Post.Queries.GetAllPosts;
using SchoolManagement.Application.Features.Post.Queries.GetPost;
using SchoolManagement.Application.Features.Post.Queries.GetPostsByAuthor;
using SchoolManagement.Application.Services.ResponseService;

namespace SchoolManagement.Api.Controllers
{
    [EnableRateLimiting("ApiPolicy")]
    [Route("api/post")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class PostController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IResponseService _responseService;

        public PostController(IMediator mediator, IResponseService responseService)
        {
            _mediator = mediator;
            _responseService = responseService;
        }

        [HttpPost]
        public async Task<IActionResult> CreatePost([FromBody] CreatePostCommand command)
        {
            return _responseService.CreateResponse(await _mediator.Send(command));
        }

        [HttpGet]
        [Route("all")]
        public async Task<IActionResult> GetAllPosts([FromQuery] GetAllPostsPagedQuery query)
        {
            return _responseService.CreateResponse(await _mediator.Send(query));
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetPostById(int id)
        {
            return _responseService.CreateResponse(await _mediator.Send(new GetPostQuery(id)));
        }

        [HttpGet("author/{id}")]
        public async Task<IActionResult> GetPostsByAuthorId(string id){
            return _responseService.CreateResponse(await _mediator.Send(new GetPostsByAuthorQuery(id)));
        }

        [HttpPut]
        public async Task<IActionResult> UpdatePost(UpdatePostCommand command)
        {
            return _responseService.CreateResponse(await _mediator.Send(command));
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeletePost(int id)
        {
            return _responseService.CreateResponse(await _mediator.Send(new DeletePostCommand(id)));
        }
    }
}
