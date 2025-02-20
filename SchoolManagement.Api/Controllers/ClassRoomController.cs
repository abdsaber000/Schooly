using MediatR;
using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Application.Features.ClassRoom.Command.AddClassRoom;
using SchoolManagement.Application.Features.ClassRoom.Command.DeleteClassRoom;
using SchoolManagement.Application.Features.ClassRoom.Command.UpdateClassRoom;
using SchoolManagement.Application.Features.ClassRoom.Queries.GetAllClassRoom;
using SchoolManagement.Application.Features.ClassRoom.Queries.GetClassRoomById;
using SchoolManagement.Application.Services.ResponseService;

namespace SchoolManagement.Api.Controllers;

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
    
    [HttpPost]
    public async Task<IActionResult> AddClassRoom([FromBody] AddClassRoomCommand command)
    {
        var respons = await _mediator.Send(command);
        return _responseService.CreateResponse(respons);
    }

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

    [HttpDelete]
    public async Task<IActionResult> DeleteClassRoom([FromQuery] Guid id)
    {
        var respons = await _mediator.Send(new  DeleteClassRoomCommand(id));
        return _responseService.CreateResponse(respons);
    }
}