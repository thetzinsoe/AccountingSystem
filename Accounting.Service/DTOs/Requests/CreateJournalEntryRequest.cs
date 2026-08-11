using Accounting.Dao.Entities;

namespace Accounting.Service.DTOs.Requests
{
    public class CreateJournalEntryRequest
    {
        public Guid JournalEntryId { get; set; }
        public string VoucherNo { get; set; } = string.Empty;
        public DateTime TransactionDate { get; set; }
        public string Description { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public virtual ICollection<JournalEntryLine> JournalEntryLines { get; set; } = new List<JournalEntryLine>();
    }
}
