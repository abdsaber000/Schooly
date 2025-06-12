using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using SchoolManagement.Domain.Entities;
using SchoolManagement.Domain.Interfaces.IRepositories;

namespace SchoolManagement.Application.Services.FileService;

public class FileService : IFileService
{
    private readonly IUploadedFileRepositry _uploadedFileRepositry;
    private readonly IWebHostEnvironment _webHostEnvironment;
    public FileService(IWebHostEnvironment webHostEnvironment, IUploadedFileRepositry uploadedFileRepositry)
    {
        _webHostEnvironment = webHostEnvironment;
        _uploadedFileRepositry = uploadedFileRepositry;
    }
    public async Task<UploadedFile> UploadFile(IFormFile file)
    {
        var fakeFileName = Path.GetRandomFileName();
        var uploadedFile = new UploadedFile
        {
            FileName = file.FileName,
            ContentType = file.ContentType,
            StoredFileName = fakeFileName
        };
        var path = Path.Combine(_webHostEnvironment.ContentRootPath, "Uploads", fakeFileName);

        using FileStream fileStream = new(path, FileMode.Create);

        await file.CopyToAsync(fileStream);
        
        await _uploadedFileRepositry.AddFile(uploadedFile);
        return uploadedFile;
    }
    public async Task<(Stream? Stream, string ContentType, string FileName, string? ErrorKey)> GetFileAsync(string fileName)
    {
        var file = await _uploadedFileRepositry.GetFileByName(fileName);
        if (file == null)
        {
            return (null, "", "", "FileMetadataNotFound");
        }

        var path = Path.Combine(_webHostEnvironment.ContentRootPath, "Uploads", file.StoredFileName);

        if (!System.IO.File.Exists(path))
        {
            return (null, "", "", "FileNotFoundOnDisk");
        }

        var stream = System.IO.File.OpenRead(path);
        return (stream, file.ContentType, file.FileName, null);
    }

    public async Task<IActionResult> DeleteFileAsync(string fileUrl)
    {
        var uploadsFolder = Path.Combine(_webHostEnvironment.ContentRootPath, "Uploads");
        var filePath = Path.Combine(uploadsFolder, fileUrl);
        if (!System.IO.File.Exists(filePath))
        {
            return new NotFoundResult();
        }
        try
        {
            System.IO.File.Delete(filePath);
            await _uploadedFileRepositry.DeleteByFileUrl(fileUrl);
            return new OkResult();
        }
        catch (Exception ex)
        {
            return new StatusCodeResult(StatusCodes.Status500InternalServerError);
        }

    }
}