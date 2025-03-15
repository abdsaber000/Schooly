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
    public async Task<IActionResult> GetFileAsync(string fileUrl)
    {
        var file = await _uploadedFileRepositry.GetFileByName(fileUrl);
        if (file == null)
        {
            return new NotFoundResult();
        }
        var path = Path.Combine(_webHostEnvironment.ContentRootPath, "Uploads", file.StoredFileName);

        using FileStream fileStream = new(path, FileMode.Open);
        
        return new PhysicalFileResult(path , file.ContentType){
            FileDownloadName = file.FileName
        };
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