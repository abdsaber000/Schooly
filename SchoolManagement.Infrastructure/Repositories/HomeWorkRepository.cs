using SchoolManagement.Domain.Entities;
using SchoolManagement.Domain.Interfaces.IRepositories;
using SchoolManagement.Infrastructure.DbContext;

namespace SchoolManagement.Infrastructure.Repositories;

public class HomeWorkRepository : IHomeWorkRepository
{
    public readonly AppDbContext _context;

    public HomeWorkRepository(AppDbContext context)
    {
        _context = context;
    }
    
    public async Task AddHomeWork(HomeWork homeWork)
    {
        await _context.HomeWorks.AddAsync(homeWork);
        await _context.SaveChangesAsync();
    }
}