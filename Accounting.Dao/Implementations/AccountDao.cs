using Accounting.Dao.Context;
using Accounting.Dao.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Accounting.Dao.Implementations
{
    public class AccountDao : IAccountDao
    {
        private readonly AppDbContext _context;

        public AccountDao(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IReadOnlyDictionary<Guid, AccountInfo>> GetByIdsAsync(
            IEnumerable<Guid> accountIds,
            CancellationToken ct)
        {
            var ids = accountIds.Distinct().ToList();
            if (ids.Count == 0)
            {
                return new Dictionary<Guid, AccountInfo>();
            }

            var accounts = await _context.Accounts
                .Where(a => ids.Contains(a.AccountId))
                .Select(a => new AccountInfo(a.AccountId, a.AccountCode, a.AccountName))
                .ToListAsync(ct);

            return accounts.ToDictionary(a => a.AccountId);
        }
    }
}