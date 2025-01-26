using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Application.Features.Upload.Commands;
using SchoolManagement.Application.Features.Upload.Queries;
using SchoolManagement.Domain.Entities;

namespace SchoolManagement.Api.Controllers
{
    [Route("api/upload")]
    [ApiController]
    public class UploadController : ControllerBase
    {
        private readonly IMediator _mediator;
        public UploadController(IMediator mediator){
            _mediator = mediator;
        }

        [HttpGet]
        [Route("all")]

        public async Task<IActionResult> GetAllFiles(){
            return Ok(await _mediator.Send(new GetAllFilesQuery()));
        }

        [HttpPost]
        [Route("upload")]
        public async Task<IActionResult> UploadFile(UploadFileCommand command){
            return Ok(await _mediator.Send(command));
        } 

        [HttpGet]
        [Route("{fileName}")]
        public async Task<IActionResult> GetFile([FromRoute] string fileName){
            return await _mediator.Send(new GetFileQuery(fileName));
        }
    }
}
