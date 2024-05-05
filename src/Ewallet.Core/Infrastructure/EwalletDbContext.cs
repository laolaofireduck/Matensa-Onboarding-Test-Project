using Ewallet.SharedKernel;
using Microsoft.EntityFrameworkCore;
using Ewallet.Core.Infrastructure.Interceptors;
using Ewallet.Core.Domain.Users;
using Ewallet.Core.Domain.Accounts;

namespace Ewallet.Core.Infrastructure;

public sealed class EwalletDbContext : DbContext
{
    private readonly PublishDomainEventsInterceptor _publishDomainEventsInterceptor;

    public EwalletDbContext(
        DbContextOptions<EwalletDbContext> options,
        PublishDomainEventsInterceptor publishDomainEventsInterceptor
    ) : base(options)
    {
        _publishDomainEventsInterceptor = publishDomainEventsInterceptor;
    }

    public DbSet<User> Users { get; set; } = null!;
    public DbSet<Account> Accounts { get; set; } = null!;
    public DbSet<AccountStatement> AccountStatements { get; set; } = null!;
    public DbSet<AccountTransaction> AccountTransactions { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .Ignore<List<IDomainEvent>>()
            .ApplyConfigurationsFromAssembly(typeof(EwalletDbContext).Assembly);

        base.OnModelCreating(modelBuilder);

        Entity.UseSoftDelete(modelBuilder);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder
            .AddInterceptors(_publishDomainEventsInterceptor);

        base.OnConfiguring(optionsBuilder);
    }
}