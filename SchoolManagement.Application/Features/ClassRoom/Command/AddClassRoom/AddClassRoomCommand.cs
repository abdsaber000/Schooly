using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Localization;
using SchoolManagement.Application.Features.ClassRoom.Dtos;
using SchoolManagement.Application.Shared;
using SchoolManagement.Domain.Interfaces.IRepositories;

namespace SchoolManagement.Application.Features.ClassRoom.Command.AddClassRoom;

public class AddClassRoomCommand  : IRequest<Result<string>>
{
    [Required(ErrorMessage = "Grade is required.")]
    public string Grade { get; set; } = string.Empty;

    [Required(ErrorMessage = "Subject is required.")]
    public string Subject { get; set; } = string.Empty;
}

public class AddClassRoomCommandHandler:IRequestHandler<AddClassRoomCommand , Result<string>>
{
    private readonly IClassRoomRepository _classRoomRepository;
    private readonly IStringLocalizer<AddClassRoomCommandHandler> _localizer;
    public AddClassRoomCommandHandler(IClassRoomRepository classRoomRepository, IStringLocalizer<AddClassRoomCommandHandler> localizer)
    {
        _classRoomRepository = classRoomRepository;
        _localizer = localizer;
    }

    public async Task<Result<string>> Handle(AddClassRoomCommand request, CancellationToken cancellationToken)
    {
        var classRoom = request.ToClassRooms();
        await _classRoomRepository.AddAsync(classRoom);

        return Result<string>.SuccessMessage(_localizer["Class Room Created successfully"]);
    }
} 