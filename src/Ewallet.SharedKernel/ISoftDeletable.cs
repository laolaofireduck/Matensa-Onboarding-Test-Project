using System.Reflection.Emit;
using System.Reflection;
using Microsoft.EntityFrameworkCore;

namespace Ewallet.SharedKernel;

public interface ISoftDeletable
{
    bool IsDeleted { get; }
    abstract void Delete();
}

public static class Entity
{
    public static void UseSoftDelete(ModelBuilder modelBuilder)
    {
        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            if (typeof(ISoftDeletable).IsAssignableFrom(entityType.ClrType))
            {
                var method = typeof(Entity).GetMethod(nameof(SetSoftDelete), BindingFlags.Static | BindingFlags.NonPublic);
                var genericMethod = method.MakeGenericMethod(entityType.ClrType);
                genericMethod.Invoke(null, new object[] { modelBuilder });
            }
        }
    }

    private static void SetSoftDelete<TEntity>(ModelBuilder modelBuilder) where TEntity : class, ISoftDeletable
    {
        modelBuilder.Entity<TEntity>().HasQueryFilter(e => !e.IsDeleted);
    }
}
