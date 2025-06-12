using SchoolManagement.Domain.Entities;

namespace SchoolManagement.Domain.Interfaces.IRepositories;

public interface IHomeWorkSubmissionRepositry : IGenericRepository<HomeWorkSubmission>
{
    public Task<int> GetTotalCountSubmittedStudentsByHomeWorkIdAsync(Guid homeWorkId);
    public Task<List<ApplicationUser>> GetSubmittedStudentsByHomeWorkIdAsync(Guid homeWorkId, int page, int pageSize);
}