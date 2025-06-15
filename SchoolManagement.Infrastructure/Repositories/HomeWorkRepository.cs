using Microsoft.EntityFrameworkCore;
using SchoolManagement.Domain.Entities;
using SchoolManagement.Domain.Interfaces.IRepositories;
using SchoolManagement.Infrastructure.DbContext;

namespace SchoolManagement.Infrastructure.Repositories;

public class HomeWorkRepository : GenericRepository<HomeWork> , IHomeWorkRepository
{
    public readonly AppDbContext _appDbContext;
    public HomeWorkRepository(AppDbContext appDbContext) : base(appDbContext)
    {
        _appDbContext = appDbContext;
    }
    
    public async Task<int> GetTotalCountAsyncByClassRoomId(Guid classRoomId, CancellationToken cancellationToken = default)
    {
       return await _appDbContext.HomeWorks.Where(hw => hw.classRoomId == classRoomId)
           .CountAsync(cancellationToken);
    }

    public async Task<List<HomeWork>> GetAllHomeWorkByClassRoomId(int page, int pageSize, Guid classRoomId)
    {
        return await _appDbContext.HomeWorks
            .Where(hw => hw.classRoomId == classRoomId)
            .Include(hw => hw.Lesson.ClassRoom)
            .Include(hw => hw.Lesson.Teacher)
            .OrderBy(hw => hw.Lesson.Date)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }

    public async Task<HomeWork> GetHomeWorkByFileUrl(string fileUrl)
    {
        return await _appDbContext.HomeWorks.FirstOrDefaultAsync(h => h.FileUrl == fileUrl);
    }

    public async Task<int> GetTotalCountAsyncByTeacherId(string teacherId, CancellationToken cancellationToken = default)
    {
        return await _appDbContext.HomeWorks
            .CountAsync(hw => hw.teacherId == teacherId , cancellationToken);
    }

    public async Task<List<HomeWork>> GetHomeWorksByTeacherId(int page, int pageSize, string teacherId)
    {
        return await _appDbContext.HomeWorks
            .Where(hw => hw.teacherId == teacherId)
            .Include(hw => hw.Lesson.ClassRoom)
            .Include(hw => hw.Lesson.Teacher)
            .OrderBy(hw => hw.Lesson.Date)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }

    public async Task<int> GetTotalCountAsyncByClassRoomsId(List<Guid> classRoomIds)
    {
        return await _appDbContext.HomeWorks
            .Where(hw => classRoomIds.Contains(hw.classRoomId))
            .CountAsync();
    }

    public async Task<List<HomeWork>> GetHomeWorksByClassRoomIds(List<Guid> classRoomIds, int page, int pageSize)
    {
        return await _appDbContext.HomeWorks
            .Where(hw => classRoomIds.Contains(hw.classRoomId))
            .Include(hw => hw.Lesson.ClassRoom)
            .Include(hw => hw.Lesson.Teacher)
            .OrderBy(hw => hw.Lesson.Date)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }
}