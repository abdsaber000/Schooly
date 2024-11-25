using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SchoolManagement.Domain.Entities;
using SchoolManagement.Domain.Interfaces.Repositories;
using SchoolManagement.Infrastructure.DbContext;


namespace SchoolManagement.Infrastructure.Repositories
{
    public class StudentRepository : IStudentRepository
    {
        
        private readonly AppDbContext _appDbContext;
        private readonly UserManager<ApplicationUser> _userManager;

        public StudentRepository(AppDbContext appDbContext, UserManager<ApplicationUser> userManager)
        {
            _appDbContext = appDbContext;
            _userManager = userManager;
        }
        public async Task AddStudent(Student Student)
        {
            var createdUser = await _userManager.CreateAsync(Student , "Aa#123456");
            await _userManager.AddToRoleAsync(Student, Roles.Student);
        }

        public async Task<int> GetTotalCountAsync(CancellationToken cancellationToken = default)
        {
            return await _appDbContext.Users.OfType<Student>().CountAsync(cancellationToken);
        }
        public async Task<List<Student>> GetPagedStudentsAsync(int page, int pageSize, CancellationToken cancellationToken = default)
        {
            return await _appDbContext.Users
                .OfType<Student>()
                .Include(student => student.Parent) // to load parent ad the defult lazy loading
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(cancellationToken);
        }
        public async Task SaveChanges()
        {
            await _appDbContext.SaveChangesAsync();
        }
    }
}
