using MediatR;
using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Application.Features.Pagination;
using SchoolManagement.Application.Features.Student.Queries.GetAllStudent;
using SchoolManagement.Domain.Entities;

namespace SchoolManagement.Api.Controllers;

[ApiController]
[Route("api/student")]
public class StudentController : ControllerBase
{
    private readonly IMediator _mediator;

    public StudentController(IMediator mediator)
    {
        _mediator = mediator  ?? throw new ArgumentNullException(nameof(mediator));;
    }
    [HttpGet]
    [Route("all")]
    public async Task<IActionResult> GetPagedData([FromQuery] GetStudentsPagedQuery query)
    {
        var result = await _mediator.Send(query);
        return Ok(result);
    }
}