using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using SchoolManagement.Application.Features.Authentication.Dtos;
using SchoolManagement.Application.Features.FaceRecognition.VerifyFace;
using SchoolManagement.Application.Services.FaceRecognitionService;
using SchoolManagement.Application.Services.TokenService;
using SchoolManagement.Application.Shared;
using SchoolManagement.Domain.Entities;

namespace SchoolManagement.Application.Features.FaceRecognition.Commands.VerifyFace;

public class VerifyFaceCommand : IRequest<Result<VerifyFaceCommandDto>>
{
    required public IFormFile Image { get; set;}
}

public class VerifyFaceCommandHandler : IRequestHandler<VerifyFaceCommand, Result<VerifyFaceCommandDto>>
{
    private readonly IFaceRecognitionService _faceRecognitionService;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly ITokenService _tokenService;
    public VerifyFaceCommandHandler(
        IFaceRecognitionService faceRecognitionService,
        UserManager<ApplicationUser> userManager,
        ITokenService tokenService)
    {
        _faceRecognitionService = faceRecognitionService;
        _userManager = userManager;
        _tokenService = tokenService;
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
        var token = await _tokenService.GenerateToken(user, true);
        return Result<VerifyFaceCommandDto>.Success(user.ToVerifyFaceCommandDto(), token);
    }
}
