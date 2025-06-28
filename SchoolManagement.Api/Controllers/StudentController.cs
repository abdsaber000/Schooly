using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using SchoolManagement.Application.Features.Student.Commands.DeleteStudent;
using SchoolManagement.Application.Features.Student.Queries.GetAllStudent;
using SchoolManagement.Application.Features.Student.Queries.GetStudentDetail;
using SchoolManagement.Application.Services.ResponseService;
using SchoolManagement.Domain.Entities;

namespace SchoolManagement.Api.Controllers;

[Authorize(AuthenticationSchemes = "Bearer")]
[EnableRateLimiting("ApiPolicy")]
[ApiController]
[Route("api/student")]
public class StudentController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IResponseService _responseService;

    public StudentController(IMediator mediator
                        , IResponseService responseService)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator)); ;
        _responseService = responseService;
    }

    [Authorize(Roles = Roles.Teacher + "," + Roles.Admin)]
    [HttpGet]
    [Route("all")]
    public async Task<IActionResult> GetPagedData([FromQuery] GetStudentsPagedQuery query)
    {
        var result = await _mediator.Send(query);
        return _responseService.CreateResponse(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetStudentById([FromQuery] string id)
    {
        var result = await _mediator.Send(new GetStudentQuery(id));
        return _responseService.CreateResponse(result);
    }

    [HttpPut]
    [Authorize(Roles = Roles.Admin)]
    public async Task<IActionResult> UpdateStudent([FromBody] UpdateStudentCommandRequest request)
    {
        var result = await _mediator.Send(request);
        return _responseService.CreateResponse(result);
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = Roles.Admin)]
    public async Task<IActionResult> DeleteStudent(string id)
    {
        var result = await _mediator.Send(new DeleteStudentCommandRequest(id));
        return _responseService.CreateResponse(result);
    }
}