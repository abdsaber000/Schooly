using MediatR;
using Microsoft.Extensions.Localization;
using Microsoft.Identity.Client;
using SchoolManagement.Application.Features.ClassRoom.Dtos;
using SchoolManagement.Application.Shared;
using SchoolManagement.Domain.Interfaces.IRepositories;

namespace SchoolManagement.Application.Features.ClassRoom.Queries.GetClassRoomById;

public class GetClassRoomByIdQuery :IRequest<Result<ClassRoomDto>>
{
    public Guid id;

    public GetClassRoomByIdQuery(Guid id)
    {
        this.id = id;
    }
}
public class GetClassRoomByIdQueryHandler : IRequestHandler<GetClassRoomByIdQuery , Result<ClassRoomDto>>
{
    private readonly IClassRoomRepository _classRoomRepository;
    private readonly IStringLocalizer<GetClassRoomByIdQueryHandler> _localizer;
    public GetClassRoomByIdQueryHandler(IClassRoomRepository classRoomRepository, IStringLocalizer<GetClassRoomByIdQueryHandler> localizer)
    {
        _classRoomRepository = classRoomRepository;
        _localizer = localizer;
    }

    public async Task<Result<ClassRoomDto>> Handle(GetClassRoomByIdQuery request, CancellationToken cancellationToken)
    {
        var classRoom = await _classRoomRepository.GetByIdAsync(request.id);
        if (classRoom is null) {
            return Result<ClassRoomDto>.Failure(_localizer["Classroom not found"]);
        }
        var classRoomDto = classRoom.ToClassRoomsDto();
        return Result<ClassRoomDto>.Success(classRoomDto);
    }
}