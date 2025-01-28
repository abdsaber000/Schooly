using Microsoft.EntityFrameworkCore;
using SchoolManagement.Domain.Interfaces.IRepositories;
using SchoolManagement.Infrastructure.DbContext;

namespace SchoolManagement.Infrastructure.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly AppDbContext _appDbContext;

        public GenericRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task<int> GetTotalCountAsync(CancellationToken cancellationToken = default)
        {
            return await _appDbContext.Set<T>().CountAsync(cancellationToken);
        }
        public async Task<List<T>> GetPagedAsync(int page, int pageSize, CancellationToken cancellationToken = default)
        {
            return await _appDbContext.Set<T>()
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(cancellationToken);
        }
    }

}
