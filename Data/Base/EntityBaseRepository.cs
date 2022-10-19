using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
namespace Project.Data.Base;

public class EntityBaseRepository <T>: IEntityBaseRepository<T> where T: class, IEntityBase, new()
{
    private readonly AppDbContext ctx;

    public EntityBaseRepository(AppDbContext context)
    {
        ctx = context;
    }

    public async Task<IEnumerable<T>> GetAllAsync() => await ctx.Set<T>().ToListAsync();

    public async Task<T> GetByIdAsync(int Id) => await ctx.Set<T>().FirstOrDefaultAsync(n => n.Id == Id);

    public async Task AddAsync(T entity) 
    {
        await ctx.Set<T>().AddAsync(entity);
        await ctx.SaveChangesAsync();
    } 

    public async Task UpdateAsync(int id, T entity)
    {
        EntityEntry entityentry = ctx.Entry<T>(entity);
        entityentry.State = EntityState.Modified;
        await ctx.SaveChangesAsync();

    }

    public async Task DeleteAsync(int Id)
    {
        var entity = await ctx.Set<T>().FirstOrDefaultAsync(n => n.Id == Id);
        EntityEntry entityentry = ctx.Entry<T>(entity);
        entityentry.State = EntityState.Deleted;
        await ctx.SaveChangesAsync();
    }

    public async Task<IEnumerable<T>> GetAllAsync(params Expression<Func<T, object>>[] includeProperties)
    {
        IQueryable<T> query = ctx.Set<T>();
        query = includeProperties.Aggregate(query, (ctx, includeProperty) => ctx.Include(includeProperty));
        return await query.ToListAsync();
    }
}