using SchoolManagement.Domain.Entities;

namespace SchoolManagement.Domain.Interfaces.IRepositories;

public interface IHomeWorkRepository
{
   public Task AddHomeWork(HomeWork homeWork);
}