using SchoolManagement.Domain.Entities;

namespace SchoolManagement.Domain.Interfaces.IRepositories;

public interface ILessonRepository : IGenericRepository<Lesson>
{
    public Task Update(Lesson updatedLesson);
    Task<int> GetTotalCountAsync(Guid classRoomId , CancellationToken cancellationToken = default);
    Task<List<Lesson>> GetPagedAsync(int page, int pageSize , Guid classRoomId, CancellationToken cancellationToken = default);
    Task<bool> IsClassRoomAvailable(Guid classRoomId, DateOnly date, TimeOnly from, TimeOnly to);
}