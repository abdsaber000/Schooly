using MediatR;
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

    public GetClassRoomByIdQueryHandler(IClassRoomRepository classRoomRepository)
    {
        _classRoomRepository = classRoomRepository;
    }

    public async Task<Result<ClassRoomDto>> Handle(GetClassRoomByIdQuery request, CancellationToken cancellationToken)
    {
        var classRoom = await _classRoomRepository.GetClassRoomById(request.id);
        var classRoomDto = classRoom.ToClassRoomsDto();
        return Result<ClassRoomDto>.Success(classRoomDto);
    }
}