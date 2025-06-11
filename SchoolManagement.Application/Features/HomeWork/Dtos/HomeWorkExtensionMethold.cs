using SchoolManagement.Application.Features.HomeWork.Commands.AddHomeWork;
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
    private static DateTime GetCurrentEgyptTime()
    {
        var egyptTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Africa/Cairo");
        var nowEgypt = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, egyptTimeZone);
        return nowEgypt;
    }
    public static Domain.Entities.HomeWork? ToHomeWork(this AddHomeWorkCommands commands, ApplicationUser teacher)
    {
        return new Domain.Entities.HomeWork()
        {
            Id = new Guid(),
            FileUrl = commands.FileUrl,
            ToDate = commands.ToDate,
            FromDate = GetCurrentEgyptTime(),
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