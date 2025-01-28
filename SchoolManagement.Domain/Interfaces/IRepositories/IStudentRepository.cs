using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SchoolManagement.Domain.Entities;
using SchoolManagement.Domain.Interfaces.IRepositories;

namespace SchoolManagement.Domain.Interfaces.Repositories
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
