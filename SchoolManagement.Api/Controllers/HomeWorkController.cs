using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Application.Features.HomeWorke.Commands.AddHomeworke;
using MediatR;
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
      
}