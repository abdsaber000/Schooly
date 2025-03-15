using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Domain.Entities;

namespace SchoolManagement.Application.Services.FileService;

public interface IFileService
{
    Task<UploadedFile> UploadFile(IFormFile file);
    Task<IActionResult> GetFileAsync(string fileUrl);
    Task<IActionResult> DeleteFileAsync(string fileUrl);
}