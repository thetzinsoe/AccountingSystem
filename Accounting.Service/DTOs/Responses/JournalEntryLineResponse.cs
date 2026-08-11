namespace Accounting.Service.DTOs.Responses
{
    public class JournalEntryLineResponse
    {
        public Guid JournalEntryLineId { get; set; }
        public Guid AccountId { get; set; }
        public string AccountCode { get; set; } = string.Empty;
        public string AccountName { get; set; } = string.Empty;
        public decimal DebitAmount { get; set; }
        public decimal CreditAmount { get; set; }
    }
}