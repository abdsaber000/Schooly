using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using SchoolManagement.Application.Features.Teacher.Command.AddTeacherCommand;
using SchoolManagement.Application.Features.Teacher.Queries.GetAllTeachers;
using SchoolManagement.Application.Services.ResponseService;

namespace SchoolManagement.Api.Controllers;

[EnableRateLimiting("ApiPolicy")]
[ApiController]
[Route("api/teacher")]
public class TeacherController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IResponseService _responseService;
    public TeacherController(IMediator mediator, IResponseService responseService)
    {
        _mediator = mediator;
        _responseService = responseService;
    }

    [HttpPost]
    public async Task<IActionResult> AddTeacher([FromBody] AddTeacherCommand command)
    {
        var result = await _mediator.Send(command);
        return _responseService.CreateResponse(result);
    }
    
    [HttpGet]
    [Route("all")]
    public async Task<IActionResult> GettAllTeachers()
    {
        var result = await _mediator.Send(new GetAllTeachersQuery());
        return _responseService.CreateResponse(result);
    }
}
