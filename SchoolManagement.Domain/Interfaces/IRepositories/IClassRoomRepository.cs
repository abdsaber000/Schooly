using SchoolManagement.Domain.Entities;

namespace SchoolManagement.Domain.Interfaces.IRepositories;

public interface IClassRoomRepository
{
    public Task AddClassRoom(ClassRooms classRooms);
    public Task<ClassRooms?> GetClassRoomById(Guid id);
    public Task UpdateClassRoom(ClassRooms updatedClassRooms);
    public Task DeleteClassRoom(Guid id);
    public Task<List<ClassRooms>> GetAllClassRoom();
    
    public Task SaveChange();
}