using System;
using MediatR;
using SchoolManagement.Application.Shared;

namespace SchoolManagement.Application.Features.Teacher.Command.DeleteTeacher;

public class DeleteTeaacherCommandRequest : IRequest<Result<string>>
{
    public string Id { get; set; } = string.Empty;

    public DeleteTeaacherCommandRequest(string id)
    {
        Id = id;
    }
}
