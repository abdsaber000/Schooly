using Microsoft.EntityFrameworkCore;
using SchoolManagement.Domain.Entities;
using SchoolManagement.Domain.Interfaces.IRepositories;
using SchoolManagement.Infrastructure.DbContext;

namespace SchoolManagement.Infrastructure.Repositories;

public class LessonRepository : GenericRepository<Lesson>, ILessonRepository
{
    private readonly AppDbContext _appDbContext;
    public LessonRepository(AppDbContext appDbContext): base(appDbContext)
    {
        _appDbContext = appDbContext;
    }
    private (DateOnly TodayEgypt, TimeOnly CurrentTimeEgypt) GetCurrentEgyptTime()
    {
        var egyptTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Africa/Cairo");
        var nowEgypt = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, egyptTimeZone);

        var todayEgypt = DateOnly.FromDateTime(nowEgypt);
        var currentTimeEgypt = TimeOnly.FromDateTime(nowEgypt);

        return (todayEgypt, currentTimeEgypt);
    }
    public async Task Update(Lesson updatedLesson)
    {
        var lesson = await _appDbContext.Lessons.FirstOrDefaultAsync(l => l.Id == updatedLesson.Id);
        
        lesson.TeacherId = updatedLesson.TeacherId;
        lesson.ClassRoomId = updatedLesson.ClassRoomId;
        lesson.Title = updatedLesson.Title;
        lesson.LessonType = updatedLesson.LessonType;
        lesson.Date = updatedLesson.Date;
        lesson.From = updatedLesson.From;
        lesson.To = updatedLesson.To;
        
        await _appDbContext.SaveChangesAsync();
    }
    public async Task<int> GetTotalCountAsyncByClassRoomsId(Guid classRoomId , CancellationToken cancellationToken = default)
    {
        var (todayEgypt, currentTimeEgypt) = GetCurrentEgyptTime();

        return await _appDbContext.Lessons
            .Where(lesson => lesson.ClassRoom.Id == classRoomId && 
                             (lesson.Date > todayEgypt || 
                              (lesson.Date == todayEgypt && lesson.To > currentTimeEgypt)))
            .CountAsync(cancellationToken);
    }
    public async Task<List<Lesson>> GetPagedAsyncByClassRoomsId(int page, int pageSize , Guid classRoomId, CancellationToken cancellationToken = default)
    {
        var (todayEgypt, currentTimeEgypt) = GetCurrentEgyptTime();

        return await _appDbContext.Lessons
            .Include(lesson => lesson.ClassRoom)
            .Where(lesson => lesson.ClassRoom.Id == classRoomId && 
                             (lesson.Date > todayEgypt || 
                              (lesson.Date == todayEgypt && lesson.To > currentTimeEgypt)))
            .OrderBy(lesson => lesson.Date)
            .ThenBy(lesson => lesson.From)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);
    }

    public async Task<bool> IsClassRoomAvailable(Guid classRoomId, DateOnly date, TimeOnly from, TimeOnly to)
    {
        var overlappingLessons = await _appDbContext.Lessons
            .Where(l => l.ClassRoomId == classRoomId && l.Date == date)
            .Where(l => (l.From < to && l.To > from))
            .AnyAsync();
        
        return !overlappingLessons;
    }

    public async Task<List<Lesson>> GetUpcommingLessonsByTeacherId(int page, int pageSize, string teacherId)
    {
        var (todayEgypt, currentTimeEgypt) = GetCurrentEgyptTime();

        return await _appDbContext.Lessons
            .Include(lesson => lesson.ClassRoom)
            .Where(lesson => lesson.TeacherId == teacherId && 
                             (lesson.Date > todayEgypt || 
                              (lesson.Date == todayEgypt && lesson.To > currentTimeEgypt)))
            .OrderBy(lesson => lesson.Date)
            .ThenBy(lesson => lesson.From)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }

    public async Task<int> GetTotalCountAsyncByTeacherId(string teacherId, CancellationToken cancellationToken = default)
    {
        var (todayEgypt, currentTimeEgypt) = GetCurrentEgyptTime();

        return await _appDbContext.Lessons
            .Where(lesson => lesson.TeacherId == teacherId && 
                             (lesson.Date > todayEgypt || 
                              (lesson.Date == todayEgypt && lesson.To > currentTimeEgypt)))
            .CountAsync(cancellationToken);
    }

    public async Task<List<Lesson>> GetUpcomingLessonsByClassRoomIds(List<Guid> classRoomIds, int page, int pageSize)
    {
        var (todayEgypt, currentTimeEgypt) = GetCurrentEgyptTime();

        return await _appDbContext.Lessons
            .Include(lesson => lesson.ClassRoom)
            .Where(lesson => classRoomIds.Contains(lesson.ClassRoomId) &&
                             (lesson.Date > todayEgypt ||
                              (lesson.Date == todayEgypt && lesson.To > currentTimeEgypt)))
            .OrderBy(lesson => lesson.Date)
            .ThenBy(lesson => lesson.From)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }

    public async Task<int> GetTotalCountUpcomingLessonsByClassRoomIds(List<Guid> classRoomIds)
    {
        var (todayEgypt, currentTimeEgypt) = GetCurrentEgyptTime();

        return await _appDbContext.Lessons
            .Where(lesson => classRoomIds.Contains(lesson.ClassRoomId) &&
                             (lesson.Date > todayEgypt ||
                              (lesson.Date == todayEgypt && lesson.To > currentTimeEgypt)))
            .CountAsync();
    }
}