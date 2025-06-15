using System;
using MediatR;
using SchoolManagement.Application.Services.FaceRecognitionService;
using SchoolManagement.Application.Shared;

namespace SchoolManagement.Application.Features.FaceRecognition.ResetFace;

public class ResetFaceCommand : IRequest<Result<string>>
{
    public string Id { get; set; } = string.Empty;
    public ResetFaceCommand(string id)
    {
        Id = id;
    }
}

public class ResetFaceCommandHandler : IRequestHandler<ResetFaceCommand, Result<string>>
{
    private readonly IFaceRecognitionService _faceRecognitionService;

    public ResetFaceCommandHandler(IFaceRecognitionService faceRecognitionService)
    {
        _faceRecognitionService = faceRecognitionService;
    }

    public async Task<Result<string>> Handle(ResetFaceCommand request, CancellationToken cancellationToken)
    {
        var result = await _faceRecognitionService.ResetFaceAsync(request.Id);
        return result.IsSuccess ? Result<string>.SuccessMessage(result.Message) 
            : Result<string>.Failure(result.Message);
    }
}

