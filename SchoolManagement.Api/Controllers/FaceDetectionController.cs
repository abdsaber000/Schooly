using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SchoolManagement.Api.Controllers
{
    [Route("api/face-detection")]
    [ApiController]
    public class FaceDetectionController : ControllerBase
    {
        private readonly IMediator _mediator;
        public FaceDetectionController(IMediator mediator){
            _mediator = mediator;
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register(){
            throw new NotImplementedException();
        }

        [HttpPost]
        [Route("verify")]
        public async Task<IActionResult> Verify(){
            throw new NotImplementedException();
        }
    }
}
