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
    private DateTime GetCurrentEgyptTime()
    {
        var egyptTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Africa/Cairo");
        var nowEgypt = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, egyptTimeZone);
        return nowEgypt;
    }

    public async Task<int> GetTotalCountAsyncByClassRoomId(Guid classRoomId, CancellationToken cancellationToken = default)
    {
        var currentDateTimeEgypt = GetCurrentEgyptTime();
       return await _appDbContext.HomeWorks.Where(hw => hw.classRoomId == classRoomId && hw.ToDate > currentDateTimeEgypt)
           .CountAsync(cancellationToken);
    }

    public async Task<List<HomeWork>> GetAllActiveHomeWorkByClassRoomId(int page, int pageSize, Guid classRoomId)
    {
        var currentDateTimeEgypt = GetCurrentEgyptTime();
        return await _appDbContext.HomeWorks
            .Where(hw => hw.classRoomId == classRoomId && hw.ToDate > currentDateTimeEgypt)
            .Include(hw => hw.Lesson)
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
        var currentDateTimeEgypt = GetCurrentEgyptTime();

        return await _appDbContext.HomeWorks
            .CountAsync(hw => hw.teacherId == teacherId && hw.ToDate > currentDateTimeEgypt, cancellationToken);
    }

    public async Task<List<HomeWork>> GetActiveHomeWorksByTeacherId(int page, int pageSize, string teacherId)
    {
        var currentDateTimeEgypt = GetCurrentEgyptTime();
        return await _appDbContext.HomeWorks
            .Where(hw => hw.teacherId == teacherId && hw.ToDate > currentDateTimeEgypt)
            .Include(hw => hw.Lesson)
            .OrderBy(hw => hw.Lesson.Date)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }

    public async Task<int> GetTotalCountAsyncByClassRoomsId(List<Guid> classRoomIds)
    {
        var currentDateTimeEgypt = GetCurrentEgyptTime();

        return await _appDbContext.HomeWorks
            .Where(hw => classRoomIds.Contains(hw.classRoomId) 
                         && hw.ToDate > currentDateTimeEgypt)
            .CountAsync();
    }

    public async Task<List<HomeWork>> GetActiveHomeWorksByClassRoomIds(List<Guid> classRoomIds, int page, int pageSize)
    {
        var currentDateTimeEgypt = GetCurrentEgyptTime();

        return await _appDbContext.HomeWorks
            .Where(hw => classRoomIds.Contains(hw.classRoomId) && hw.ToDate > currentDateTimeEgypt)
            .Include(hw => hw.Lesson)
            .OrderBy(hw => hw.Lesson.Date)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }
}