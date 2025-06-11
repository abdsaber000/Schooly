using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using SchoolManagement.Application.Features.HomeWork.Commands.AddHomeWork;
using SchoolManagement.Application.Features.HomeWork.Commands.DeleteHomeWork;
using SchoolManagement.Application.Features.HomeWork.Query.GetAllClassRoomHomeWork;
using SchoolManagement.Application.Features.HomeWork.Query.GetHomeWork;
using SchoolManagement.Application.Services.ResponseService;
using SchoolManagement.Domain.Entities;

namespace SchoolManagement.Api.Controllers;

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

   [HttpGet("active")]
   public async Task<IActionResult> GetAllHomeWork([FromQuery] GetActiveHomeWorkQuery Query)
   {
      return _responseService.CreateResponse(await _mediator.Send(Query));
   }
   
   [HttpDelete]
   [Authorize(Roles = Roles.Teacher)]
   public async Task<IActionResult> DeleteHomeWork(string fileName)
   {
      return _responseService.CreateResponse(await _mediator.Send(new DeleteHomeWorkCommand(fileName)));
   }
}