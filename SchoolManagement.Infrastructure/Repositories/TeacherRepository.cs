using Microsoft.AspNetCore.Identity;
using SchoolManagement.Domain.Entities;
using SchoolManagement.Domain.Interfaces.IRepositories;
using SchoolManagement.Infrastructure.DbContext;
using Microsoft.EntityFrameworkCore;

namespace SchoolManagement.Infrastructure.Repositories;

public class TeacherRepository : GenericRepository<Teacher>, ITeacherRepository
{
    private readonly AppDbContext _appDbContext;
    private readonly UserManager<ApplicationUser> _userManager;

    public TeacherRepository(AppDbContext appDbContext, UserManager<ApplicationUser> userManager) : base(appDbContext)
    {
        _appDbContext = appDbContext;
        _userManager = userManager;
    }
    public async Task AddTeacher(Teacher teacher, string password)
    {
        await _userManager.CreateAsync(teacher, password);
        await _userManager.AddToRoleAsync(teacher, Roles.Teacher);
    }
    public async Task<Teacher?> GetTeacherByEmail(string email)
    {
        var Teacher = await _appDbContext.Users
            .OfType<Teacher>()
            .FirstOrDefaultAsync(s => s.Email == email);
        return Teacher;
    }

    public async Task<Teacher?> GetByIdAsync(string id)
    {
        var teacher = await _appDbContext.Users
            .OfType<Teacher>()
            .FirstOrDefaultAsync(s => s.Id == id);
        return teacher;
    }

    public async Task RemoveTeacher(Teacher teacher)
    {
        var comments = await _appDbContext.Comments
            .Where(c => c.AuthorId == teacher.Id)
            .ToListAsync();
        _appDbContext.Comments.RemoveRange(comments);

        var posts = await _appDbContext.Posts
            .Where(c => c.AuthorId == teacher.Id)
            .ToListAsync();

        comments = await _appDbContext.Comments
            .Include(c => c.Post)
            .Where(c => posts.Contains(c.Post))
            .ToListAsync();
        _appDbContext.Comments.RemoveRange(comments);

        _appDbContext.Posts.RemoveRange(posts);

        var homeworks = await _appDbContext.HomeWorks
            .Where(c => c.teacherId == teacher.Id)
            .ToListAsync();

        var homeworkSubmissions = await _appDbContext.HomeWorkSubmissions
            .Include(c => c.HomeWork)
            .Where(c => homeworks.Contains(c.HomeWork))
            .ToListAsync();
        _appDbContext.HomeWorkSubmissions.RemoveRange(homeworkSubmissions);
        _appDbContext.HomeWorks.RemoveRange(homeworks);

        var lessons = await _appDbContext.Lessons
            .Where(c => c.TeacherId == teacher.Id)
            .ToListAsync();
        _appDbContext.Lessons.RemoveRange(lessons);

        var classrooms = await _appDbContext.ClassRooms
            .Where(c => c.TeacherId == teacher.Id)
            .ToListAsync();
        _appDbContext.ClassRooms.RemoveRange(classrooms);

        _appDbContext.Users.Remove(teacher);

        await _appDbContext.SaveChangesAsync();    
        
    }
}