using Microsoft.EntityFrameworkCore;
using StudyVera.Domain.Interfaces;
using System.Linq.Expressions;

namespace StudyVera.Infrastructure.Persistence.Repositories;

public abstract class RepositoryBase<T> : IRepository<T> where T : class
{
    protected readonly AppDbContext _context;
    public RepositoryBase(AppDbContext context)
    {
        _context = context;
    }
    public void Create(T entity) => _context.Set<T>().Add(entity);

    public void Delete(T entity) => _context.Set<T>().Remove(entity);

    public IQueryable<T> FindAll(bool trackChanges) =>
        !trackChanges ?
        _context.Set<T>().AsNoTracking() :
        _context.Set<T>();

    public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression, bool trackChanges) =>
        !trackChanges ?
        _context.Set<T>().Where(expression).AsNoTracking() :
        _context.Set<T>().Where(expression);

    public void Update(T entity) => _context.Set<T>().Update(entity);
}
