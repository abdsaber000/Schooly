using SchoolManagement.Domain.Entities;
using SchoolManagement.Domain.HelperClass;

namespace SchoolManagement.Domain.Interfaces.IRepositories;

public interface IHomeWorkSubmissionRepositry : IGenericRepository<HomeWorkSubmission>
{
    public Task<int> GetTotalCountSubmittedStudentsByHomeWorkIdAsync(Guid homeWorkId);
    public Task<IEnumerable<StudentHomeWorkDto>> GetSubmittedStudentsByHomeWorkIdAsync(Guid homeWorkId, int page, int pageSize);
   Task<bool> isSubmittedByStudent(string studentId, Guid homeWorkId);
   Task<HomeWorkSubmission?> GetSubmissionByStudentIdAndHomeWorkId(string userId, Guid homeWorkId);
}