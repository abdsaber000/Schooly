using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using SchoolManagement.Application.Features.Student.Dtos;
using SchoolManagement.Application.Shared;

namespace SchoolManagement.Application.Features.Student.Queries.GetStudentDetail
{
    public class GetStudentQuery : IRequest<Result<StudentDto>>
    {
        public string Id { get; set; }

        public GetStudentQuery(string id)
        {
            Id = id;
        }
    }
}
