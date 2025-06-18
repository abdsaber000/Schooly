using MediatR;
using SchoolManagement.Application.Features.ClassRoom.Dtos;
using SchoolManagement.Application.Features.Pagination;
using SchoolManagement.Application.Shared;
using SchoolManagement.Domain.Entities;
using SchoolManagement.Domain.Interfaces.IRepositories;

namespace SchoolManagement.Application.Features.ClassRoom.Queries.GetAllClassRoom;

public class GetAllClassRoomQuery : IRequest<PagedResult<ClassRoomDto>>
{
    public int page { get; set; } = 1;
    public int pageSize { get; set; } = 10;
}

public class GetAllClassRoomQueryHandler : IRequestHandler<GetAllClassRoomQuery , PagedResult<ClassRoomDto>>
{
    private readonly IClassRoomRepository _classRoomRepository;

    public GetAllClassRoomQueryHandler(IClassRoomRepository classRoomRepository)
    {
        _classRoomRepository = classRoomRepository;
    }

    public async Task<PagedResult<ClassRoomDto>> Handle(GetAllClassRoomQuery request, CancellationToken cancellationToken)
    {
        var classRooms = await _classRoomRepository
            .GetAllClassroomsPagedAsync(request.page, request.pageSize, cancellationToken);
        
        var totalCount = await _classRoomRepository.GetTotalCountAsync(cancellationToken);
        
        var classRoomsDtos = classRooms
            .Select(cr => cr.ToClassRoomsDto())
            .ToList();
        
        return new PagedResult<ClassRoomDto>
        {
            Items = classRoomsDtos,
            TotalItems = totalCount,
            Page = request.page,
            PageSize = request.pageSize
        };
    }
}