using SchoolManagement.Domain.Entities;

namespace SchoolManagement.Domain.Interfaces.IRepositories
{
    public interface IStudentRepository : IGenericRepository<Student>
    {
        Task AddStudent(Student student , string password);
        Task<Student?> GetStudentByIdAsync(string id, CancellationToken cancellationToken = default);
        Task<Student?> GetStudentByEmail(string email); 
        Task<List<Student>> GetPagedStudentsAsync(int page, int pageSize, CancellationToken cancellationToken = default);
    }
}
