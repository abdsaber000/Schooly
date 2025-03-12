using Microsoft.EntityFrameworkCore;
using SchoolManagement.Domain.Interfaces.IRepositories;
using SchoolManagement.Infrastructure.DbContext;
using SchoolManagement.Domain.Entities;

namespace SchoolManagement.Infrastructure.Repositories;

public class StudentClassRoomRepository : GenericRepository<StudentClassRoom , Guid>, IStudentClassRoomRepository
{
    private readonly AppDbContext _appDbContext;
    public StudentClassRoomRepository(AppDbContext appDbContext) : base(appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public async Task<StudentClassRoom?> GetStudentClassRoomAsync(string studentId, Guid classRoomId)
    {
        return await _appDbContext.StudentClassRooms.FirstOrDefaultAsync(sc =>
            sc.StudentId == studentId && sc.ClassRoomId == classRoomId);
    }
}