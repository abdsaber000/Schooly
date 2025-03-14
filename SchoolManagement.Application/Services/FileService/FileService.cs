using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;

namespace SchoolManagement.Application.Services.FileService;

public class FileService : IFileService
{
    private readonly IWebHostEnvironment _webHostEnvironment;
    public FileService(IWebHostEnvironment webHostEnvironment)
    {
        _webHostEnvironment = webHostEnvironment;
    }
    public async Task<string> UploadFile(IFormFile file)
    {
        string uniqueFileName = string.Empty;

        if (file is not null && file.Length > 0)
        {
            string uploadsFolder = Path.Combine(_webHostEnvironment.ContentRootPath , "Uploads");
            uniqueFileName = Guid.NewGuid() + "_" + file.FileName;
            string filePath = Path.Combine(uploadsFolder, uniqueFileName);

            using (var fileStream = new FileStream(filePath, FileMode.Create , FileAccess.Write, FileShare.None, 4096, useAsync: true))
            {
                await file.CopyToAsync(fileStream);
            }
        }
        return uniqueFileName;
    }
    public async Task<IActionResult> GetFileAsync(string fileName)
    {
      
        var uploadsFolder = Path.Combine(_webHostEnvironment.ContentRootPath, "Uploads");
        var filePath = Path.Combine(uploadsFolder, fileName);

        if (!System.IO.File.Exists(filePath))
        {
            return new NotFoundResult();
        }
        var contentType = GetContentType(filePath);
        return new PhysicalFileResult(fileName, contentType)
        {
            FileDownloadName = filePath
        };
    }

    public async Task<IActionResult> DeleteFileAsync(string fileName)
    {
        var uploadsFolder = Path.Combine(_webHostEnvironment.ContentRootPath, "Uploads");
        var filePath = Path.Combine(uploadsFolder, fileName);

        if (!System.IO.File.Exists(filePath))
        {
            return new NotFoundResult();
        }
        try
        {
            System.IO.File.Delete(filePath);
            return new OkResult();
        }
        catch (Exception ex)
        {
            return new StatusCodeResult(StatusCodes.Status500InternalServerError);
        }
    }

    private string GetContentType(string filePath)
    {
        var provider = new FileExtensionContentTypeProvider();
        if (!provider.TryGetContentType(filePath, out var contentType))
        {
            contentType = "octet-stream";
        }
        return contentType;
    }
}