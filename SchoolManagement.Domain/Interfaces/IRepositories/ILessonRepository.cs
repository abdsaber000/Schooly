using SchoolManagement.Domain.Entities;

namespace SchoolManagement.Domain.Interfaces.IRepositories;

public interface ILessonRepository
{
    public Task CreateLesson(Lesson lesson);
    public Task<Lesson?> GetLessonById(string id);
    public Task Update(Lesson updatedLesson);
    public Task Delete(string id);
    Task SaveChanges();
}