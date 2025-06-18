using Microsoft.Identity.Client;
using SchoolManagement.Application.Features.ClassRoom.Command.AddClassRoom;
using SchoolManagement.Application.Features.ClassRoom.Command.UpdateClassRoom;
using SchoolManagement.Domain.Entities;

namespace SchoolManagement.Application.Features.ClassRoom.Dtos;

public  class ClassRoomDto
{
    public Guid Id { get; set; }
    public string TeacherId { get; set; } 
    public string TeacherName { get; set; } = string.Empty;
    public int NumberOfStudents { get; set; } = 0;
    public string Subject { get; set; } = string.Empty;
    public string Grade { get; set; } = string.Empty;
}

public static class classRoomExtensionMethold
{
    public static Domain.Entities.ClassRoom ToClassRooms(this AddClassRoomCommand Command)
    {
        return new Domain.Entities.ClassRoom()
        {
            Id = new Guid(),
            Grade = Command.Grade,
            Subject = Command.Subject
        };
    }
    public static Domain.Entities.ClassRoom ToUpdatedClassRooms(this UpdateClassrRoomCommand Command)
    {
        return new Domain.Entities.ClassRoom()
        {
            Id = Command.id,
            Grade = Command.Grade,
            Subject = Command.Subject
        };
    }
    public static ClassRoomDto ToClassRoomsDto(this Domain.Entities.ClassRoom classRooms)
    {
        return new ClassRoomDto()
        {
            Id = classRooms.Id,
            TeacherId = classRooms.TeacherId,
            TeacherName = classRooms.Teacher?.Name ?? "Unknown",
            NumberOfStudents = classRooms.StudentClassRooms.Count,
            Grade = classRooms.Grade,
            Subject = classRooms.Subject
        };
    }
}