using Microsoft.AspNetCore.Identity;
using SchoolManagement.Domain.Entities;
using SchoolManagement.Domain.Interfaces.IRepositories;
using SchoolManagement.Infrastructure.DbContext;
using Microsoft.EntityFrameworkCore;

namespace SchoolManagement.Infrastructure.Repositories;

public class TeacherRepository : ITeacherRepository
{
    private readonly AppDbContext _appDbContext;
    private readonly UserManager<ApplicationUser> _userManager;

    public TeacherRepository(AppDbContext appDbContext, UserManager<ApplicationUser> userManager)
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
}