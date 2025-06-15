using System;
using MediatR;
using Microsoft.AspNetCore.Http;
using SchoolManagement.Application.Services.FaceRecognitionService;
using SchoolManagement.Application.Shared;

namespace SchoolManagement.Application.Features.FaceRecognition.Commands.RegisterFace;

public class RegisterFaceCommand : IRequest<Result<string>>
{
    public string UserId { get; set; } = string.Empty;
    required public IFormFile Image { get; set; }
}

public class RegisterFaceCommandHandler : IRequestHandler<RegisterFaceCommand, Result<string>>
{
    private readonly IFaceRecognitionService _faceRecognitionService;
    public RegisterFaceCommandHandler(IFaceRecognitionService faceRecognitionService)
    {
        _faceRecognitionService = faceRecognitionService;
    }
    public async Task<Result<string>> Handle(RegisterFaceCommand request, CancellationToken cancellationToken)
    {
        var result = await _faceRecognitionService.RegisterFaceAsync(request.UserId , request.Image);
        return result ? Result<string>.SuccessMessage("Face is registered successfully.") : Result<string>.Failure("Face registration failed.");
    }
}
