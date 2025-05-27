using System;

namespace SchoolManagement.Application.Services.FaceRecognitionService;

public class FaceRecognitionServiceDto
{
    public string StudentId { get; set; } = string.Empty;
    public bool IsSuccess { get; set; }
    public string ErrorMessage { get; set; } = string.Empty;
}
