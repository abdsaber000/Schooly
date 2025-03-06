using Microsoft.EntityFrameworkCore;
using SchoolManagement.Domain.Entities;
using SchoolManagement.Domain.Interfaces.IRepositories;
using SchoolManagement.Infrastructure.DbContext;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SchoolManagement.Infrastructure.Repositories
{
    public class ResetCodeRepository : IResetCodeRepository
    {
        private readonly AppDbContext _appDbContext;

        public ResetCodeRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task AddResetCodeAsync(ResetCode resetCode, CancellationToken cancellationToken)
        {
            await _appDbContext.ResetCodes.AddAsync(resetCode, cancellationToken);
            await _appDbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task<ResetCode?> GetResetCodeByEmailAsync(string email, CancellationToken cancellationToken = default)
        {
            return await _appDbContext.ResetCodes
                .FirstOrDefaultAsync(r => r.Email == email, cancellationToken);
        }

        public async Task RemoveResetCodeAsync(ResetCode resetCode, CancellationToken cancellationToken = default)
        {
            _appDbContext.ResetCodes.Remove(resetCode);
            await _appDbContext.SaveChangesAsync(cancellationToken); // Ensure changes are saved
        }

        public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            await _appDbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task RemoveExpiredResetCodesAsync(CancellationToken cancellationToken = default)
        {
            var expiredCodes = await _appDbContext.ResetCodes
                .Where(r => r.ExpirationTime < DateTime.UtcNow)
                .ToListAsync(cancellationToken);

            _appDbContext.ResetCodes.RemoveRange(expiredCodes);
            await _appDbContext.SaveChangesAsync(cancellationToken);
        }
    }
}