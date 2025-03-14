using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SchoolManagement.Application.Services.FileService;

public interface IFileService
{
    Task<string> UploadFile(IFormFile file);
    Task<IActionResult> GetFileAsync(string fileName);
    Task<IActionResult> DeleteFileAsync(string fileName);
}