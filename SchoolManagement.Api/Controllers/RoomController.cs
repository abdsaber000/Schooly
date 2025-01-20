using MediatR;
using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Application.Features.Rooms.Commands.CreateRoom;

namespace SchoolManagement.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RoomController : ControllerBase
{
    private readonly IMediator _mediator;

    public RoomController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("create")]
    public async Task<IActionResult> CreateRoom([FromBody] CreateRoomCommand command)
    {
        var result = await _mediator.Send(command);
        return Ok(result);
    }
}
