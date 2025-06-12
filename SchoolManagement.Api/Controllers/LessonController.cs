using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using SchoolManagement.Application.Features.Lesson.Command.CancelLesson;
using SchoolManagement.Application.Features.Lesson.Command.CreateLesson;
using SchoolManagement.Application.Features.Lesson.Command.DeleteLesson;
using SchoolManagement.Application.Features.Lesson.Command.UpdateLeeson;
using SchoolManagement.Application.Features.Lesson.Queries.GetAllLessons;
using SchoolManagement.Application.Features.Lesson.Queries.GetLesson;
using SchoolManagement.Application.Features.Lesson.Queries.JoinLesson;
using SchoolManagement.Application.Services.ResponseService;
using SchoolManagement.Domain.Entities;

namespace SchoolManagement.Api.Controllers;

[EnableRateLimiting("ApiPolicy")]
[Authorize(AuthenticationSchemes = "Bearer")]
[ApiController]
[Route("api/lesson")]
public class LessonController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IResponseService _responseService;
    public LessonController(IMediator mediator
                    , IResponseService responseService)
    {
        _mediator = mediator;
        _responseService = responseService;
    }
 
    [Authorize(Roles = Roles.Teacher)]
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateLessonCommand command)
    {
        return _responseService.CreateResponse(await _mediator.Send(command));
    }
    
    [Authorize(Roles = $"{Roles.Teacher} , {Roles.Student}")]
    [HttpPost("join")]
    public async Task<IActionResult> Join(JoinLessonCommand command)
    {
        return _responseService.CreateResponse(await _mediator.Send(command));
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAllLessons([FromQuery] GetLessonsPagedQuery query)
    {
        return _responseService.CreateResponse(await _mediator.Send(query));
    }
    
    [Authorize(Roles = $"{Roles.Teacher} , {Roles.Student}")]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetLessonById(Guid id)
    {
        return _responseService.CreateResponse(await _mediator.Send(new GetLessonQuery(id)));
    }

    [Authorize(Roles = Roles.Teacher)]
    [HttpPut]
    public async Task<IActionResult> UpdateLesson(UpdateLessonCommand command)
    {
        return _responseService.CreateResponse(await _mediator.Send(command));
    }

    [Authorize(Roles = Roles.Teacher)]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteLesson(Guid id)
    {
        return _responseService.CreateResponse(await _mediator.Send(new DeleteLessonCommand(id)));
    }
    
    [HttpPost("cancel")]
    [Authorize(Roles = Roles.Teacher)]
    public async Task<IActionResult> CancelLesson([FromQuery] Guid lessonId)
    {
        return _responseService.CreateResponse(await _mediator.Send(new CancelLessonCommand(lessonId)));
    }
}