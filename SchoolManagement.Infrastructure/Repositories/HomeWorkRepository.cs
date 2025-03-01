using Microsoft.EntityFrameworkCore;
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
    
    public async Task AddHomeWork(HomeWork? homeWork)
    {
        await _context.HomeWorks.AddAsync(homeWork);
        await _context.SaveChangesAsync();
    }

    public async Task<List<HomeWork>> GetAllClassRoomHomeWork(Guid classRoomId)
    {
        return await _context.HomeWorks
            .Where(c => c.classRoomId == classRoomId).ToListAsync();
    }

    public async Task<HomeWork?> GetHomeWork(Guid homeWorkId)
    {
        return await _context.HomeWorks.FindAsync(homeWorkId);
    }

    public async Task DeleteHomeWork(Guid homeWorkId)
    {
        var homeWork = await _context.HomeWorks.FindAsync(homeWorkId);
        if (homeWorkId != null)
        {
             _context.HomeWorks.Remove(homeWork);
            await _context.SaveChangesAsync();
        }
    }
}