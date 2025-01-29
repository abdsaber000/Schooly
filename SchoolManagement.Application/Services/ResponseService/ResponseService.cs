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
    private class Meta {
        public int TotalItems { get; set; }
        public int PageSize { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
       
    }
    private Meta GetMeta<T> (PagedResult<T> result){
        return new Meta(){
            TotalItems = result.TotalItems,
            PageSize = result.PageSize,
            CurrentPage = result.Page,
            TotalPages = result.TotalPages,
        };
    }
    public IActionResult CreateResponse<T>(PagedResult<T> result)
    {
        return new OkObjectResult(new {Data = result.Items , Meta = GetMeta(result)});
    }
}
