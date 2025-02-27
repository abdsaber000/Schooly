using SchoolManagement.Domain.Entities;

namespace SchoolManagement.Domain.Interfaces.IRepositories;

public interface IHomeWorkRepository
{
   public Task AddHomeWork(HomeWork? homeWork);
   public Task<List<HomeWork?>> GetAllClassRoomHomeWork(Guid classRoomId);
   public Task<HomeWork?> GetHomeWork(Guid homeWorkId);
}