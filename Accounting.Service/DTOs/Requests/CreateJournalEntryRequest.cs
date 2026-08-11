namespace Accounting.Service.DTOs.Requests
{
    public class CreateJournalEntryRequest
    {
        public string VoucherNo { get; set; } = string.Empty;
        public DateTime TransactionDate { get; set; }
        public string Description { get; set; } = string.Empty;
        public List<CreateJournalEntryLineRequest> Lines { get; set; } = new();
    }
}