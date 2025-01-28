using System;
using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Application.Features.Pagination;
using SchoolManagement.Application.Shared;

namespace SchoolManagement.Application.Services.ResponseService;

public interface IResponseService
{
    IActionResult CreateResponse<T>(Result<T> result);
    IActionResult CreateResponse<T>(PagedResult<T> result);
}
