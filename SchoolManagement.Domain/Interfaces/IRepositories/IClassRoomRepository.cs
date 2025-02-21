using SchoolManagement.Domain.Entities;

namespace SchoolManagement.Domain.Interfaces.IRepositories;

public interface IClassRoomRepository
{
    public Task AddClassRoom(ClassRoom classRooms);
    public Task<ClassRoom?> GetClassRoomById(Guid id);
    public Task UpdateClassRoom(ClassRoom updatedClassRooms);
    public Task DeleteClassRoom(Guid id);
    public Task<List<ClassRoom>> GetAllClassRoom();
    
    public Task SaveChange();
}