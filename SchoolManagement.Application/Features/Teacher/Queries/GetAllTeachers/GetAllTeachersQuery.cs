using MediatR;
using SchoolManagement.Application.Features.Pagination;
using SchoolManagement.Application.Features.Teacher.Dtos;
using SchoolManagement.Application.Shared;
using SchoolManagement.Domain.Interfaces.IRepositories;

namespace SchoolManagement.Application.Features.Teacher.Queries.GetAllTeachers;

public class GetAllTeachersQuery : IRequest<PagedResult<TeacherDto>>
{
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}

public class GetAllTeachersQueryHandler : IRequestHandler<GetAllTeachersQuery, PagedResult<TeacherDto>>
{
    private readonly ITeacherRepository _teacherRepository;

    public GetAllTeachersQueryHandler(ITeacherRepository teacherRepository)
    {
        _teacherRepository = teacherRepository;
    }

    public async Task<PagedResult<TeacherDto>> Handle(GetAllTeachersQuery request, CancellationToken cancellationToken)
    {
        var teachers = await _teacherRepository.GetPagedAsync(request.Page , request.PageSize , cancellationToken);
        var teacherDtos = teachers.Select(t => t.ToTeacherDto()).ToList();
        
       var result = new PagedResult<TeacherDto>
        {
            TotalItems = await _teacherRepository.GetTotalCountAsync(cancellationToken),
            Page = request.Page,
            PageSize = request.PageSize,
            Items = teacherDtos
        };

        return result;
    }
}