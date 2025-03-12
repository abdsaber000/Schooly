using SchoolManagement.Domain.Entities;

namespace SchoolManagement.Domain.Interfaces.IRepositories;

public interface IClassRoomRepository: IGenericRepository<ClassRoom , Guid>
{
    public Task<List<ClassRoom>> GetAllClassRoom();
    
}