using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using SchoolManagement.Application.Features.Pagination;
using SchoolManagement.Application.Features.Student.Queries.GetAllStudent;
using SchoolManagement.Application.Services.ResponseService;
using SchoolManagement.Domain.Entities;

namespace SchoolManagement.Api.Controllers;

// [Authorize]
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
        _mediator = mediator  ?? throw new ArgumentNullException(nameof(mediator));;
        _responseService = responseService;
    }
    [HttpGet]
    [Route("all")]
    public async Task<IActionResult> GetPagedData([FromQuery] GetStudentsPagedQuery query)
    {
        var result = await _mediator.Send(query);
        return _responseService.CreateResponse(result);
    }
}