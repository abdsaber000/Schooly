using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Http;
using SchoolManagement.Application.Features.HomeWork.Dtos;
using SchoolManagement.Application.Features.Pagination;
using SchoolManagement.Domain.Entities;
using SchoolManagement.Domain.Interfaces.IRepositories;

namespace SchoolManagement.Application.Features.HomeWork.Query.GetAllHomeWork;

public class GetِِِِAllHomeWorkQuery : IRequest<PagedResult<HomeWorkDto>>
{
    public int page { get; set; } = 1;
    public int pageSize { get; set; } = 10;
    public Guid ClassRoomId { get; set; }
}

public class GetAllClassRoomHomeWorkQueryHandler : IRequestHandler<GetِِِِAllHomeWorkQuery, PagedResult<HomeWorkDto>>
{
    private readonly IHomeWorkRepository _homeWorkRepository;
    private readonly IClassRoomRepository _classRoomRepository;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IStudentClassRoomRepository _studentClassRoomRepository;
    private readonly IHomeWorkSubmissionRepositry _homeWorkSubmissionRepositry;
    public GetAllClassRoomHomeWorkQueryHandler(IHomeWorkRepository homeWorkRepository, IClassRoomRepository classRoomRepository, IHttpContextAccessor httpContextAccessor, IStudentClassRoomRepository studentClassRoomRepository, IHomeWorkSubmissionRepositry homeWorkSubmissionRepositry)
    {
        _homeWorkRepository = homeWorkRepository;
        _classRoomRepository = classRoomRepository;
        _httpContextAccessor = httpContextAccessor;
        _studentClassRoomRepository = studentClassRoomRepository;
        _homeWorkSubmissionRepositry = homeWorkSubmissionRepositry;
    }

    public async Task<PagedResult<HomeWorkDto>> Handle(GetِِِِAllHomeWorkQuery request, CancellationToken cancellationToken)
    {
        var user = _httpContextAccessor.HttpContext?.User;
        var userId = user.FindFirst(ClaimTypes.NameIdentifier).Value;
        var HomeWorks = new List<Domain.Entities.HomeWork>();
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

            HomeWorks = await _homeWorkRepository
                .GetAllHomeWorkByClassRoomId(request.page, request.pageSize, request.ClassRoomId);
        }
        else if (user.IsInRole(Roles.Student))
        {
            // get all class that student is assign in it 
            var studentClassrooms = await _studentClassRoomRepository
                .GetAllClassRoomsByStudentId(userId);
            
            var classroomIds = studentClassrooms.Select(c => c.Id).ToList();
            
            // get all homeWork that for classROoms that student in it
            HomeWorks = await _homeWorkRepository
                .GetHomeWorksByClassRoomIds(classroomIds, request.page, request.pageSize);
            
            totalCount = await _homeWorkRepository
                .GetTotalCountAsyncByClassRoomsId(classroomIds);
        }
        else if (user.IsInRole(Roles.Teacher))
        {
             HomeWorks = await _homeWorkRepository
                .GetHomeWorksByTeacherId(request.page, request.pageSize, userId);
            totalCount = await _homeWorkRepository.GetTotalCountAsyncByTeacherId(userId);
        }

        var HomeWorskDto = HomeWorks
            .Select(homeWork => homeWork.ToHomeWorkDto())
            .ToList();

        foreach (var hw in HomeWorskDto)
        {
            hw.isSubmitted = await _homeWorkSubmissionRepositry.isSubmittedByStudent(userId, hw.homeWorkId);
            hw.totalSubmissions =
                await _homeWorkSubmissionRepositry.GetTotalCountSubmittedStudentsByHomeWorkIdAsync(hw.homeWorkId);
        }
        
        return new PagedResult<HomeWorkDto>
        {
            TotalItems = totalCount,
            Page = request.page,
            PageSize = request.pageSize,
            Items = HomeWorskDto
        };
    }
}