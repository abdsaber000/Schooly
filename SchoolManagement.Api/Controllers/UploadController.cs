using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Application.Features.Upload.Commands;
using SchoolManagement.Application.Features.Upload.Queries;
using SchoolManagement.Application.Services.ResponseService;
using SchoolManagement.Domain.Entities;

namespace SchoolManagement.Api.Controllers
{
    [Route("api/upload")]
    [ApiController]
    public class UploadController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IResponseService _responseService;
        public UploadController(IMediator mediator, IResponseService responseService){
            _mediator = mediator;
            _responseService = responseService;
        }

        [HttpGet]
        [Route("all")]

        public async Task<IActionResult> GetAllFiles(){
            return _responseService.CreateResponse(await _mediator.Send(new GetAllFilesQuery()));
        }

        [HttpPost]
        [Route("upload")]
        public async Task<IActionResult> UploadFile(UploadFileCommand command){
            return _responseService.CreateResponse(await _mediator.Send(command));
        } 

        [HttpGet]
        [Route("{fileName}")]
        public async Task<IActionResult> GetFile([FromRoute] string fileName){
            return await _mediator.Send(new GetFileQuery(fileName));
        }
    }
}
