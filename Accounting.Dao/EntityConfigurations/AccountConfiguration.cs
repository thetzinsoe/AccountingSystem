using Accounting.Dao.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Accounting.Dao.EntityConfigurations
{
    public class AccountConfiguration : IEntityTypeConfiguration<Account>
    {
        public void Configure(EntityTypeBuilder<Account> builder)
        {
            builder.HasKey(e => e.AccountId);
            builder.Property(e => e.AccountCode).IsRequired().HasMaxLength(20);
            builder.Property(e => e.AccountName).IsRequired().HasMaxLength(100);
            builder.Property(e => e.AccountType).HasConversion<string>().IsRequired().HasMaxLength(20);
            builder.Property(e => e.CreatedAt).IsRequired().HasDefaultValueSql("now()");

            builder.HasIndex(e => e.AccountCode).IsUnique();
        }
    }
}
