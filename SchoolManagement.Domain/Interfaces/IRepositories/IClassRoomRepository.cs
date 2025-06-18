using SchoolManagement.Domain.Entities;

namespace SchoolManagement.Domain.Interfaces.IRepositories;

public interface IClassRoomRepository: IGenericRepository<ClassRoom>
{
    public Task<List<ClassRoom>> GetAllClassroomsPagedAsync(int page, int pageSize, CancellationToken cancellationToken = default);
    public Task<bool> IsExistAsync(Guid id);
    
    public Task<List<ClassRoom>> GetAllClassRoomsByTeacherId(string teacherId);
}