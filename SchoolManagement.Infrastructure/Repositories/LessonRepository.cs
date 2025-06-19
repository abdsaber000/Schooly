using Microsoft.EntityFrameworkCore;
using SchoolManagement.Domain.Entities;
using SchoolManagement.Domain.Enums;
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

    public async Task<Lesson?> GetLessonByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _appDbContext.Lessons
            .Include(l => l.ClassRoom)
            .Include(l => l.Teacher)
            .FirstOrDefaultAsync(l => l.Id == id, cancellationToken);
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
    public async Task<int> GetTotalCountAsyncByClassRoomId(Guid classRoomId , LessonStatus? status = null , CancellationToken cancellationToken = default)
    {
        var query = _appDbContext.Lessons
            .Where(l => l.ClassRoom.Id == classRoomId);

        if (status.HasValue)
            query = query.Where(l => l.LessonStatus == status);

        return await query.CountAsync(cancellationToken);
    }
    public async Task<List<Lesson>> GetPagedAsyncByClassRoomId(int page, int pageSize , Guid classRoomId, LessonStatus? status = null , CancellationToken cancellationToken = default)
    {
        var query = _appDbContext.Lessons
            .Include(l => l.ClassRoom)
            .Include(lesson => lesson.Teacher)
            .Where(l => l.ClassRoom.Id == classRoomId);

        if (status.HasValue)
            query = query.Where(l => l.LessonStatus == status);

        return await query
            .OrderBy(l => l.Date).ThenBy(l => l.From)
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

    public async Task<List<Lesson>> GetLessonsByTeacherId(int page, int pageSize, string teacherId, LessonStatus? status)
    {
        var teacherClassRoomIds = await _appDbContext.ClassRooms
            .Where(c => c.TeacherId == teacherId)
            .Select(c => c.Id)
            .ToListAsync();
        
        var query = _appDbContext.Lessons
            .Include(lesson => lesson.ClassRoom)
            .Include(lesson => lesson.Teacher)
            .Where(lesson => teacherClassRoomIds.Contains(lesson.ClassRoomId));

        if (status.HasValue) {
            query = query.Where(lesson => lesson.LessonStatus == status);
        }

        return await query
            .OrderBy(lesson => lesson.Date)
            .ThenBy(lesson => lesson.From)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }

    public async Task<int> GetTotalCountAsyncByTeacherId(string teacherId, LessonStatus? status,
        CancellationToken cancellationToken = default)
    {
        var teacherClassRoomIds = await _appDbContext.ClassRooms
            .Where(c => c.TeacherId == teacherId)
            .Select(c => c.Id)
            .ToListAsync();
        
        var query = _appDbContext.Lessons
            .Include(lesson => lesson.ClassRoom)
            .Include(lesson => lesson.Teacher)
            .Where(lesson => teacherClassRoomIds.Contains(lesson.ClassRoomId));

        if (status.HasValue){
            query = query.Where(lesson => lesson.LessonStatus == status);
        }

        return await query.CountAsync(cancellationToken);
    }
    
    public async Task<List<Lesson>> GetLessonsByClassRoomsIds(List<Guid> classRoomIds, int page, int pageSize ,LessonStatus? status = null )
    {
        var query = _appDbContext.Lessons
            .Include(lesson => lesson.ClassRoom)
            .Include(lesson => lesson.Teacher)
            .Where(lesson => classRoomIds.Contains(lesson.ClassRoomId));

        if (status.HasValue) {
            query = query.Where(lesson => lesson.LessonStatus == status);
        }

        return await query
            .OrderBy(lesson => lesson.Date)
            .ThenBy(lesson => lesson.From)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }

    public async Task<int> GetTotalCountLessonsByClassRoomsIds(List<Guid> classRoomIds , LessonStatus? status = null )
    {
        var query = _appDbContext.Lessons
            .Where(lesson => classRoomIds.Contains(lesson.ClassRoomId));
        
        if (status.HasValue) {
            query = query.Where(lesson => lesson.LessonStatus == status);
        }

        return await query.CountAsync();
    }

    public async Task<bool> MarkExpiredLessonsAsCompleted()
    {
        var (todayEgypt, currentTimeEgypt) = GetCurrentEgyptTime();

        var expiredLessons = await _appDbContext.Lessons
            .Where(lesson =>
                (lesson.Date < todayEgypt ||
                (lesson.Date == todayEgypt && lesson.To <= currentTimeEgypt)) 
                && lesson.LessonStatus != LessonStatus.Completed && lesson.LessonStatus != LessonStatus.Canceled)
            .ToListAsync();

        if (!expiredLessons.Any())
            return false;

        foreach (var lesson in expiredLessons)
        {
            lesson.LessonStatus = LessonStatus.Completed;
        }

        await _appDbContext.SaveChangesAsync();
        return true;
    }

    public async Task<bool> CancelLessonByIdAsync(Guid lessonId, CancellationToken cancellationToken = default)
    {
       var lesson = await _appDbContext.Lessons.FindAsync( lessonId, cancellationToken);
           if (lesson == null)
               return false;
       
           lesson.LessonStatus = LessonStatus.Canceled;
           await _appDbContext.SaveChangesAsync(cancellationToken);
           return true; 
    }
}