using System.Data;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using SchoolManagement.Application.Shared;
using SchoolManagement.Domain.Entities;
using SchoolManagement.Domain.Interfaces.IRepositories;

namespace SchoolManagement.Application.Features.HomeWork.Commands.SubmitHomeWork;

public class SubmitHomeWorkCommandHandler : IRequestHandler<SubmitHomeWorkCommand, Result<string>>
{
    private readonly IHomeWorkRepository _homeWorkRepository;
    private readonly IHomeWorkSubmissionRepositry _homeWorkSubmissionRepositry;
    private readonly IStringLocalizer<SubmitHomeWorkCommandHandler> _localizer;
    private readonly IUploadedFileRepositry _uploadedFileRepositry;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IHttpContextAccessor _httpContextAccessor;
    public SubmitHomeWorkCommandHandler(IHomeWorkRepository homeWorkRepository, IStringLocalizer<SubmitHomeWorkCommandHandler> localizer, IUploadedFileRepositry uploadedFileRepositry, UserManager<ApplicationUser> userManager, IHttpContextAccessor httpContextAccessor, IHomeWorkSubmissionRepositry homeWorkSubmissionRepositry)
    {
        _homeWorkRepository = homeWorkRepository;
        _localizer = localizer;
        _uploadedFileRepositry = uploadedFileRepositry;
        _userManager = userManager;
        _httpContextAccessor = httpContextAccessor;
        _homeWorkSubmissionRepositry = homeWorkSubmissionRepositry;
    }

    public async Task<Result<string>> Handle(SubmitHomeWorkCommand request, CancellationToken cancellationToken)
    {
        var user = _userManager.GetUserAsync(_httpContextAccessor?.HttpContext.User).Result;
        var homeWork = await _homeWorkRepository.GetByIdAsync(request.HomeWorkId);
        if (homeWork is null)
        {
            return Result<string>.Failure(_localizer["HomeWork not found"]);
        }
        if (DateTime.UtcNow > homeWork.Deadline)
        {
            return Result<string>.Failure(_localizer["HomeWorkSubmissionClosed"]);
        }
        var file = await _uploadedFileRepositry.GetFileByName(request.FileUrl);
        var submission = new HomeWorkSubmission
        {
            Id = Guid.NewGuid(),
            HomeWorkId = request.HomeWorkId,
            StudentId = user.Id, 
            FileUrl = request.FileUrl,
            FileName = file.FileName,
            SubmittedAt = DateTime.UtcNow
        };
        await _homeWorkSubmissionRepositry.AddAsync(submission);
        return Result<string>.SuccessMessage(_localizer["HomeWorkSubmittedSuccessfully"]);
    }
}