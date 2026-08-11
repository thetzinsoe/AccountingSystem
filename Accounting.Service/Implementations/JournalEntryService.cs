using Accounting.Dao.Entities;
using Accounting.Dao.Interfaces;
using Accounting.Service.DTOs.Requests;
using Accounting.Service.DTOs.Responses;
using Accounting.Service.Exceptions;
using Accounting.Service.Interfaces;
using FluentValidation;

namespace Accounting.Service.Implementations
{
    public class JournalEntryService : IJournalEntryService
    {
        private readonly IJournalEntryDao _journalEntryDao;
        private readonly IAccountDao _accountDao;
        private readonly IValidator<CreateJournalEntryRequest> _validator;

        public JournalEntryService(
            IJournalEntryDao journalEntryDao,
            IAccountDao accountDao,
            IValidator<CreateJournalEntryRequest> validator)
        {
            _journalEntryDao = journalEntryDao;
            _accountDao = accountDao;
            _validator = validator;
        }

        public async Task<JournalEntryResponse> CreateAsync(CreateJournalEntryRequest request, CancellationToken ct)
        {
            var validationResult = await _validator.ValidateAsync(request, ct);
            if (!validationResult.IsValid)
            {
                throw new BadRequestException(
                    string.Join("; ", validationResult.Errors.Select(e => e.ErrorMessage)));
            }

            var accountIds = request.Lines.Select(l => l.AccountId).ToList();
            var accounts = await _accountDao.GetByIdsAsync(accountIds, ct);
            var missing = accountIds.Where(id => !accounts.ContainsKey(id)).Distinct().ToList();
            if (missing.Count > 0)
            {
                throw new BadRequestException($"Account(s) not found: {string.Join(", ", missing)}");
            }

            var totalDebit = request.Lines.Sum(l => l.DebitAmount);
            var totalCredit = request.Lines.Sum(l => l.CreditAmount);
            if (totalDebit != totalCredit)
            {
                throw new BadRequestException(
                    $"Journal entry is not balanced. Total debits: {totalDebit}, total credits: {totalCredit}");
            }

            if (await _journalEntryDao.VoucherNoExistsAsync(request.VoucherNo, ct))
            {
                throw new BadRequestException($"Voucher number '{request.VoucherNo}' already exists.");
            }

            var journalEntryId = Guid.NewGuid();
            var now = DateTime.UtcNow;
            var transactionDate = DateTime.SpecifyKind(request.TransactionDate, DateTimeKind.Utc);

            var journalEntry = new JournalEntry
            {
                JournalEntryId = journalEntryId,
                VoucherNo = request.VoucherNo,
                TransactionDate = transactionDate,
                Description = request.Description,
                CreatedAt = now,
                JournalEntryLines = request.Lines.Select(l => new JournalEntryLine
                {
                    JournalEntryLineId = Guid.NewGuid(),
                    JournalEntryId = journalEntryId,
                    AccountId = l.AccountId,
                    DebitAmount = l.DebitAmount,
                    CreditAmount = l.CreditAmount,
                    CreatedAt = now
                }).ToList()
            };

            await _journalEntryDao.AddAsync(journalEntry, ct);

            return new JournalEntryResponse
            {
                JournalEntryId = journalEntryId,
                VoucherNo = journalEntry.VoucherNo,
                TransactionDate = journalEntry.TransactionDate,
                Description = journalEntry.Description,
                CreatedAt = journalEntry.CreatedAt,
                UpdatedAt = journalEntry.UpdatedAt,
                Lines = journalEntry.JournalEntryLines.Select(l => new JournalEntryLineResponse
                {
                    JournalEntryLineId = l.JournalEntryLineId,
                    AccountId = l.AccountId,
                    AccountCode = accounts[l.AccountId].AccountCode,
                    AccountName = accounts[l.AccountId].AccountName,
                    DebitAmount = l.DebitAmount,
                    CreditAmount = l.CreditAmount
                }).ToList()
            };
        }

        public async Task DeleteJournalEntryAsync(Guid id, CancellationToken ct)
        {
            if (!await _journalEntryDao.DeleteAsync(id, ct))
            {
                throw new NotFoundException($"Journal entry with ID '{id}' was not found.");
            }
        }
    }
}