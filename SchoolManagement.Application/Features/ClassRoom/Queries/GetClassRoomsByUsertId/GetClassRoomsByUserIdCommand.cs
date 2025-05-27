using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Http;
using SchoolManagement.Application.Features.ClassRoom.Dtos;
using SchoolManagement.Application.Shared;
using SchoolManagement.Domain.Entities;
using SchoolManagement.Domain.Interfaces.IRepositories;

namespace SchoolManagement.Application.Features.ClassRoom.Queries.GetClassRoomsByUsertId;

public class GetClassRoomsByUserIdCommand: IRequest<Result<List<ClassRoomDto>>>
{
    
}
public class GetClassRoomsByUserIdCommandHandler : IRequestHandler<GetClassRoomsByUserIdCommand , Result<List<ClassRoomDto>>>
{
    private IHttpContextAccessor _httpContextAccessor;
    private IStudentClassRoomRepository _studentClassRoomRepository;
    private IClassRoomRepository _classRoomRepository;
    public GetClassRoomsByUserIdCommandHandler(IHttpContextAccessor httpContextAccessor, IStudentClassRoomRepository studentClassRoomRepository, IClassRoomRepository classRoomRepository)
    {
        _httpContextAccessor = httpContextAccessor;
        _studentClassRoomRepository = studentClassRoomRepository;
        _classRoomRepository = classRoomRepository;
    }

    public async Task<Result<List<ClassRoomDto>>> Handle(GetClassRoomsByUserIdCommand request,
        CancellationToken cancellationToken)
    {
        var classRooms = new List<Domain.Entities.ClassRoom>();
        var user = _httpContextAccessor.HttpContext?.User;
        var userId = user.FindFirst(ClaimTypes.NameIdentifier).Value;
        if (user.IsInRole(Roles.Student))
        {
            classRooms = await _studentClassRoomRepository.GetAllClassRoomsByStudentId(userId);
        }
        else if (user.IsInRole(Roles.Teacher))
        {
            classRooms = await _classRoomRepository.GetAllClassRoomsByTeacherId(userId);
        }

        var classRoomsDtos = new List<ClassRoomDto>();
        foreach (var classRoom in classRooms)
        {
            classRoomsDtos.Add(classRoom.ToClassRoomsDto());
        }

        return Result<List<ClassRoomDto>>.Success(classRoomsDtos);
    }
}