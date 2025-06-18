using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using SchoolManagement.Application.Features.Teacher.Command.AddTeacherCommand;
using SchoolManagement.Application.Features.Teacher.Queries.GetAllTeachers;
using SchoolManagement.Application.Services.ResponseService;
using SchoolManagement.Domain.Entities;

namespace SchoolManagement.Api.Controllers;

[Authorize(AuthenticationSchemes = "Bearer")]
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

    [Authorize(Roles = Roles.Admin)]
    [HttpPost]
    public async Task<IActionResult> AddTeacher([FromBody] AddTeacherCommand command)
    {
        var result = await _mediator.Send(command);
        return _responseService.CreateResponse(result);
    }
    
    [Authorize(Roles = Roles.Admin)]
    [HttpGet]
    [Route("all")]
    public async Task<IActionResult> GettAllTeachers()
    {
        var result = await _mediator.Send(new GetAllTeachersQuery());
        return _responseService.CreateResponse(result);
    }
}
