using MediatR;
using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Application.Features.Pagination;
using SchoolManagement.Application.Features.Student.Commands.AddStudent;
using SchoolManagement.Domain.Entities;

namespace SchoolManagement.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class studentController : ControllerBase
{
    private readonly IMediator _mediator;

    public studentController(IMediator mediator)
    {
        _mediator = mediator  ?? throw new ArgumentNullException(nameof(mediator));;
    }

    [HttpPost]
    public async Task<IActionResult> Add([FromBody] AddStudentCommand request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var result = await _mediator.Send(request);
        return Ok(result);
    }
    
    [HttpGet]
    public async Task<IActionResult> GetPagedData([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
    {
        var query = new GetPagedQuery() { Page = page, PageSize = pageSize };
        var result = await _mediator.Send(query);
        return Ok(result);
    }
}