using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Localization;
using SchoolManagement.Application.Features.HomeWork.Dtos;
using SchoolManagement.Application.Features.Pagination;
using SchoolManagement.Domain.Entities;
using SchoolManagement.Domain.Interfaces.IRepositories;

namespace SchoolManagement.Application.Features.HomeWork.Query.GetAllClassRoomHomeWork;

public class GetActiveHomeWorkQuery : IRequest<PagedResult<HomeWorkDto>>
{
    public int page { get; set; } = 1;
    public int pageSize { get; set; } = 10;
    public Guid ClassRoomId { get; set; }
}

public class GetAllClassRoomHomeWorkQueryHandler : IRequestHandler<GetActiveHomeWorkQuery, PagedResult<HomeWorkDto>>
{
    private readonly IHomeWorkRepository _homeWorkRepository;
    private readonly IClassRoomRepository _classRoomRepository;
    private readonly IStringLocalizer<GetAllClassRoomHomeWorkQueryHandler> _localizer;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IStudentClassRoomRepository _studentClassRoomRepository;
    public GetAllClassRoomHomeWorkQueryHandler(IHomeWorkRepository homeWorkRepository, IClassRoomRepository classRoomRepository, IStringLocalizer<GetAllClassRoomHomeWorkQueryHandler> localizer, IHttpContextAccessor httpContextAccessor, IStudentClassRoomRepository studentClassRoomRepository)
    {
        _homeWorkRepository = homeWorkRepository;
        _classRoomRepository = classRoomRepository;
        _localizer = localizer;
        _httpContextAccessor = httpContextAccessor;
        _studentClassRoomRepository = studentClassRoomRepository;
    }

    public async Task<PagedResult<HomeWorkDto>> Handle(GetActiveHomeWorkQuery request, CancellationToken cancellationToken)
    {
        var user = _httpContextAccessor.HttpContext?.User;
        var userId = user.FindFirst(ClaimTypes.NameIdentifier).Value;
        var ActiveHomeWork = new List<Domain.Entities.HomeWork>();
        var totalCount = 0;
        if (request.ClassRoomId != null && request.ClassRoomId != Guid.Empty)
        {
            var classRoom = await _classRoomRepository.GetByIdAsync(request.ClassRoomId);
            if (classRoom is null)
            {
                return new PagedResult<HomeWorkDto>();
            }
            totalCount = await _homeWorkRepository
                .GetTotalCountAsyncByClassRoomId(request.ClassRoomId, cancellationToken);

            ActiveHomeWork = await _homeWorkRepository
                .GetAllActiveHomeWorkByClassRoomId(request.page, request.pageSize, request.ClassRoomId);
        }
        else if (user.IsInRole(Roles.Student))
        {
            // get all class that student is assign in it 
            var studentClassrooms = await _studentClassRoomRepository
                .GetAllClassRoomsByStudentId(userId);
            
            var classroomIds = studentClassrooms.Select(c => c.Id).ToList();
            
            // get all homeWork that for classROoms that student in it
            ActiveHomeWork = await _homeWorkRepository
                .GetActiveHomeWorksByClassRoomIds(classroomIds, request.page, request.pageSize);
            
            totalCount = await _homeWorkRepository
                .GetTotalCountAsyncByClassRoomsId(classroomIds);
        }
        else if (user.IsInRole(Roles.Teacher))
        {
            ActiveHomeWork = await _homeWorkRepository
                .GetActiveHomeWorksByTeacherId(request.page, request.pageSize, userId);
            totalCount = await _homeWorkRepository.GetTotalCountAsyncByTeacherId(userId);
        }

        var ActiveHomeWorkDto = ActiveHomeWork
            .Select(homeWork => homeWork.ToHomeWorkDto())
            .ToList();
        return new PagedResult<HomeWorkDto>
        {
            TotalItems = totalCount,
            Page = request.page,
            PageSize = request.pageSize,
            Items = ActiveHomeWorkDto
        };
    }
}