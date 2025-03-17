using SchoolManagement.Application.Features.HomeWorke.Commands.AddHomeworke;
using SchoolManagement.Domain.Entities;

namespace SchoolManagement.Application.Features.HomeWork.Dtos;

public class HomeWorkDto
{
    public Guid homeWorkId { get; set; }
    public string fileUrl { get; set;}
    public string fileName { get; set; }
    public string lessonTitle { get; set; }
}
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
    public static HomeWorkDto ToHomeWorkDto(this Domain.Entities.HomeWork homeWork)
    {
        return new HomeWorkDto()
        {
           homeWorkId = homeWork.Id,
           fileName = homeWork.fileName,
           fileUrl = homeWork.FileUrl,
           lessonTitle = homeWork.Lesson.Title
        };
    }
}