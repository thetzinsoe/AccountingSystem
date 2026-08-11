using Accounting.Service.DTOs.Requests;
using Accounting.Service.DTOs.Responses;

namespace Accounting.Service.Interfaces
{
    public interface IJournalEntryService
    {
        Task<JournalEntryResponse> CreateAsync(CreateJournalEntryRequest request, CancellationToken ct);
        Task DeleteJournalEntryAsync(Guid id, CancellationToken ct);
    }
}