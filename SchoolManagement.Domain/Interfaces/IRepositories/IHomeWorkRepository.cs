using SchoolManagement.Domain.Entities;

namespace SchoolManagement.Domain.Interfaces.IRepositories;

public interface IHomeWorkRepository : IGenericRepository<HomeWork>
{
   Task<int> GetTotalCountAsyncByClassRoomId(Guid classRoomId , CancellationToken cancellationToken = default);
   public Task<List<HomeWork>> GetAllHomeWorkByClassRoomId(int page, int pageSize,Guid classRoomId);
   public Task<HomeWork> GetHomeWorkByFileUrl(string fileUrl);
   
   Task<int> GetTotalCountAsyncByTeacherId(string teacherId , CancellationToken cancellationToken = default);
   Task<List<HomeWork>> GetHomeWorksByTeacherId(int page, int pageSize  , string teacherId);
   Task<int> GetTotalCountAsyncByClassRoomsId(List<Guid> classRoomIds);
   Task<List<HomeWork>> GetHomeWorksByClassRoomIds(List<Guid> classRoomIds, int page, int pageSize);
}