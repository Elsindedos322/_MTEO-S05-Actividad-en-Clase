using Microsoft.EntityFrameworkCore;
using Caso04actividadclase.interfaces;
using Caso04actividadclase.Models;

namespace Caso04actividadclase.Repositories;

public class GenericRepository<T> : IGenericRepository<T> where T : class
{
    protected readonly AgenciaDbContext _context;

    public GenericRepository(AgenciaDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<T>> GetAllAsync() 
        => await _context.Set<T>().ToListAsync();

    public async Task<T?> GetByIdAsync(int id) 
        => await _context.Set<T>().FindAsync(id);

    public async Task AddAsync(T entity) 
        => await _context.Set<T>().AddAsync(entity);

    public void Update(T entity) 
        => _context.Set<T>().Update(entity);

    public void Delete(T entity) 
        => _context.Set<T>().Remove(entity);
}