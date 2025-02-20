using System;
using SchoolManagement.Domain.Entities;

namespace SchoolManagement.Domain.Interfaces.IRepositories;

public interface IUploadedFileRepositry 
{
    Task<List<UploadedFile>> GetAllFiles();
    Task<UploadedFile> GetFileByName(string FileName);
    Task AddFile(UploadedFile UploadedFile);
    bool IsFileExists(string FileName);
}
