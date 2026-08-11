using Accounting.Service.DTOs.Requests;
using FluentValidation;

namespace Accounting.Service.Validators.JournalEntry
{
    public class JournalEntryRequestValidator : AbstractValidator<CreateJournalEntryRequest>
    {
        public JournalEntryRequestValidator(IValidator<CreateJournalEntryLineRequest> lineValidator)
        {
            RuleFor(x => x.VoucherNo)
                .NotEmpty()
                .MaximumLength(50);

            RuleFor(x => x.TransactionDate)
                .NotEmpty()
                .LessThanOrEqualTo(DateTime.UtcNow.AddDays(1));

            RuleFor(x => x.Description)
                .MaximumLength(500);

            RuleFor(x => x.Lines)
                .NotEmpty();

            RuleForEach(x => x.Lines)
                .NotNull()
                .SetValidator(lineValidator);
        }
    }
}