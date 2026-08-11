using Accounting.Dao.Entities;

namespace Accounting.Dao.Interfaces
{
    public interface IJournalEntryDao
    {
        Task<bool> VoucherNoExistsAsync(string voucherNo, CancellationToken ct);
        Task<Guid> AddAsync(JournalEntry entity, CancellationToken ct);
        Task<bool> DeleteAsync(Guid id, CancellationToken ct);
    }
}