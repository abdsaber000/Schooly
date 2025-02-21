using Microsoft.EntityFrameworkCore;
using SchoolManagement.Domain.Entities;
using SchoolManagement.Domain.Interfaces.IRepositories;
using SchoolManagement.Infrastructure.DbContext;

namespace SchoolManagement.Infrastructure.Repositories;

public class ClassRoomRepository : IClassRoomRepository
{
    private readonly AppDbContext _appDbContext;

    public ClassRoomRepository(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public async Task AddClassRoom(ClassRooms classRooms)
    {
        await _appDbContext.ClassRooms.AddAsync(classRooms);
    }

    public async Task<ClassRooms?> GetClassRoomById(Guid id)
    {
        return await _appDbContext.ClassRooms.FindAsync(id);
    }

    public async Task UpdateClassRoom(ClassRooms updatedClassRooms)
    {
        var classRoom = await _appDbContext.ClassRooms.FindAsync(updatedClassRooms.Id);
        classRoom.Grade = updatedClassRooms.Grade;
        classRoom.Subject = updatedClassRooms.Subject;
    }

    public async Task DeleteClassRoom(Guid id)
    {
        var classRoom = await _appDbContext.ClassRooms.FindAsync(id);
        _appDbContext.ClassRooms.Remove(classRoom);
    }

    public async Task<List<ClassRooms>> GetAllClassRoom()
    {
        return await _appDbContext.ClassRooms.ToListAsync();
    }

    public async Task SaveChange()
    {
       await _appDbContext.SaveChangesAsync();
    }
}