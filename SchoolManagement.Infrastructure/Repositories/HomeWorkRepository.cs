using Microsoft.EntityFrameworkCore;
using SchoolManagement.Domain.Entities;
using SchoolManagement.Domain.Interfaces.IRepositories;
using SchoolManagement.Infrastructure.DbContext;

namespace SchoolManagement.Infrastructure.Repositories;

public class HomeWorkRepository : GenericRepository<HomeWork> , IHomeWorkRepository
{
    public readonly AppDbContext _appDbContext;
    public HomeWorkRepository(AppDbContext appDbContext) : base(appDbContext)
    {
        _appDbContext = appDbContext;
    }
    public async Task<List<HomeWork>> GetAllClassRoomHomeWork(Guid classRoomId)
    {
        return await _appDbContext.HomeWorks
            .Where(c => c.classRoomId == classRoomId).ToListAsync();
    }

    public async Task<HomeWork> GetHomeWorkByFileUrl(string fileUrl)
    {
        return await _appDbContext.HomeWorks.FirstOrDefaultAsync(h => h.FileUrl == fileUrl);
    }
}