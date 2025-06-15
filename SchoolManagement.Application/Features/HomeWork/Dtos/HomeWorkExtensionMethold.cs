using SchoolManagement.Application.Features.HomeWork.Commands.AddHomeWork;
using SchoolManagement.Domain.Entities;

namespace SchoolManagement.Application.Features.HomeWork.Dtos;

public class HomeWorkDto
{
    public Guid homeWorkId { get; set; }
    public string fileUrl { get; set;}
    public string fileName { get; set; }
    public string lessonTitle { get; set; }
    public string grade { get; set; }
    public string teacherName { get; set; } = string.Empty; // for student to see who is the teacher
    public string subjectName { get; set; } = string.Empty; // for student to see which subject this home work is for
    public bool isSubmitted { get; set; }     // for student to see if he submitted this home work or not
    public int totalSubmissions { get; set; } // see how many students submitted this home work
    public DateTime Deadline { get; set; } 
}
public static class HomeWorkExtensionMethold
{
    public static Domain.Entities.HomeWork? ToHomeWork(this AddHomeWorkCommands commands, ApplicationUser teacher)
    {
        return new Domain.Entities.HomeWork()
        {
            Id = new Guid(),
            FileUrl = commands.FileUrl,
            Deadline = commands.Deadline,
            FromDate = DateTime.UtcNow,
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
            lessonTitle = homeWork.Lesson.Title,
            Deadline = homeWork.Deadline,
            subjectName = homeWork.Lesson.ClassRoom.Subject,
            teacherName = homeWork.Lesson.Teacher.Name,
            grade = homeWork.Lesson.ClassRoom.Grade
        };
    }
}