using System;
using SchoolManagement.Domain.Entities;
using SchoolManagement.Domain.Interfaces.Repositories;

namespace SchoolManagement.Domain.Interfaces.IRepositories;

public interface IUploadedFileRepositry : IGenericRepository
{
    Task<List<UploadedFile>> GetAllFiles();
    Task<UploadedFile> GetFileByName(string FileName);
    Task AddFile(UploadedFile UploadedFile);
    bool IsFileExists(string FileName);
}
