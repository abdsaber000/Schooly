using MediatR;
using SchoolManagement.Application.Features.Pagination;
using SchoolManagement.Application.Features.Student.Dtos;
using SchoolManagement.Domain.Interfaces.IRepositories;


namespace SchoolManagement.Application.Features.Student.Queries.GetAllStudent;
public class GetStudentsPagedQueryHandler : IRequestHandler<GetStudentsPagedQuery, PagedResult<StudentDto>> 
{
    private readonly IStudentRepository _studentRepository;

    public GetStudentsPagedQueryHandler(IStudentRepository studentRepository)
    {
        _studentRepository = studentRepository;
    }

    public async Task<PagedResult<StudentDto>>Handle(GetStudentsPagedQuery request, CancellationToken cancellationToken)
    {
        var totalCount = await _studentRepository.GetTotalCountAsync(cancellationToken);
        
        var students = await _studentRepository
            .GetPagedStudentsAsync(request.Page, request.PageSize, cancellationToken);

        var studentsDto = students
            .Select(s => s.ToStudentDto())
            .ToList();
       
        return new PagedResult<StudentDto>
        {
            TotalItems = totalCount,
            Page = request.Page,
            PageSize = request.PageSize,
            Items = studentsDto
        };
    }
}