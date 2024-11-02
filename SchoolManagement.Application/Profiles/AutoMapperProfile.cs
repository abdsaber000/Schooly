using AutoMapper;
using SchoolManagement.Application.Features.Student.Queries.GetStudentDetail;
using SchoolManagement.Domain.Entities;

namespace SchoolManagement.Application.Profiles
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Student, StudentVM>().ReverseMap();
        }
    }
}
