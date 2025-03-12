using System;
using SchoolManagement.Domain.Entities;
using SchoolManagement.Domain.Interfaces.IRepositories;
using SchoolManagement.Infrastructure.DbContext;

namespace SchoolManagement.Infrastructure.Repositories;

public class CommentRepositry : GenericRepository<Comment , int>, ICommentRepositry
{
    public CommentRepositry(AppDbContext appDbContext) : base(appDbContext)
    {
    }
}
