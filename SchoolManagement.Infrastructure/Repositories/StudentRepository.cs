using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SchoolManagement.Domain.Entities;
using SchoolManagement.Domain.Interfaces.IRepositories;
using SchoolManagement.Infrastructure.DbContext;


namespace SchoolManagement.Infrastructure.Repositories
{
    public class StudentRepository : GenericRepository<Student>, IStudentRepository 
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
            await _userManager.CreateAsync(student , password);
            await _userManager.AddToRoleAsync(student, Roles.Student);
        }

        public async Task<Student?> GetStudentByIdAsync(string id, CancellationToken cancellationToken = default)
        {
            return await _appDbContext.Users
                .OfType<Student>()
                .Include(s => s.Parent) 
                .FirstOrDefaultAsync(s => s.Id == id, cancellationToken);
        }

        public async Task<Student?> GetStudentByEmail(string email)
        {
            var student = await _appDbContext.Users
                .OfType<Student>()
                .FirstOrDefaultAsync(s => s.Email == email);
            return student;
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

        public async Task RemoveStudent(Student student)
        {
            var comments = await _appDbContext.Comments
                .Where(c => c.AuthorId == student.Id)
                .ToListAsync();
            _appDbContext.Comments.RemoveRange(comments);

            var posts = await _appDbContext.Posts
                .Where(c => c.AuthorId == student.Id)
                .ToListAsync();

            comments = await _appDbContext.Comments
                .Include(c => c.Post)
                .Where(c => posts.Contains(c.Post))
                .ToListAsync();
            _appDbContext.Comments.RemoveRange(comments);

            _appDbContext.Posts.RemoveRange(posts);

            var homeworkSubmissions = await _appDbContext.HomeWorkSubmissions
                .Where(c => c.StudentId == student.Id)
                .ToListAsync();
            _appDbContext.HomeWorkSubmissions.RemoveRange(homeworkSubmissions);

            _appDbContext.Users.Remove(student);

            await _appDbContext.SaveChangesAsync();
        }
        public async Task SaveChanges()
        {
            await _appDbContext.SaveChangesAsync();
        }
    }
}
