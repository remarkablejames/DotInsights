using DotInsights.API.Contracts;
using DotInsights.API.Data;
using Microsoft.EntityFrameworkCore;

namespace DotInsights.API.Repository;

public class GenericRepository<T> : IGenericRepository<T> where T : class
{
    private readonly BlogDbContext _context;

    public GenericRepository(BlogDbContext context)
    {
        _context = context;
    }
    public async Task<T?> GetAsync(int? id)
    {
        if (id is null)
        {
            return null;
        }
        else
        {
            return await _context.Set<T>().FindAsync(id);
        }
    }

    public async Task<IList<T>> GetAllAsync()
    {
        return await  _context.Set<T>().ToListAsync();
    }

    public async Task<T> AddAsync(T entity)
    { 
        await _context.AddAsync(entity);
        await _context.SaveChangesAsync();
        return entity;

    }

    public async Task DeleteAsync(int id)
    {
        var entity = await GetAsync(id);
        _context.Remove(entity);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(T entity)
    {
        _context.Update(entity);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> Exists(int id)
    {
        var entity = await GetAsync(id);
        return entity != null;
    }
}