using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using SchoolManagement.Application.Features.Authentication.Dtos;
using SchoolManagement.Application.Services.FaceRecognitionService;
using SchoolManagement.Application.Shared;
using SchoolManagement.Domain.Entities;

namespace SchoolManagement.Application.Features.Authentication.Commands.VerifyFace;

public class VerifyFaceCommand : IRequest<Result<VerifyFaceCommandDto>>
{
    required public IFormFile Image { get; set;}
}

public class VerifyFaceCommandHandler : IRequestHandler<VerifyFaceCommand, Result<VerifyFaceCommandDto>>
{
    private readonly IFaceRecognitionService _faceRecognitionService;
    private readonly UserManager<ApplicationUser> _userManager;
    public VerifyFaceCommandHandler(IFaceRecognitionService faceRecognitionService
                                    , UserManager<ApplicationUser> userManager) {
        _faceRecognitionService = faceRecognitionService;
        _userManager = userManager;
    }
    public async Task<Result<VerifyFaceCommandDto>> Handle(VerifyFaceCommand request, CancellationToken cancellationToken)
    {
        var result = await _faceRecognitionService.VerifyFaceAsync(request.Image);
        if(!result.IsSuccess){
            return Result<VerifyFaceCommandDto>.Failure(result.ErrorMessage);
        } 
        var user = await _userManager.FindByIdAsync(result.StudentId);
        if(user is null){
            return Result<VerifyFaceCommandDto>.Failure("User id is not valid.");
        }
        return Result<VerifyFaceCommandDto>.Success(user.ToVerifyFaceCommandDto());
    }
}
