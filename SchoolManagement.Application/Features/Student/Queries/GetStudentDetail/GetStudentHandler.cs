using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Localization;
using SchoolManagement.Application.Features.Student.Dtos;
using SchoolManagement.Application.Shared;
using SchoolManagement.Domain.Interfaces.IRepositories;

namespace SchoolManagement.Application.Features.Student.Queries.GetStudentDetail
{

    public class GetStudentHandler : IRequestHandler<GetStudentQuery, Result<StudentDto>>
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IStringLocalizer<GetStudentHandler> _localizer;
        public GetStudentHandler(IStudentRepository studentRepository, IStringLocalizer<GetStudentHandler> localizer)
        {
            _studentRepository = studentRepository;
            _localizer = localizer;
        }

        public async Task<Result<StudentDto>> Handle(GetStudentQuery request, CancellationToken cancellationToken)
        {
            var student = await _studentRepository.GetStudentByIdAsync(request.Id , cancellationToken);
            if (student is null)
            {
                return Result<StudentDto>.Failure(_localizer["Student not found"]);
            }
            return Result<StudentDto>.Success(student.ToStudentDto());
        }
    }
}
