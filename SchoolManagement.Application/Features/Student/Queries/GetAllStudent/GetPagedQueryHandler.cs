using MediatR;
using Microsoft.EntityFrameworkCore;
using SchoolManagement.Application.Features.Student.Dtos;
using SchoolManagement.Infrastructure.DbContext;
using SchoolManagement.Domain.Entities;
using SchoolManagement.Domain.Interfaces.Repositories;

namespace SchoolManagement.Application.Features.Pagination;

public class GetPagedQueryHandler : IRequestHandler<GetPagedQuery, PagedResult<StudentDto>> 
{
    private readonly AppDbContext _context;
    private readonly IStudentRepository _studentRepository;
    public GetPagedQueryHandler(AppDbContext context, IStudentRepository studentRepository)
    {
        _context = context;
        _studentRepository = studentRepository;
    }
    
    public async Task<PagedResult<StudentDto>>Handle(GetPagedQuery request, CancellationToken cancellationToken)
    {
        var totalCount = await _studentRepository.GetTotalCountAsync(cancellationToken);
        
        var students = await _studentRepository
            .GetPagedStudentsAsync(request.Page, request.PageSize, cancellationToken);

        var studentsDto = new List<StudentDto>();
        foreach (var student in students)
        {
            var studentDto = student.ToStudentDto();
            studentsDto.Add(studentDto);
        }
        return new PagedResult<StudentDto>
        {
            TotalItems = totalCount,
            Page = request.Page,
            PageSize = request.PageSize,
            Items = studentsDto
        };
    }
}