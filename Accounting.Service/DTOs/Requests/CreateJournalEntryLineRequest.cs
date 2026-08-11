namespace Accounting.Service.DTOs.Requests
{
    public class CreateJournalEntryLineRequest
    {
        public Guid AccountId { get; set; }
        public decimal DebitAmount { get; set; }
        public decimal CreditAmount { get; set; }
    }
}