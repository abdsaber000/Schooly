using SchoolManagement.Domain.Entities;

namespace SchoolManagement.Domain.Interfaces.IRepositories;

public interface ILessonRepository : IGenericRepository<Lesson>
{
    public Task Update(Lesson updatedLesson);
    Task<int> GetTotalCountAsyncByClassRoomsId(Guid classRoomId , CancellationToken cancellationToken = default);
    Task<List<Lesson>> GetPagedAsyncByClassRoomsId(int page, int pageSize , Guid classRoomId, CancellationToken cancellationToken = default);
    Task<bool> IsClassRoomAvailable(Guid classRoomId, DateOnly date, TimeOnly from, TimeOnly to);
    Task<List<Lesson>> GetUpcommingLessonsByTeacherId(int page, int pageSize  , string teacherId);
    Task<int> GetTotalCountAsyncByTeacherId(string teacherId , CancellationToken cancellationToken = default);
    Task<List<Lesson>> GetUpcomingLessonsByClassRoomIds(List<Guid> classRoomIds, int page, int pageSize);
    Task<int> GetTotalCountUpcomingLessonsByClassRoomIds(List<Guid> classRoomIds);
    Task<bool> DeleteExpiredLessons();
}