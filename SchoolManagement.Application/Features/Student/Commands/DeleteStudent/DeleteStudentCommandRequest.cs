using System;
using MediatR;
using SchoolManagement.Application.Shared;

namespace SchoolManagement.Application.Features.Student.Commands.DeleteStudent;

public class DeleteStudentCommandRequest : IRequest<Result<string>>
{
    public string Id { get; set; } = string.Empty;
    public DeleteStudentCommandRequest(string id)
    {
        Id = id;
    }
}
