using SchoolManagement.Domain.Entities;

namespace SchoolManagement.Domain.Interfaces.IRepositories;

public interface IStudentClassRoomRepository : IGenericRepository<StudentClassRoom , Guid>
{
    public Task<StudentClassRoom?> GetStudentClassRoomAsync(string studentId, Guid classRoomId);
}