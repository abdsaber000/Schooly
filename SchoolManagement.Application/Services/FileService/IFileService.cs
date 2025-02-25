using Microsoft.AspNetCore.Http;

namespace SchoolManagement.Application.Services.FileService;

public interface IFileService
{
    Task<string> UploadFile(IFormFile file);
}