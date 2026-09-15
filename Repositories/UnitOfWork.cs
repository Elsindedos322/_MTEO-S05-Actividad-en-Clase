using System.Collections;
using Caso04actividadclase.interfaces;
using Caso04actividadclase.Models;

namespace Caso04actividadclase.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly AgenciaDbContext _context;
    private Hashtable? _repositories;

    public UnitOfWork(AgenciaDbContext context)
    {
        _context = context;
    }

    public IGenericRepository<T> Repository<T>() where T : class
    {
        _repositories ??= new Hashtable();
        var type = typeof(T).Name;

        if (!_repositories.ContainsKey(type))
        {
            var repositoryType = typeof(GenericRepository<>);
            var repositoryInstance = Activator.CreateInstance(repositoryType.MakeGenericType(typeof(T)), _context);
            _repositories.Add(type, repositoryInstance);
        }

        return (IGenericRepository<T>)_repositories[type]!;
    }

    public async Task<int> CompleteAsync() => await _context.SaveChangesAsync();

    public void Dispose() => _context.Dispose();
}