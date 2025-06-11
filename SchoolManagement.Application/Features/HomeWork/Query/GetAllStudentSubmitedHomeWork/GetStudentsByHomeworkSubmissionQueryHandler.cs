using MediatR;
using SchoolManagement.Application.Features.Pagination;
using SchoolManagement.Application.Features.Student.Dtos;
using SchoolManagement.Domain.Interfaces.IRepositories;

namespace SchoolManagement.Application.Features.HomeWork.Query.GetAllStudentSubmitedHomeWork;

public class GetStudentsByHomeworkSubmissionQueryHandler : IRequestHandler<GetStudentsByHomeworkSubmissionQuery , PagedResult<StudentDto>>
{
    private readonly IHomeWorkSubmissionRepositry _homeWorkSubmissionRepositry;

    public GetStudentsByHomeworkSubmissionQueryHandler(IHomeWorkSubmissionRepositry homeWorkSubmissionRepositry)
    {
        _homeWorkSubmissionRepositry = homeWorkSubmissionRepositry;
    }

    public async Task<PagedResult<StudentDto>> Handle(GetStudentsByHomeworkSubmissionQuery request, CancellationToken cancellationToken)
    {
        var count = await _homeWorkSubmissionRepositry
            .GetTotalCountSubmittedStudentsByHomeWorkIdAsync(request.HomeWorkId);
        
        var students = await _homeWorkSubmissionRepositry
            .GetSubmittedStudentsByHomeWorkIdAsync(request.HomeWorkId, request.pageNumber, request.pageSize);

        var studentsDto = students.Select(s => s.ToStudentDto());
        var pagedResult = new PagedResult<StudentDto>
        {
            PageSize = request.pageSize,
            Page = request.pageNumber,
            Items = studentsDto,
            TotalItems = count
        };
        return pagedResult;
    }
}