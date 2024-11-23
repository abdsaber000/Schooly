using MediatR;
using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Application.Features.Student.Commands.AddStudent;

namespace SchoolManagement.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StudentsController : ControllerBase
{
    private readonly IMediator _mediator;

    public StudentsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    [Route("Add")]
    public async Task<IActionResult> Add([FromBody] AddStudentCommand request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var result = await _mediator.Send(request);
        return Ok(result);
    }
}