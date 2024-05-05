using Ewallet.SharedKernel;
using Microsoft.EntityFrameworkCore;

namespace Ewallet.Core.Infrastructure.Repositories;
public abstract class RepositoryBase<T, TId, TIdType> : IRepository<T, TId, TIdType>
    where T : AggregateRoot<TId, TIdType>
    where TId : AggregateRootId<TIdType>

{
    private readonly EwalletDbContext context;
    protected DbSet<T> dbSet;

    public RepositoryBase(EwalletDbContext context)
    {
        this.context = context;
        dbSet = context.Set<T>();


    }
    public async Task<T> Add(T entity)
    {
        dbSet.Add(entity);
        await context.SaveChangesAsync();
        return entity;
    }

    public async Task<T> Delete(TId id)
    {
        var entity = await dbSet.FindAsync(id);
        if (entity == null)
        {
            return entity;
        }
        if (entity is ISoftDeletable softDeletableEntity)
            softDeletableEntity.Delete();
        else
            dbSet.Remove(entity);

        await context.SaveChangesAsync();

        return entity;
    }

    public async Task<T> Get(TId id)
    {
        return await dbSet.FindAsync(id);
    }

    public async Task<IEnumerable<T>> GetAll(int? skip, int? take)
    {
        IQueryable<T> usersQuery = dbSet;

        if (skip.HasValue)
            usersQuery = usersQuery.Skip(skip.Value);

        if (take.HasValue)
            usersQuery = usersQuery.Take(take.Value);

        return await usersQuery.ToListAsync();
    }

    public async Task<T> Update(T entity)
    {
        context.Entry(entity).State = EntityState.Modified;
        await context.SaveChangesAsync();
        return entity;
    }

}

