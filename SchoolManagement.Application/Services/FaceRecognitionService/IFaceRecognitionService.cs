using Microsoft.AspNetCore.Http;

namespace SchoolManagement.Application.Services.FaceRecognitionService;

public interface IFaceRecognitionService
{
    Task<bool> RegisterFaceAsync(string studentId, IFormFile image);
    Task<FaceRecognitionServiceDto> VerifyFaceAsync(IFormFile image);
}
