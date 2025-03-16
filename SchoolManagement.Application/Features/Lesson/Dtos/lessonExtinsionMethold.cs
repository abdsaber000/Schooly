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
            Id = Guid.NewGuid(),
            ClassRoomId = command.ClassRoomId,
            Title = command.Title,
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
            ClassRoomId = command.ClassRoomId,
            Title = command.Title,
            LessonType = command.LessonType,
            Date = command.Date,
            From = command.From,
            To = command.To
        };
    }

    public static LessonDto ToLessonDto(this Domain.Entities.Lesson lesson)
    {
        return new LessonDto()
        {
            Id = lesson.Id,
            LessonType = lesson.LessonType,
            Title = lesson.Title,
            Subject = lesson.ClassRoom.Subject,
            Grade = lesson.ClassRoom.Grade,
            TeacherId = lesson.TeacherId,
            Date = lesson.Date,
            From = lesson.From,
            To = lesson.To
        };
    }
}