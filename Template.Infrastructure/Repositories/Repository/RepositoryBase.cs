using Microsoft.EntityFrameworkCore;
using Template.Infrastructure.DataBaseHelper;
using Template.Infrastructure.Repositories.Interfaces;

namespace Template.Infrastructure.Repositories.Repository;

public class RepositoryBase<TModel> : IRepositoryBase<TModel> where TModel : class
{
    protected readonly ApplicationDbContext _context;
    protected readonly DbSet<TModel> _dbSet;

    public RepositoryBase(ApplicationDbContext context)
    {
        _context = context;
        _dbSet = _context.Set<TModel>();
    }

    public virtual async Task AddAsync(TModel entity)
    {
        await _dbSet.AddAsync(entity);
        await _context.SaveChangesAsync();
    }

    public virtual async Task<IEnumerable<TModel>> LoadAsync()
        => _dbSet;

    public virtual async Task<TModel> GetByIdAsync(object id)
        => await _dbSet.FindAsync(id);

    public virtual async Task UpdateAsync(TModel entity)
    {
        _dbSet.Update(entity);
        await _context.SaveChangesAsync();
    }

    public virtual async Task RemoveAsync(TModel entity)
    {
        _dbSet.Remove(entity);
        await _context.SaveChangesAsync();
    }

    public virtual async Task RemoveById(object id)
    {
        _dbSet.Remove(_dbSet.Find(id)!);
        await _context.SaveChangesAsync();
    }
}
