using SchoolManagement.Domain.Entities;

namespace SchoolManagement.Domain.Interfaces.IRepositories;

public interface ITeacherRepository
{
    Task AddTeacher(Teacher teacher , string password);
}