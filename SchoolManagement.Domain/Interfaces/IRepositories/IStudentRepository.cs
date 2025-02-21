using SchoolManagement.Domain.Entities;

namespace SchoolManagement.Domain.Interfaces.IRepositories
{
    public interface IStudentRepository
    {
        Task AddStudent(Student student , string password);
        Task<Student?> GetStudentByEmail(string email); 
        Task<int> GetTotalCountAsync(CancellationToken cancellationToken = default);
        Task<List<Student>> GetPagedStudentsAsync(int page, int pageSize, CancellationToken cancellationToken = default);
        Task SaveChanges();
    }
}
