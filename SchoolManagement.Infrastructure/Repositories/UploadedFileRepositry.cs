using System;
using Microsoft.EntityFrameworkCore;
using SchoolManagement.Domain.Entities;
using SchoolManagement.Domain.Interfaces.IRepositories;
using SchoolManagement.Infrastructure.DbContext;

namespace SchoolManagement.Infrastructure.Repositories;

public class UploadedFileRepositry : GenericRepository<UploadedFile> , IUploadedFileRepositry
{
    private readonly AppDbContext _appDbContext;
    public UploadedFileRepositry(AppDbContext appDbContext) : base(appDbContext){
        _appDbContext = appDbContext;
    }
    public async Task AddFile(UploadedFile UploadedFile)
    {
        await _appDbContext.UploadedFiles.AddAsync(UploadedFile);
        await _appDbContext.SaveChangesAsync();
    }

    public async Task DeleteByFileUrl(string fileUrl)
    {
        var uploadeFile = await _appDbContext.UploadedFiles
            .FirstOrDefaultAsync(u => u.StoredFileName == fileUrl);
          _appDbContext.UploadedFiles.Remove(uploadeFile);
          await _appDbContext.SaveChangesAsync();
    }

    public async Task<List<UploadedFile>> GetAllFiles()
    {
        return await _appDbContext.UploadedFiles.ToListAsync();
    }

    public async Task<UploadedFile> GetFileByName(string fileUrl)
    {
        return await _appDbContext.UploadedFiles
            .FirstOrDefaultAsync(file => file.StoredFileName == fileUrl);
    }

    public bool IsFileExists(string FileName)
    {
        return _appDbContext.UploadedFiles.Any(file => file.FileName == FileName);
    }
}
