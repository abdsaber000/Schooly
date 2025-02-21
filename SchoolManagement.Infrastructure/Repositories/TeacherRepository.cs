using Microsoft.AspNetCore.Identity;
using SchoolManagement.Domain.Entities;
using SchoolManagement.Domain.Interfaces.IRepositories;
using SchoolManagement.Infrastructure.DbContext;

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
    public async Task AddTeacher(Teacher teacher , string password)
    {
        await _userManager.CreateAsync(teacher , password);
        await _userManager.AddToRoleAsync(teacher, Roles.Teacher);
    }
}