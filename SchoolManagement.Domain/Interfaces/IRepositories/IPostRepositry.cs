using System;
using SchoolManagement.Domain.Entities;

namespace SchoolManagement.Domain.Interfaces.IRepositories;

public interface IPostRepositry
{
    Task<List<Post>> GetAllPosts();
    Task<List<Post>> GetPagedAsync(int page, int pageSize);
    Task<Post> GetPostById(int id);
    Task<Post> CreatePost(Post post);
    Task<Post> UpdatePost(Post post);
    Task DeletePost(Post post);
    Task<List<Post>> GetPostsByAuthor(string authorId);
    Task<List<Post>> GetPostsByAuthorPagedAsync(string authorId , int page, int pageSize);
    Task<int> GetTotalPostsCount();
}
