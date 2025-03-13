using SchoolManagement.Domain.Entities;

namespace SchoolManagement.Domain.Interfaces.IRepositories;

public interface IStudentClassRoomRepository : IGenericRepository<StudentClassRoom>
{
    public Task<StudentClassRoom?> GetStudentClassRoomAsync(string studentId, Guid classRoomId);
}