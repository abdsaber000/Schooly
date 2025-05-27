using System;
using SchoolManagement.Domain.Entities;

namespace SchoolManagement.Domain.Interfaces.IRepositories;

public interface ICommentRepositry : IGenericRepository<Comment>
{
	Task<List<Comment>> GetByPostId(int postId, int page, int pageSize);
	Task<int> GetByPostIdTotalCount(int postId);
}
