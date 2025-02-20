using MediatR;
using SchoolManagement.Application.Features.ClassRoom.Dtos;
using SchoolManagement.Application.Shared;
using SchoolManagement.Domain.Entities;
using SchoolManagement.Domain.Interfaces.IRepositories;

namespace SchoolManagement.Application.Features.ClassRoom.Queries.GetAllClassRoom;

public class GetAllClassRoomQuery : IRequest<Result<List<ClassRoomDto>>>
{
    
}

public class GetAllClassRoomQueryHandler : IRequestHandler<GetAllClassRoomQuery , Result<List<ClassRoomDto>>>
{
    private readonly IClassRoomRepository _classRoomRepository;

    public GetAllClassRoomQueryHandler(IClassRoomRepository classRoomRepository)
    {
        _classRoomRepository = classRoomRepository;
    }

    public async Task<Result<List<ClassRoomDto>>> Handle(GetAllClassRoomQuery request, CancellationToken cancellationToken)
    {
        var classRooms = await _classRoomRepository.GetAllClassRoom();
        var classRoomsDtos = new List<ClassRoomDto>();
        foreach (var classRoom in classRooms)
        {
            var classRoomDto = classRoom.ToClassRoomsDto();
            classRoomsDtos.Add(classRoomDto);
        }

        return Result<List<ClassRoomDto>>.Success(classRoomsDtos);
    }
}