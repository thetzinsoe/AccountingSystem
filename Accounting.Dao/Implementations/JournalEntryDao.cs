using Accounting.Dao.Context;
using Accounting.Dao.Entities;
using Accounting.Dao.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Accounting.Dao.Implementations
{
    public class JournalEntryDao : IJournalEntryDao
    {
        private readonly AppDbContext _context;

        public JournalEntryDao(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> VoucherNoExistsAsync(string voucherNo, CancellationToken ct)
        {
            return await _context.JournalEntries
                .AnyAsync(e => e.VoucherNo == voucherNo, ct);
        }

        public async Task<Guid> AddAsync(JournalEntry entity, CancellationToken ct)
        {
            _context.JournalEntries.Add(entity);
            await _context.SaveChangesAsync(ct);
            return entity.JournalEntryId;
        }

        public async Task<bool> DeleteAsync(Guid id, CancellationToken ct)
        {
            var entity = await _context.JournalEntries
                .FirstOrDefaultAsync(e => e.JournalEntryId == id && !e.IsDeleted, ct);

            if (entity == null)
            {
                return false;
            }

            entity.IsDeleted = true;
            entity.UpdatedAt = DateTime.UtcNow;
            await _context.SaveChangesAsync(ct);

            return true;
        }
    }
}