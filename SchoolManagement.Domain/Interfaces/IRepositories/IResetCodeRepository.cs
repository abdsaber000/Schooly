using System.Threading;
using System.Threading.Tasks;
using SchoolManagement.Domain.Entities;

namespace SchoolManagement.Domain.Interfaces.IRepositories
{
    public interface IResetCodeRepository
    {
        Task AddResetCodeAsync(ResetCode resetCode, CancellationToken cancellationToken = default);
        Task<ResetCode?> GetResetCodeByEmailAsync(string email, CancellationToken cancellationToken = default);
        Task RemoveResetCodeAsync(ResetCode resetCode, CancellationToken cancellationToken = default);
        Task SaveChangesAsync(CancellationToken cancellationToken = default);
        Task RemoveExpiredResetCodesAsync(CancellationToken cancellationToken = default);
    }
}