using System;
using Microsoft.EntityFrameworkCore;
using SchoolManagement.Domain.Entities;
using SchoolManagement.Domain.Interfaces.IRepositories;
using SchoolManagement.Infrastructure.DbContext;

namespace SchoolManagement.Infrastructure.Repositories;

public class CommentRepositry : GenericRepository<Comment>, ICommentRepositry
{
    public CommentRepositry(AppDbContext appDbContext) : base(appDbContext)
    {
    }

    public async Task<List<Comment>> GetByPostId(int postId, int page, int pageSize){
        return await _appDbContext.Comments
            .Where(comment => comment.PostId == postId)
            .Include(comment => comment.Author)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }

    public async Task<int> GetByPostIdTotalCount(int postId)
    {
        return await _appDbContext.Comments
            .Where(comment => comment.PostId == postId)
            .CountAsync();
    }
}
