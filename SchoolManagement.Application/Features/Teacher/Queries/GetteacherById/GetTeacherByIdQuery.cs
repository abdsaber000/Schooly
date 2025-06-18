using MediatR;
using Microsoft.Extensions.Localization;
using SchoolManagement.Application.Features.Teacher.Dtos;
using SchoolManagement.Application.Shared;
using SchoolManagement.Domain.Interfaces.IRepositories;

namespace SchoolManagement.Application.Features.Teacher.Queries.GetteacherById;

public class GetTeacherByIdQuery: IRequest<Result<TeacherDto>>
{
    public string Id { get; }
    public GetTeacherByIdQuery(string id)
    {
        Id = id;
    }
}
public class GetteacherByIdQueryHandler : IRequestHandler<GetTeacherByIdQuery, Result<TeacherDto>>
{
    private readonly ITeacherRepository _teacherRepository;
    private readonly IStringLocalizer<GetTeacherByIdQuery> _localizer;
    
    public GetteacherByIdQueryHandler(ITeacherRepository teacherRepository, IStringLocalizer<GetTeacherByIdQuery> localizer)
    {
        _teacherRepository = teacherRepository;
        _localizer = localizer;
    }

    public async Task<Result<TeacherDto>> Handle(GetTeacherByIdQuery request, CancellationToken cancellationToken)
    {
        var teacher = await _teacherRepository.GetByIdAsync(request.Id);
        if (teacher == null)
        {
            return Result<TeacherDto>.Failure(_localizer[ "Teacher not found" ]);
        }

        return Result<TeacherDto>.Success(teacher.ToTeacherDto());
    }
}