using MediatR;
using SchoolManagement.Application.Features.Pagination;
using SchoolManagement.Application.Features.Student.Dtos;

namespace SchoolManagement.Application.Features.Student.Queries.GetAllStudent;

public class GetStudentsPagedQuery : IRequest<PagedResult<StudentDto>>
{
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}