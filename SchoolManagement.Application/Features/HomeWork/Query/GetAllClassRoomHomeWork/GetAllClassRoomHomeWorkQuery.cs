using MediatR;
using SchoolManagement.Application.Shared;
using SchoolManagement.Domain.Interfaces.IRepositories;

namespace SchoolManagement.Application.Features.HomeWork.Query.GetAllClassRoomHomeWork;

public class GetAllClassRoomHomeWorkQuery : IRequest<Result<List<Domain.Entities.HomeWork>>>
{
    public Guid classRoomId;

    public GetAllClassRoomHomeWorkQuery(Guid classRoomId)
    {
        classRoomId = classRoomId;
    }
}

public class GetAllClassRoomHomeWorkQueryHandler : IRequestHandler<GetAllClassRoomHomeWorkQuery, Result<List<Domain.Entities.HomeWork>>>
{
    private readonly IHomeWorkRepository _homeWorkRepository;
    public GetAllClassRoomHomeWorkQueryHandler(IHomeWorkRepository homeWorkRepository)
    {
        _homeWorkRepository = homeWorkRepository;
    }

    public async Task<Result<List<Domain.Entities.HomeWork>>> Handle(GetAllClassRoomHomeWorkQuery request, CancellationToken cancellationToken)
    {
        var homeWorks =  await _homeWorkRepository.GetAllClassRoomHomeWork(request.classRoomId);
        return Result<List<Domain.Entities.HomeWork>>.Success(homeWorks);
    }
}