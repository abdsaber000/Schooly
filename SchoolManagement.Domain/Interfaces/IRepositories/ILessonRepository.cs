using SchoolManagement.Domain.Entities;

namespace SchoolManagement.Domain.Interfaces.IRepositories;

public interface ILessonRepository
{
    public Task CreateLesson(Lesson lesson);
    public Task<Lesson?> GetLessonById(string id);
    public Task Update(Lesson updatedLesson);
    public Task Delete(string id);
    Task<int> GetTotalCountAsync(Guid classRoomId , CancellationToken cancellationToken = default);
    Task<List<Lesson>> GetPagedAsync(int page, int pageSize , Guid classRoomId, CancellationToken cancellationToken = default);
    Task<bool> IsClassRoomAvailable(Guid classRoomId, DateOnly date, TimeOnly from, TimeOnly to);
}