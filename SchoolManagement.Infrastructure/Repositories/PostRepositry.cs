using System;
using Microsoft.EntityFrameworkCore;
using SchoolManagement.Domain.Entities;
using SchoolManagement.Domain.Interfaces.IRepositories;
using SchoolManagement.Infrastructure.DbContext;

namespace SchoolManagement.Infrastructure.Repositories;

public class PostRepositry : IPostRepositry
{
    private readonly AppDbContext _context;
    public PostRepositry(AppDbContext context)
    {
        _context = context;
    }
    public async Task<Post> CreatePost(Post post)
    {
        await _context.Posts.AddAsync(post);
        await _context.SaveChangesAsync();
        return post;
    }

    public async Task DeletePost(int id)
    {
        var post = await _context.Posts.FirstOrDefaultAsync(p => p.Id == id);
        if(post != null){
            _context.Posts.Remove(post);
            await _context.SaveChangesAsync();
        }
    }

    public async Task DeletePost(Post post)
    {
        _context.Posts.Remove(post);
        await _context.SaveChangesAsync();
    }

    public async Task<List<Post>> GetAllPosts()
    {
        return await _context.Posts
                .Include(post => post.Comments)
                .ThenInclude(comment => comment.Author)
                .Include(post => post.Author)
                .Include(post => post.ClassRoom)
                .ToListAsync();
    }

    public async Task<List<Post>> GetPagedAsync(int page, int pageSize)
    {
        return await _context.Posts
                    .OrderByDescending(post => post.CreatedAt)
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .Include(post => post.Comments)
                    .ThenInclude(comment => comment.Author)
                    .Include(post => post.Author)
                    .Include(post => post.ClassRoom)
                    .ToListAsync();
    }

    public async Task<Post> GetPostById(int id)
    {
        return await _context.Posts
            .Where(post => post.Id == id)
            .Include(post => post.Comments)
            .ThenInclude(comment => comment.Author)
            .Include(post => post.Author)
            .Include(post => post.ClassRoom)
            .FirstOrDefaultAsync();
    }

    public async Task<List<Post>> GetPostsByAuthor(string authorId)
    {
        return await _context.Posts
            .Where(post => post.AuthorId == authorId)
            .Include(post => post.Comments)
            .ThenInclude(comment => comment.Author)
            .Include(post => post.Author)
            .Include(post => post.ClassRoom)
            .ToListAsync();
    }

    public async Task<List<Post>> GetPostsByAuthorPagedAsync(string authorId , int page, int pageSize)
    {
        return await _context.Posts
                        .Where(post => post.AuthorId == authorId)
                        .OrderByDescending(post => post.CreatedAt)
                        .Skip((page - 1) * pageSize)
                        .Take(pageSize)
                        .Include(post => post.Comments)
                        .ThenInclude(comment => comment.Author)
                        .Include(post => post.Author)
                        .Include(post => post.ClassRoom)
                        .ToListAsync();
    }

    public async Task<int> GetTotalPostsCount()
    {
        return await _context.Posts.CountAsync();
    }

    public async Task<Post> UpdatePost(Post post)
    {
        _context.Posts.Update(post);
        await _context.SaveChangesAsync();
        return post;
    }

}
