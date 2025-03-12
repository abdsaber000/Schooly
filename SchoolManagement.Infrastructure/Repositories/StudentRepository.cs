using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SchoolManagement.Domain.Entities;
using SchoolManagement.Domain.Interfaces.IRepositories;
using SchoolManagement.Infrastructure.DbContext;


namespace SchoolManagement.Infrastructure.Repositories
{
    public class StudentRepository : GenericRepository<Student , string>, IStudentRepository 
    {
        
        private readonly AppDbContext _appDbContext;
        private readonly UserManager<ApplicationUser> _userManager;


        public StudentRepository(AppDbContext appDbContext, UserManager<ApplicationUser> userManager) : base(appDbContext)
        {
            _appDbContext = appDbContext;
            _userManager = userManager;
        }

        public async Task AddStudent(Student student , string password)
        {
            var createdUser = await _userManager.CreateAsync(student , password);
            await _userManager.AddToRoleAsync(student, Roles.Student);
        }
        public async Task<Student?> GetStudentByEmail(string email)
        {
            var student = await _appDbContext.Users
                .OfType<Student>()
                .FirstOrDefaultAsync(s => s.Email == email);
            return student;
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
