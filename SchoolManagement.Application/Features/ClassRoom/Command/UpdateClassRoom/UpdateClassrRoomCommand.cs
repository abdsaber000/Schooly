using System.ComponentModel.DataAnnotations;
using MediatR;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Localization;
using Microsoft.Identity.Client;
using SchoolManagement.Application.Features.ClassRoom.Dtos;
using SchoolManagement.Application.Shared;
using SchoolManagement.Domain.Interfaces.IRepositories;

namespace SchoolManagement.Application.Features.ClassRoom.Command.UpdateClassRoom;

public class UpdateClassrRoomCommand : IRequest<Result<string>>
{
    [Required]
    public Guid id { get; set; }
    
    [Required] 
    public string Grade { get; set; } = string.Empty;
    
    [Required] 
    public string Subject { get; set; } = string.Empty;
}
public class UpdateClassRoomCommandHandler:IRequestHandler<UpdateClassrRoomCommand , Result<string>>
{
    private readonly IClassRoomRepository _classRoomRepository;
    private readonly IStringLocalizer<UpdateClassRoomCommandHandler> _localizer;
    public UpdateClassRoomCommandHandler(IClassRoomRepository classRoomRepository, IStringLocalizer<UpdateClassRoomCommandHandler> localizer)
    {
        _classRoomRepository = classRoomRepository;
        _localizer = localizer;
    }

    public async Task<Result<string>> Handle(UpdateClassrRoomCommand request, CancellationToken cancellationToken)
    {
        var classRoom = await _classRoomRepository.GetByIdAsync(request.id);
        if (classRoom is null)
        {
            return Result<string>.Failure(_localizer["There is no class with this id"]);
        }

        var updatedClassRooms = request.ToUpdatedClassRooms(); 
        await _classRoomRepository.UpdateClassroomAsync(updatedClassRooms);
        
        return Result<string>.SuccessMessage(_localizer["Class Updated Successfully"]);
    }
}