using Microsoft.EntityFrameworkCore;
using SchoolManagement.Domain.Entities;
using SchoolManagement.Domain.Interfaces.IRepositories;
using SchoolManagement.Infrastructure.DbContext;

namespace SchoolManagement.Infrastructure.Repositories;

public class HomeWorkSubmissionRepositry : GenericRepository<HomeWorkSubmission> , IHomeWorkSubmissionRepositry
{
    public readonly AppDbContext _appDbContext;
    public HomeWorkSubmissionRepositry(AppDbContext appDbContext) : base(appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public async Task<int> GetTotalCountSubmittedStudentsByHomeWorkIdAsync(Guid homeWorkId)
    {
        return await _appDbContext.HomeWorkSubmissions
            .Where(hs => hs.HomeWorkId == homeWorkId)
            .Select(hs => hs.StudentId)
            .Distinct()
            .CountAsync();
    }

    public async Task<List<ApplicationUser>> GetSubmittedStudentsByHomeWorkIdAsync(Guid homeWorkId, int page, int pageSize)
    {
        return await _appDbContext.HomeWorkSubmissions
            .Where(hs => hs.HomeWorkId == homeWorkId)
            .Select(hs => hs.Student)
            .Distinct()
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }
}