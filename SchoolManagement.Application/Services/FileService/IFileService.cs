using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Domain.Entities;

namespace SchoolManagement.Application.Services.FileService;

public interface IFileService
{
    Task<UploadedFile> UploadFile(IFormFile file);
    Task<(Stream? Stream, string ContentType, string FileName, string? ErrorKey)> GetFileAsync(string fileName);
    Task<IActionResult> DeleteFileAsync(string fileUrl);
}