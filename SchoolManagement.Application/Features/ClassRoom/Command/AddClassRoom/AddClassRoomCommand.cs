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
    [Required] 
    public string Grade { get; set; } = string.Empty;

    [Required] 
    public string Subject { get; set; } = string.Empty;
}

public class AddClassRoomCommandHandler:IRequestHandler<AddClassRoomCommand , Result<string>>
{
    private readonly IClassRoomRepository _classRoomRepository;
    private readonly IStringLocalizer<AddClassRoomCommandHandler> _localizer;
    private readonly IHttpContextAccessor _httpContextAccessor;
    public AddClassRoomCommandHandler(IClassRoomRepository classRoomRepository, IStringLocalizer<AddClassRoomCommandHandler> localizer, IHttpContextAccessor httpContextAccessor)
    {
        _classRoomRepository = classRoomRepository;
        _localizer = localizer;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<Result<string>> Handle(AddClassRoomCommand request, CancellationToken cancellationToken)
    {
        var classRoom = request.ToClassRooms();
        var userIdClaim = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier);
        var teacherId = userIdClaim.Value;
        classRoom.TeacherId = teacherId;
        await _classRoomRepository.AddAsync(classRoom);

        return Result<string>.SuccessMessage(_localizer["Class Room Created successfully"]);
    }
} 