using System;
using MediatR;
using Microsoft.AspNetCore.Http;
using SchoolManagement.Application.Services.FaceRecognitionService;
using SchoolManagement.Application.Shared;

namespace SchoolManagement.Application.Features.Authentication.Commands.VerifyFace;

public class VerifyFaceCommand : IRequest<Result<VerifyFaceCommandDto>>
{
    required public IFormFile Image { get; set;}
}

public class VerifyFaceCommandHandler : IRequestHandler<VerifyFaceCommand, Result<VerifyFaceCommandDto>>
{
    private readonly IFaceRecognitionService _faceRecognitionService;
    public VerifyFaceCommandHandler(IFaceRecognitionService faceRecognitionService){
        _faceRecognitionService = faceRecognitionService;
    }
    public async Task<Result<VerifyFaceCommandDto>> Handle(VerifyFaceCommand request, CancellationToken cancellationToken)
    {
        var result = await _faceRecognitionService.VerifyFaceAsync(request.Image);
        return result.IsSuccess ? Result<VerifyFaceCommandDto>.Success(result.StudentId) : Result<VerifyFaceCommandDto>.Failure("Face recognition failed."); 
    }
}
