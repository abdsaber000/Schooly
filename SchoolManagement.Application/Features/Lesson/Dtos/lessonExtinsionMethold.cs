using SchoolManagement.Application.Features.Lesson.Command.CreateLesson;
using SchoolManagement.Application.Features.Lesson.Command.UpdateLeeson;
using SchoolManagement.Domain.Entities;

namespace SchoolManagement.Application.Features.Lesson.Dtos;

public static class lessonExtinsionMethold
{
    public static Domain.Entities.Lesson ToLesson(this CreateLessonCommand command)
    {
        return new Domain.Entities.Lesson()
        {
            Id = Guid.NewGuid().ToString(),
            TeacherId = command.TeacherId,
            Subject = command.Subject,
            Title = command.Title,
            Grade = command.Grade,
            LessonType = command.LessonType,
            Date = command.Date,
            From = command.From,
            To = command.To
        };
    }
    public static Domain.Entities.Lesson ToUpdatedLesson(this UpdateLessonCommand command)
    {
        return new Domain.Entities.Lesson()
        {
            Id = command.Id,
            TeacherId = command.TeacherId,
            Subject = command.Subject,
            Title = command.Title,
            Grade = command.Grade,
            LessonType = command.LessonType,
            Date = command.Date,
            From = command.From,
            To = command.To
        };
    }
}