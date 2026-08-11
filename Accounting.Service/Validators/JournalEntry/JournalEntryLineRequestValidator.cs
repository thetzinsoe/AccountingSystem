using Accounting.Service.DTOs.Requests;
using FluentValidation;

namespace Accounting.Service.Validators.JournalEntry
{
    public class JournalEntryLineRequestValidator : AbstractValidator<CreateJournalEntryLineRequest>
    {
        public JournalEntryLineRequestValidator()
        {
            RuleFor(x => x.AccountId)
                .NotEmpty();

            RuleFor(x => x.DebitAmount)
                .GreaterThanOrEqualTo(0);

            RuleFor(x => x.CreditAmount)
                .GreaterThanOrEqualTo(0);

            RuleFor(x => x)
                .Must(line => (line.DebitAmount > 0) ^ (line.CreditAmount > 0))
                .WithMessage("A line must have either a debit or a credit amount, not both and not neither.");
        }
    }
}