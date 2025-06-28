using SchoolManagement.Domain.Entities;

namespace SchoolManagement.Domain.Interfaces.IRepositories;

public interface ITeacherRepository : IGenericRepository<Teacher>
{
    Task AddTeacher(Teacher teacher, string password);
    Task<Teacher?> GetTeacherByEmail(string email);
    Task<Teacher?> GetByIdAsync(string id);
    Task RemoveTeacher(Teacher teacher);
}