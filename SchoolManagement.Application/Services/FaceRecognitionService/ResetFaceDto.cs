using System;

namespace SchoolManagement.Application.Services.FaceRecognitionService;

public class ResetFaceDto
{
    public bool IsSuccess { get; set; }
    public string Message { get; set; } = string.Empty;
}
