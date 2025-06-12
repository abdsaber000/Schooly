using MediatR;
using SchoolManagement.Application.Features.Pagination;
using SchoolManagement.Application.Features.Student.Dtos;

namespace SchoolManagement.Application.Features.HomeWork.Query.GetAllStudentSubmitedHomeWork;

public class GetStudentsByHomeworkSubmissionQuery : IRequest<PagedResult<StudentDto>>
{
    public Guid HomeWorkId { get; set; }
    public int pageNumber { get; set; } = 1;
    public int pageSize { get; set; } = 10;
}