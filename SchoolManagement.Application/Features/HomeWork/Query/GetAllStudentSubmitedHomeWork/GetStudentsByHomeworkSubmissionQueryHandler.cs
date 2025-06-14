using MediatR;
using SchoolManagement.Application.Features.HomeWork.Dtos;
using SchoolManagement.Application.Features.Pagination;
using SchoolManagement.Application.Features.Student.Dtos;
using SchoolManagement.Domain.HelperClass;
using SchoolManagement.Domain.Interfaces.IRepositories;

namespace SchoolManagement.Application.Features.HomeWork.Query.GetAllStudentSubmitedHomeWork;

public class GetStudentsByHomeworkSubmissionQueryHandler : IRequestHandler<GetStudentsByHomeworkSubmissionQuery , PagedResult<StudentHomeWorkDto>>
{
    private readonly IHomeWorkSubmissionRepositry _homeWorkSubmissionRepositry;

    public GetStudentsByHomeworkSubmissionQueryHandler(IHomeWorkSubmissionRepositry homeWorkSubmissionRepositry)
    {
        _homeWorkSubmissionRepositry = homeWorkSubmissionRepositry;
    }

    public async Task<PagedResult<StudentHomeWorkDto>> Handle(GetStudentsByHomeworkSubmissionQuery request, CancellationToken cancellationToken)
    {
        var count = await _homeWorkSubmissionRepositry
            .GetTotalCountSubmittedStudentsByHomeWorkIdAsync(request.HomeWorkId);
        
        var studentsHomeWorksDto = await _homeWorkSubmissionRepositry
            .GetSubmittedStudentsByHomeWorkIdAsync(request.HomeWorkId, request.pageNumber, request.pageSize);
        
        var pagedResult = new PagedResult<StudentHomeWorkDto>
        {
            PageSize = request.pageSize,
            Page = request.pageNumber,
            Items = studentsHomeWorksDto,
            TotalItems = count
        };
        return pagedResult;
    }
}