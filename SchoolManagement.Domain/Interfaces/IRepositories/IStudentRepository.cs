using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SchoolManagement.Domain.Entities;

namespace SchoolManagement.Domain.Interfaces.Repositories
{
    public interface IStudentRepository : IGenericRepository
    {
        Task AddStudent(Student student , string password);
        Task<Student?> GetStudentByEmail(string email); 
        Task<int> GetTotalCountAsync(CancellationToken cancellationToken = default);
        Task<List<Student>> GetPagedStudentsAsync(int page, int pageSize, CancellationToken cancellationToken = default);
        Task SaveChanges();
    }
}
