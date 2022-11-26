using HomeworkDb1.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace HomeworkDb1.Domain.Repos;

public class EfRepository<T> : IRepository<T> where T : BaseEntity
{
    private readonly DbContext _dataContext;
    public EfRepository(DbContext dataContext)
    {
        _dataContext = dataContext;
    }
    
    public async Task<IEnumerable<T>> GetAllAsync()
    {
        return await _dataContext.Set<T>().ToListAsync();
    }

    public async Task<T?> GetAsync(Guid id)
    {
        return await _dataContext.Set<T>().FirstOrDefaultAsync(e => e.Id == id);
    }

    public async Task AddAsync(T entity)
    {
        await _dataContext.Set<T>().AddAsync(entity);
        await _dataContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(T entity)
    {
        await _dataContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var entity = await _dataContext.Set<T>().FirstOrDefaultAsync(e => e.Id == id);
        if (entity != null)
        {
            _dataContext.Set<T>().Remove(entity);
            await _dataContext.SaveChangesAsync();
        }
    }
}