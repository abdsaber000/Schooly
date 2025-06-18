using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using Microsoft.AspNetCore.RateLimiting;
using SchoolManagement.Application.Features.HomeWork.Commands.AddHomeWork;
using SchoolManagement.Application.Features.HomeWork.Commands.DeleteHomeWork;
using SchoolManagement.Application.Features.HomeWork.Commands.SubmitHomeWork;
using SchoolManagement.Application.Features.HomeWork.Query.GetAllHomeWork;
using SchoolManagement.Application.Features.HomeWork.Query.GetAllStudentSubmitedHomeWork;
using SchoolManagement.Application.Features.HomeWork.Query.GetHomeWork;
using SchoolManagement.Application.Services.ResponseService;
using SchoolManagement.Domain.Entities;

namespace SchoolManagement.Api.Controllers;

[EnableRateLimiting("ApiPolicy")]
[Authorize(AuthenticationSchemes = "Bearer")]
[ApiController]
[Route("api/homework")]
public class HomeWorkController : ControllerBase
{
   private readonly IMediator _mediator;
   private readonly IResponseService _responseService;
   public HomeWorkController(IMediator mediator, IResponseService responseService)
   {
      _mediator = mediator;
      _responseService = responseService;
   }
   
   [HttpPost]
   [Authorize(Roles = Roles.Teacher)]
   public async Task<IActionResult> AddHomeWork(AddHomeWorkCommands commands)
   {
      return _responseService.CreateResponse(await _mediator.Send(commands));
   }

   [HttpGet("all")]
   public async Task<IActionResult> GetAllHomeWork([FromQuery] GetِِِِAllHomeWorkQuery Query)
   {
      return _responseService.CreateResponse(await _mediator.Send(Query));
   }
   
   [HttpDelete]
   [Authorize(Roles = Roles.Teacher)]
   public async Task<IActionResult> DeleteHomeWork(Guid homeWorkId)
   {
      return _responseService.CreateResponse(await _mediator.Send(new DeleteHomeWorkCommand(homeWorkId)));
   }
   
   [Authorize(Roles = Roles.Student)]
   [HttpPost("{homeWorkId}/submit")]
   public async Task<IActionResult> SubmitHomework(Guid homeWorkId, [FromBody] SubmitHomeWorkCommand command)
   {
      var result = await _mediator.Send(command);
      return _responseService.CreateResponse(result);
   }
   
   [Authorize(Roles = Roles.Teacher)]
   [HttpGet("{homeWorkId}/students")]
   public async Task<IActionResult> GetStudentsWhoSubmittedHomework(Guid homeWorkId)
   {
      var result = await _mediator.Send(new GetStudentsByHomeworkSubmissionQuery { HomeWorkId = homeWorkId });
      return _responseService.CreateResponse(result);
   }

}