using MediatR;
using Microsoft.Extensions.Localization;
using SchoolManagement.Application.Shared;
using SchoolManagement.Domain.Interfaces.IRepositories;

namespace SchoolManagement.Application.Features.ClassRoom.Command.DeleteClassRoom;

public class DeleteClassRoomCommand : IRequest<Result<string>>
{
    public Guid id;

    public DeleteClassRoomCommand(Guid id)
    {
        this.id = id;
    }
}
public class DeleteClassRoomCommandHnadler : IRequestHandler<DeleteClassRoomCommand , Result<string>>
{
    private readonly IClassRoomRepository _classRoomRepository;
    private readonly IStringLocalizer<DeleteClassRoomCommandHnadler> _localizer;
    
    public DeleteClassRoomCommandHnadler(IClassRoomRepository classRoomRepository, IStringLocalizer<DeleteClassRoomCommandHnadler> localizer)
    {
        _classRoomRepository = classRoomRepository;
        _localizer = localizer;
    }

    public async Task<Result<string>> Handle(DeleteClassRoomCommand request, CancellationToken cancellationToken)
    {
        var classRoom = await _classRoomRepository.GetClassRoomById(request.id);
        if (classRoom is null)
        {
            return Result<string>.Failure(_localizer["There is no class with this id"]);
        }
        await _classRoomRepository.DeleteClassRoom(request.id);
        await _classRoomRepository.SaveChange();
        
        return Result<string>.SuccessMessage(_localizer["Class Room Deleted Successfully"]);
    }
}