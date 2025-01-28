using System;
using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Application.Features.Pagination;
using SchoolManagement.Application.Shared;

namespace SchoolManagement.Application.Services.ResponseService;

public class ResponseService : IResponseService
{
    public IActionResult CreateResponse<T>(Result<T> result)
    {
        if(result.IsSuccess){
            if(result.Message != string.Empty){
                return new OkObjectResult(new { message = result.Message });
            }else if(result.Token != string.Empty && result.Data != null){
                return new OkObjectResult(new { token = result.Token , data = result.Data});
            }else if(result.Token != string.Empty){
                return new OkObjectResult(new { token = result.Token });
            }else if(result.Data != null){
                return new OkObjectResult(new { data = result.Data });
            }
        }else{
            return new BadRequestObjectResult(new { message = result.Message });
        }
        return new BadRequestObjectResult("unhandled error");
    }

    public IActionResult CreateResponse<T>(PagedResult<T> result)
    {
        return CreateResponse(Result<PagedResult<T>>.Success(result));
    }
}
