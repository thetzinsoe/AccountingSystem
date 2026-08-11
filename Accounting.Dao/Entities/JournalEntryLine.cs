namespace Accounting.Dao.Entities
{
    public class JournalEntryLine
    {
        public Guid JournalEntryLineId { get; set; }
        public Guid JournalEntryId { get; set; }
        public Guid AccountId { get; set; }
        public decimal DebitAmount { get; set; }
        public decimal CreditAmount { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public virtual JournalEntry JournalEntry { get; set; } = null!;
        public virtual Account Account { get; set; } = null!;
    }
}
