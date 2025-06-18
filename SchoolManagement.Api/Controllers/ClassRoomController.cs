using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using SchoolManagement.Application.Features.ClassRoom.Command.AddClassRoom;
using SchoolManagement.Application.Features.ClassRoom.Command.AssignStudentToClassRoom;
using SchoolManagement.Application.Features.ClassRoom.Command.AssignTeacerToClassroom;
using SchoolManagement.Application.Features.ClassRoom.Command.DeleteClassRoom;
using SchoolManagement.Application.Features.ClassRoom.Command.RemoveStudentFromClassRoom;
using SchoolManagement.Application.Features.ClassRoom.Command.UpdateClassRoom;
using SchoolManagement.Application.Features.ClassRoom.Queries.GetAllClassRoom;
using SchoolManagement.Application.Features.ClassRoom.Queries.GetClassRoomById;
using SchoolManagement.Application.Services.ResponseService;
using SchoolManagement.Domain.Entities;

namespace SchoolManagement.Api.Controllers;

[EnableRateLimiting("ApiPolicy")]
[Authorize(AuthenticationSchemes = "Bearer")]
[ApiController]
[Route("api/classroom")]
public class ClassRoomController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IResponseService _responseService;
    
    public ClassRoomController(IMediator mediator, IResponseService responseService)
    {
        _mediator = mediator;
        _responseService = responseService;
    }
    
    [Authorize(Roles = Roles.Admin)]
    [HttpPost]
    public async Task<IActionResult> AddClassRoom([FromBody] AddClassRoomCommand command)
    {
        var respons = await _mediator.Send(command);
        return _responseService.CreateResponse(respons);
    }

    [Authorize(Roles = Roles.Admin)]
    [HttpPut]
    public async Task<IActionResult> UpdateClassRoom([FromBody] UpdateClassrRoomCommand command)
    {
        var respons = await _mediator.Send(command);
        return _responseService.CreateResponse(respons);
    }
    
    
    [HttpGet]
    public async Task<IActionResult> GetClassRoomById([FromQuery] Guid id)
    {
        var respons = await _mediator.Send(new GetClassRoomByIdQuery(id));
        return _responseService.CreateResponse(respons);
    }
    
   
    [HttpGet("all")]
    public async Task<IActionResult> GetAllClassRoom()
    {
        var respons = await _mediator.Send(new GetAllClassRoomQuery());
        return _responseService.CreateResponse(respons);
    }
    
    [Authorize(Roles = Roles.Admin)]
    [HttpDelete]
    public async Task<IActionResult> DeleteClassRoom([FromQuery] Guid id)
    {
        var respons = await _mediator.Send(new  DeleteClassRoomCommand(id));
        return _responseService.CreateResponse(respons);
    }
    
    [Authorize(Roles = Roles.Admin)]
    [HttpPost]
    [Route("assign-studnet")]
    public async Task<IActionResult> AssignStudentToClassRoom([FromQuery] AssignStudentToClassRoomCommand command)
    {
        return _responseService.CreateResponse(await _mediator.Send(command));
    }
    
    [Authorize(Roles = Roles.Admin)]
    [HttpDelete]
    [Route("unassign-student")]
    public async Task<IActionResult> RemoveStudentFromClassRoom([FromQuery] RemoveStudentFromClassRoomCommand command)
    {
        return _responseService.CreateResponse(await _mediator.Send(command));
    }
    
    [Authorize(Roles = Roles.Admin)]
    [HttpPut]
    [Route("assign-teacher")]
    public async Task<IActionResult> AssignTeacherToClassRoom([FromQuery] AssignTeacherToClassroomCommand command)
    {
        return _responseService.CreateResponse(await _mediator.Send(command));
    }
    
}