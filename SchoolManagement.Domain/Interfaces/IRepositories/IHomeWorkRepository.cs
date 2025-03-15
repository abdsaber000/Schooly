using SchoolManagement.Domain.Entities;

namespace SchoolManagement.Domain.Interfaces.IRepositories;

public interface IHomeWorkRepository : IGenericRepository<HomeWork>
{
   public Task<List<HomeWork>> GetAllClassRoomHomeWork(Guid classRoomId);
   public Task<HomeWork> GetHomeWorkByFileUrl(string fileUrl);
}