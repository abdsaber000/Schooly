using System;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using SchoolManagement.Application.Services.FaceRecognitionService;
using SchoolManagement.Application.Services.TokenService;
using SchoolManagement.Application.Shared;
using SchoolManagement.Domain.Entities;

namespace SchoolManagement.Application.Features.Authentication.Commands.VerifyFace;

public class VerifyFaceCommand : IRequest<Result<string>>
{
    required public IFormFile Image { get; set;}
}

public class VerifyFaceCommandHandler : IRequestHandler<VerifyFaceCommand, Result<string>>
{
    private readonly IFaceRecognitionService _faceRecognitionService;
    private readonly ITokenService _tokenService;
    private readonly UserManager<ApplicationUser> _userManager;
    public VerifyFaceCommandHandler(IFaceRecognitionService faceRecognitionService
                                    , ITokenService tokenService
                                    , UserManager<ApplicationUser> userManager) {
        _faceRecognitionService = faceRecognitionService;
        _tokenService = tokenService;
        _userManager = userManager;
    }
    public async Task<Result<string>> Handle(VerifyFaceCommand request, CancellationToken cancellationToken)
    {
        var result = await _faceRecognitionService.VerifyFaceAsync(request.Image);
        if(!result.IsSuccess){
            return Result<string>.Failure(result.ErrorMessage);
        } 
        var user = await _userManager.FindByIdAsync(result.StudentId);
        if(user is null){
            return Result<string>.Failure("User id is not valid.");
        }
        return Result<string>.Success(await _tokenService.GenerateToken(user, true));
    }
}
