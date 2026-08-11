using Accounting.Dao.Entities;

namespace Accounting.Dao.Interfaces
{
    public interface IAccountDao
    {
        Task<IReadOnlyDictionary<Guid, AccountInfo>> GetByIdsAsync(IEnumerable<Guid> accountIds, CancellationToken ct);
    }
}