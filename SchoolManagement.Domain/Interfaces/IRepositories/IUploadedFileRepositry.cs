using System;
using SchoolManagement.Domain.Entities;

namespace SchoolManagement.Domain.Interfaces.IRepositories;

public interface IUploadedFileRepositry : IGenericRepository<UploadedFile>
{
    Task<List<UploadedFile>> GetAllFiles();
    Task<UploadedFile> GetFileByName(string fileUrl);
    Task AddFile(UploadedFile UploadedFile);
    Task DeleteByFileUrl(string fileUrl);
    bool IsFileExists(string FileName);
}
