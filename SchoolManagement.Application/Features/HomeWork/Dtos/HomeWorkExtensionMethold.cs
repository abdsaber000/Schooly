using SchoolManagement.Application.Features.HomeWorke.Commands.AddHomeworke;
using SchoolManagement.Domain.Entities;

namespace SchoolManagement.Application.Features.HomeWork.Dtos;

public static class HomeWorkExtensionMethold
{
    public static Domain.Entities.HomeWork? ToHomeWork(this AddHomeWorkCommands commands, ApplicationUser teacher)
    {
        return new Domain.Entities.HomeWork()
        {
            Id = new Guid(),
            FileUrl = commands.FileUrl,
            classRoomId = commands.classRoomId,
            lessonId = commands.lessonId,
            teacherId = teacher.Id
        };
    }
}