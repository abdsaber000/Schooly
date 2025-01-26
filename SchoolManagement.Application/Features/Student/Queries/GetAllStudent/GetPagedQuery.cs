using MediatR;
using SchoolManagement.Application.Features.Authentication.Dtos;
using SchoolManagement.Application.Features.Pagination;
using SchoolManagement.Domain.Entities;

public class GetPagedQuery : IRequest<PagedResult<StudentDto>>
{
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}