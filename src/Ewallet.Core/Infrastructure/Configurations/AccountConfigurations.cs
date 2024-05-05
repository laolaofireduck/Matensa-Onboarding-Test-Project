using Ewallet.Core.Domain.Accounts;
using Ewallet.Core.Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ewallet.Core.Infrastructure.Configurations;

internal class AccountConfigurations : IEntityTypeConfiguration<Account>
{
    public void Configure(EntityTypeBuilder<Account> builder)
    {
        builder.ToTable("Accounts");
        builder
            .HasKey(a => a.Id);

        builder
            .Property(u => u.Id)
            .ValueGeneratedNever()
            .HasConversion(
                id => id.Value,
                value => AccountId.Create(value));

        builder
            .Property(a => a.UserId)
            .HasConversion(
                id => id.Value,
                value => UserId.Create(value));

        // statements
        builder.OwnsMany(a => a.Statements, statementBuilder =>
        {
            statementBuilder.ToTable("AccountStatements");
            statementBuilder
            .WithOwner()
            .HasForeignKey("AccountId");

            
        });
        builder.Metadata
            .FindNavigation(nameof(Account.Statements))!
            .SetPropertyAccessMode(PropertyAccessMode.Field);

        // transactions
        builder.OwnsMany(a => a.Transactions, statementBuilder =>
        {
            statementBuilder.ToTable("AccountTransactions");
            statementBuilder
            .WithOwner()
            .HasForeignKey("AccountId");

        });
        builder.Metadata
            .FindNavigation(nameof(Account.Transactions))!
            .SetPropertyAccessMode(PropertyAccessMode.Field);

    }
}
