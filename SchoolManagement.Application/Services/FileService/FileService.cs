using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

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
}