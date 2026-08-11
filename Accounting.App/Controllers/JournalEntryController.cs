using Accounting.Service.DTOs.Requests;
using Accounting.Service.DTOs.Responses;
using Accounting.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Accounting.App.Controllers
{
    [ApiController]
    [Route("api/journal-entries")]
    public class JournalEntryController : ControllerBase
    {
        private readonly IJournalEntryService _journalEntryService;
        private readonly ILogger<JournalEntryController> _logger;

        public JournalEntryController(
            IJournalEntryService journalEntryService,
            ILogger<JournalEntryController> logger)
        {
            _journalEntryService = journalEntryService;
            _logger = logger;
        }

        [HttpPost]
        [ProducesResponseType(typeof(JournalEntryResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create(
            [FromBody] CreateJournalEntryRequest request,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation("Creating journal entry with voucher no {VoucherNo}", request.VoucherNo);

            var response = await _journalEntryService.CreateAsync(request, cancellationToken);

            return Created($"/api/journal-entries/{response.JournalEntryId}", response);
        }

        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Soft-deleting journal entry {JournalEntryId}", id);

            await _journalEntryService.DeleteJournalEntryAsync(id, cancellationToken);

            return NoContent();
        }
    }
}