using SchoolManagement.Application.Features.HomeWorke.Commands.AddHomeworke;
using SchoolManagement.Domain.Entities;

namespace SchoolManagement.Application.Features.HomeWork.Dtos;

public static class HomeWorkExtensionMethold
{
    public static Domain.Entities.HomeWork? ToHomeWork(this AddHomeWorkCommands commands, ApplicationUser teacher , string fileName)
    {
        return new Domain.Entities.HomeWork()
        {
            fileName = fileName,
            classRoomId = commands.classRoomId,
            lessonId = commands.lessonId,
            Id = new Guid(),
            teacherId = teacher.Id
        };
    }
}