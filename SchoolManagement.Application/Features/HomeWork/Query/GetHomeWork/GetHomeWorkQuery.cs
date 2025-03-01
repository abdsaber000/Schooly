using MediatR;
using SchoolManagement.Application.Shared;
using SchoolManagement.Domain.Interfaces.IRepositories;

namespace SchoolManagement.Application.Features.HomeWork.Query.GetHomeWork;

public class GetHomeWorkQuery : IRequest<Result<Domain.Entities.HomeWork>>
{
    public Guid HomeWorkId;

    public GetHomeWorkQuery(Guid homeWorkId)
    {
        HomeWorkId = homeWorkId;
    }
}

public class GetHomeWorkQueryHandler : IRequestHandler<GetHomeWorkQuery, Result<Domain.Entities.HomeWork>>
{
    private readonly IHomeWorkRepository _homeWorkRepository;

    public GetHomeWorkQueryHandler(IHomeWorkRepository homeWorkRepository)
    {
        _homeWorkRepository = homeWorkRepository;
    }

    public async Task<Result<Domain.Entities.HomeWork>> Handle(GetHomeWorkQuery request, CancellationToken cancellationToken)
    {
        var homeWork = await _homeWorkRepository.GetHomeWork(request.HomeWorkId);
        return Result<Domain.Entities.HomeWork>.Success(homeWork);
    }
}