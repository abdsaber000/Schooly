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
    public static Domain.Entities.ClassRoom ToClassRooms(this AddClassRoomCommand Command)
    {
        return new Domain.Entities.ClassRoom()
        {
            ClassRoomId = new Guid(),
            Grade = Command.Grade,
            Subject = Command.Subject
        };
    }
    public static Domain.Entities.ClassRoom ToUpdatedClassRooms(this UpdateClassrRoomCommand Command)
    {
        return new Domain.Entities.ClassRoom()
        {
            ClassRoomId = Command.id,
            Grade = Command.Grade,
            Subject = Command.Subject
        };
    }
    public static ClassRoomDto ToClassRoomsDto(this Domain.Entities.ClassRoom classRooms)
    {
        return new ClassRoomDto()
        {
            Id = classRooms.ClassRoomId,
            Grade = classRooms.Grade,
            Subject = classRooms.Subject
        };
    }
}