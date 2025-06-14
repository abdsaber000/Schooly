using Microsoft.EntityFrameworkCore;
using SchoolManagement.Domain.Entities;
using SchoolManagement.Domain.HelperClass;
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

    public async Task<IEnumerable<StudentHomeWorkDto>> GetSubmittedStudentsByHomeWorkIdAsync(Guid homeWorkId, int page, int pageSize)
    {
        return await _appDbContext.HomeWorkSubmissions
            .Where(hs => hs.HomeWorkId == homeWorkId)
            .Select(hs => new StudentHomeWorkDto
            {
                studentId = hs.StudentId,
                studentName = hs.Student.Name,
                fileName = hs.FileName,
                fileUrl = hs.FileUrl,
                submittedDate = hs.SubmittedAt,
                Dateline = hs.HomeWork.Deadline
            })
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }

    public async Task<bool> isSubmittedByStudent(string studentId, Guid homeWorkId)
    {
        return await _appDbContext.HomeWorkSubmissions
            .AnyAsync(hs => hs.StudentId == studentId && hs.HomeWorkId == homeWorkId);
    }
    
}