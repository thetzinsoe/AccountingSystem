using Accounting.Dao.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Accounting.Dao.EntityConfigurations
{
    public class JournalEntryConfiguration : IEntityTypeConfiguration<JournalEntry>
    {
        public void Configure(EntityTypeBuilder<JournalEntry> builder)
        {
            builder.HasKey(e => e.JournalEntryId);
            builder.Property(e => e.VoucherNo).IsRequired().HasMaxLength(50);
            builder.Property(e => e.TransactionDate).IsRequired();
            builder.Property(e => e.Description).IsRequired().HasColumnType("text");
            builder.Property(e => e.CreatedAt).IsRequired().HasDefaultValueSql("now()");

            builder.HasIndex(e => e.VoucherNo).IsUnique();
        }
    }
}
