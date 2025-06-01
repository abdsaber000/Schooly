using SchoolManagement.Domain.Entities;
using SchoolManagement.Domain.Enums;

namespace SchoolManagement.Domain.Interfaces.IRepositories;

public interface ILessonRepository : IGenericRepository<Lesson>
{
    public Task Update(Lesson updatedLesson);
    
    Task<int> GetTotalCountAsyncByClassRoomId(Guid classRoomId , LessonStatus? status = LessonStatus.Upcoming , CancellationToken cancellationToken = default);
    Task<List<Lesson>> GetPagedAsyncByClassRoomId(int page, int pageSize , Guid classRoomId , LessonStatus? status = LessonStatus.Upcoming , CancellationToken cancellationToken = default);
    
    Task<bool> IsClassRoomAvailable(Guid classRoomId, DateOnly date, TimeOnly from, TimeOnly to);
    
    Task<List<Lesson>> GetLessonsByTeacherId(int page, int pageSize  , string teacherId , LessonStatus? status = LessonStatus.Upcoming);
    Task<int> GetTotalCountAsyncByTeacherId(string teacherId , LessonStatus? status = LessonStatus.Upcoming , CancellationToken cancellationToken = default);
    
    Task<List<Lesson>> GetLessonsByClassRoomsIds(List<Guid> classRoomIds, int page, int pageSize , LessonStatus? status = LessonStatus.Upcoming );
    Task<int> GetTotalCountLessonsByClassRoomsIds(List<Guid> classRoomIds , LessonStatus? status = LessonStatus.Upcoming );
    
    Task<bool> MarkExpiredLessonsAsCompleted();
    Task<bool> CancelLessonByIdAsync(Guid lessonId, CancellationToken cancellationToken = default);
}