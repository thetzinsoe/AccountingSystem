using Accounting.Dao.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Accounting.Dao.EntityConfigurations
{
    public class JournalEntryLineConfiguration : IEntityTypeConfiguration<JournalEntryLine>
    {
        public void Configure(EntityTypeBuilder<JournalEntryLine> builder)
        {
            builder.HasKey(e => e.JournalEntryLineId);
            builder.Property(e => e.JournalEntryId).IsRequired();
            builder.Property(e => e.AccountId).IsRequired();
            builder.Property(e => e.DebitAmount).IsRequired().HasPrecision(18, 2);
            builder.Property(e => e.CreditAmount).IsRequired().HasPrecision(18, 2);
            builder.Property(e => e.CreatedAt).IsRequired().HasDefaultValueSql("now()");

            builder.HasOne(e => e.JournalEntry)
                .WithMany(j => j.JournalEntryLines)
                .HasForeignKey(e => e.JournalEntryId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(e => e.Account)
                .WithMany()
                .HasForeignKey(e => e.AccountId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(e => e.JournalEntryId);
            builder.HasIndex(e => e.AccountId);

            builder.ToTable(t => t.HasCheckConstraint(
                "CK_JournalEntryLines_DebitOrCredit",
                "(debit_amount > 0 AND credit_amount = 0) OR (debit_amount = 0 AND credit_amount > 0)"));
        }
    }
}
