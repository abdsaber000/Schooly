using MediatR;
using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Application.Features.Lesson.Command.CreateLesson;
using SchoolManagement.Application.Features.Lesson.Command.DeleteLesson;
using SchoolManagement.Application.Features.Lesson.Command.UpdateLeeson;
using SchoolManagement.Application.Features.Lesson.Queries.GetAllLessons;
using SchoolManagement.Application.Features.Lesson.Queries.GetLesson;
using SchoolManagement.Application.Features.Lesson.Queries.JoinLesson;

namespace SchoolManagement.Api.Controllers;

[ApiController]
[Route("api/lesson")]
public class LessonController : ControllerBase
{
    private readonly IMediator _mediator;

    public LessonController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateLessonCommand command)
    {
        return Ok(await _mediator.Send(command));
    }

    [HttpPost("join/{id}")]
    public async Task<IActionResult> Join(string id)
    {
        return Ok(await _mediator.Send(new JoinLessonCommand(id)));
    }

    [HttpGet("all")]
    public async Task<IActionResult> GetAllLessons([FromQuery] GetLessonsPagedQuery query)
    {
        return Ok(await _mediator.Send(query));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetLessonById(string id)
    {
        return Ok(await _mediator.Send(new GetLessonQuery(id)));
    }

    [HttpPut]
    public async Task<IActionResult> UpdateLesson(UpdateLessonCommand command)
    {
        return Ok(await _mediator.Send(command));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteLesson(string id)
    {
        return Ok(await _mediator.Send(new DeleteLessonCommand(id)));
    }
}