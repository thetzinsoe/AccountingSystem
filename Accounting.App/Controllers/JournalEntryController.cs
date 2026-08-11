using Accounting.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Accounting.App.Controllers
{
    [ApiController]
    public class JournalEntryController : ControllerBase
    {
        private readonly IJournalEntryService _journalEntryService;
        private readonly Logger<JournalEntryController> _logger;
        public JournalEntryController
        (
            IJournalEntryService journalEntryService,
            Logger<JournalEntryController> logger
        )
        {
            _journalEntryService = journalEntryService;
            _logger = logger;
        }
    }
}
