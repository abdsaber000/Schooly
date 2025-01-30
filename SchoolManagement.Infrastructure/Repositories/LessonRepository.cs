using Microsoft.EntityFrameworkCore;
using SchoolManagement.Domain.Entities;
using SchoolManagement.Domain.Interfaces.IRepositories;
using SchoolManagement.Infrastructure.DbContext;

namespace SchoolManagement.Infrastructure.Repositories;

public class LessonRepository : ILessonRepository
{
    private readonly AppDbContext _appDbContext;

    public LessonRepository(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }
    
    public async Task CreateLesson(Lesson lesson)
    {
        await _appDbContext.Lessons.AddAsync(lesson);
    }

    public async Task<Lesson?> GetLessonById(string id)
    {
        var lesson = await _appDbContext.Lessons.FirstOrDefaultAsync(l => l.Id == id);
        return lesson;
    }

    public async Task Update(Lesson updatedLesson)
    {
        var lesson = await _appDbContext.Lessons.FirstOrDefaultAsync(l => l.Id == updatedLesson.Id);
        
        lesson.TeacherId = updatedLesson.TeacherId;
        lesson.Subject = updatedLesson.Subject;
        lesson.Grade = updatedLesson.Grade;
        lesson.Title = updatedLesson.Title;
        lesson.LessonType = updatedLesson.LessonType;
        lesson.Date = updatedLesson.Date;
        lesson.From = updatedLesson.From;
        lesson.To = updatedLesson.To;
    }

    public async Task Delete(string id)
    {
        var lesson = await _appDbContext.Lessons.FirstOrDefaultAsync(l => l.Id == id); 
        _appDbContext.Lessons.Remove(lesson);
    }

   public async Task<int> GetTotalCountAsync(CancellationToken cancellationToken = default)
   {
       var egyptTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Africa/Cairo");
   
       var nowEgypt = TimeZoneInfo.ConvertTime(DateTime.Now, egyptTimeZone);
   
       var todayEgypt = DateOnly.FromDateTime(nowEgypt);
       var currentTimeEgypt = TimeOnly.FromDateTime(nowEgypt);
   
       return await _appDbContext.Lessons
           .Where(lesson => lesson.Date > todayEgypt || 
                            (lesson.Date == todayEgypt && lesson.To > currentTimeEgypt))
           .CountAsync(cancellationToken);
   }

    public async Task<List<Lesson>> GetPagedAsync(int page, int pageSize, CancellationToken cancellationToken = default)
    {
        var now = DateTime.Now;

        return await _appDbContext.Lessons
            .Where(lesson => lesson.Date > DateOnly.FromDateTime(now) || 
                             (lesson.Date == DateOnly.FromDateTime(now) && lesson.To > TimeOnly.FromDateTime(now)))
            .OrderBy(lesson => lesson.Date)
            .ThenBy(lesson => lesson.From)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);
    }

    public async Task SaveChanges()
    {
        await _appDbContext.SaveChangesAsync();
    }
}