using Microsoft.EntityFrameworkCore;
using SchoolManagement.Domain.Entities;
using SchoolManagement.Domain.Interfaces.IRepositories;
using SchoolManagement.Infrastructure.DbContext;

namespace SchoolManagement.Infrastructure.Repositories;

public class ClassRoomRepository : GenericRepository<ClassRoom>, IClassRoomRepository
{
    private readonly AppDbContext _appDbContext;

    public ClassRoomRepository(AppDbContext appDbContext): base(appDbContext)
    {
        _appDbContext = appDbContext;
    }
    public async Task<List<ClassRoom>> GetAllClassRoom()
    {
        return await _appDbContext.ClassRooms.ToListAsync();
    }

    public async Task<bool> IsExistAsync(Guid id)
    {
        return await _appDbContext.ClassRooms.CountAsync(x => x.Id == id) > 0;
    }
    
    public async Task<List<ClassRoom>> GetAllClassRoomsByTeacherId(string teacherId)
    {
        return await _appDbContext.ClassRooms.Where(c => c.TeacherId == teacherId)
            .ToListAsync();
    }
}