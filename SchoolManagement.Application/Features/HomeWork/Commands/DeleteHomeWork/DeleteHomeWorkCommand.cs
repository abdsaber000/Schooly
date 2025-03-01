using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using SchoolManagement.Application.Shared;
using SchoolManagement.Domain.Interfaces.IRepositories;

namespace SchoolManagement.Application.Features.HomeWork.Commands.DeleteHomeWork;

public class DeleteHomeWorkCommand : IRequest<Result<string>>
{
    public DeleteHomeWorkCommand(Guid homeWorkId)
    {
        HomeWorkId = homeWorkId;
    }

    public Guid HomeWorkId { get; }
}

public class DeleteHomeWorkCommandHandler : IRequestHandler<DeleteHomeWorkCommand, Result<string>>
{
    private readonly IHomeWorkRepository _homeWorkRepository;
    private readonly IStringLocalizer<DeleteHomeWorkCommand> _localizer;
    public DeleteHomeWorkCommandHandler(IHomeWorkRepository homeWorkRepository, IStringLocalizer<DeleteHomeWorkCommand> localizer)
    {
        _homeWorkRepository = homeWorkRepository;
        _localizer = localizer;
    }

    public async Task<Result<string>> Handle(DeleteHomeWorkCommand request, CancellationToken cancellationToken)
    {
        await _homeWorkRepository.DeleteHomeWork(request.HomeWorkId);
        return Result<string>.SuccessMessage(_localizer["HomeWork deleted successfully"]);
    }
}