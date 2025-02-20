using Microsoft.Identity.Client;
using SchoolManagement.Application.Features.ClassRoom.Command.AddClassRoom;
using SchoolManagement.Application.Features.ClassRoom.Command.UpdateClassRoom;
using SchoolManagement.Domain.Entities;

namespace SchoolManagement.Application.Features.ClassRoom.Dtos;

public  class ClassRoomDto
{
    public Guid Id { get; set; }
    public string Subject { get; set; } = string.Empty;
    public string Grade { get; set; } = string.Empty;
}

public static class classRoomExtensionMethold
{
    public static ClassRooms ToClassRooms(this AddClassRoomCommand Command)
    {
        return new ClassRooms()
        {
            Id = new Guid(),
            Grade = Command.Grade,
            Subject = Command.Subject
        };
    }
    public static ClassRooms ToUpdatedClassRooms(this UpdateClassrRoomCommand Command)
    {
        return new ClassRooms()
        {
            Id = Command.id,
            Grade = Command.Grade,
            Subject = Command.Subject
        };
    }
    public static ClassRoomDto ToClassRoomsDto(this ClassRooms classRooms)
    {
        return new ClassRoomDto()
        {
            Id = classRooms.Id,
            Grade = classRooms.Grade,
            Subject = classRooms.Subject
        };
    }
}