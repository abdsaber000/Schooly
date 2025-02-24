using SchoolManagement.Domain.Entities;
using SchoolManagement.Domain.Interfaces.IRepositories;
using SchoolManagement.Infrastructure.DbContext;

namespace SchoolManagement.Infrastructure.Repositories;

public class HomeworkeRepository : IHomeworkeRepository
{
    public readonly AppDbContext _context;

    public HomeworkeRepository(AppDbContext context)
    {
        _context = context;
    }
    
    public async Task AddHomeworke(Homeworke homeworke)
    {
        await _context.Homeworkes.AddAsync(homeworke);
        await _context.SaveChangesAsync();
    }
}