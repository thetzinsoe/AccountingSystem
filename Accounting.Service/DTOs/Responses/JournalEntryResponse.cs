namespace Accounting.Service.DTOs.Responses
{
    public class JournalEntryResponse
    {
        public Guid JournalEntryId { get; set; }
        public string VoucherNo { get; set; } = string.Empty;
        public DateTime TransactionDate { get; set; }
        public string Description { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public List<JournalEntryLineResponse> Lines { get; set; } = new();
    }

    public static class JournalEntryMappingExtension
    {
        public static JournalEntryResponse ToResponse(this Accounting.Dao.Entities.JournalEntry entity)
        {
            if (entity == null) return null!;

            return new JournalEntryResponse
            {
                JournalEntryId = entity.JournalEntryId,
                VoucherNo = entity.VoucherNo,
                TransactionDate = entity.TransactionDate,
                Description = entity.Description,
                CreatedAt = entity.CreatedAt,
                UpdatedAt = entity.UpdatedAt,
                Lines = entity.JournalEntryLines?.Select(l => new JournalEntryLineResponse
                {
                    JournalEntryLineId = l.JournalEntryLineId,
                    AccountId = l.AccountId,
                    DebitAmount = l.DebitAmount,
                    CreditAmount = l.CreditAmount
                }).ToList() ?? new()
            };
        }
    }
}