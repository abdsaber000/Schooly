using MediatR;
using Microsoft.Extensions.Localization;
using SchoolManagement.Application.Shared;
using SchoolManagement.Domain.Interfaces.IRepositories;

namespace SchoolManagement.Application.Features.HomeWork.Query.GetAllClassRoomHomeWork;

public class GetAllClassRoomHomeWorkQuery : IRequest<Result<List<Domain.Entities.HomeWork>>>
{
    public Guid ClassRoomId { get;}

    public GetAllClassRoomHomeWorkQuery(Guid classRoomId)
    {
        ClassRoomId = classRoomId;
    }
}

public class GetAllClassRoomHomeWorkQueryHandler : IRequestHandler<GetAllClassRoomHomeWorkQuery, Result<List<Domain.Entities.HomeWork>>>
{
    private readonly IHomeWorkRepository _homeWorkRepository;
    private readonly IClassRoomRepository _classRoomRepository;
    private readonly IStringLocalizer<GetAllClassRoomHomeWorkQueryHandler> _localizer;
    public GetAllClassRoomHomeWorkQueryHandler(IHomeWorkRepository homeWorkRepository, IClassRoomRepository classRoomRepository, IStringLocalizer<GetAllClassRoomHomeWorkQueryHandler> localizer)
    {
        _homeWorkRepository = homeWorkRepository;
        _classRoomRepository = classRoomRepository;
        _localizer = localizer;
    }

    public async Task<Result<List<Domain.Entities.HomeWork>>> Handle(GetAllClassRoomHomeWorkQuery request, CancellationToken cancellationToken)
    {
        var classRoom = await _classRoomRepository.GetByIdAsync(request.ClassRoomId);
        if (classRoom is null)
        {
            return Result<List<Domain.Entities.HomeWork>>.Failure(_localizer["Classroom not found"]);
        }
        var homeWorks =  await _homeWorkRepository.GetAllClassRoomHomeWork(request.ClassRoomId);
        return Result<List<Domain.Entities.HomeWork>>.Success(homeWorks);
    }
}