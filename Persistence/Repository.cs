using System.Data;
using System.Linq.Expressions;
using AuctionApp.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace AuctionApp.Persistence;

public class Repository<T> : IRepository<T> where T : class
{
    private readonly AuctionDBContext _dbContext;
    private readonly DbSet<T> _dbSet;

    public Repository(AuctionDBContext dbContext)
    {
        _dbContext = dbContext;
        _dbSet = _dbContext.Set<T>();
    }
    public T GetById(int id)
    {
        T t = _dbSet.Find(id);
        if (t == null)
        {
            throw new KeyNotFoundException("The specified item does not exist in the database");
        }
        return t;
    }

    // public IEnumerable<T> GetAll()
    // {
    //     return _dbSet.ToList();
    // }

    public IQueryable<T> Query()
    {
        return _dbSet;
    }

    public void Add(T entity)
    {
        try
        {
            _dbSet.Add(entity);
            _dbContext.SaveChanges();
        }
        catch (DbUpdateException)
        {
            throw new DataException("An error occured when trying to add an entity to the database");
        }
    }

    public void Update(T entity)
    {
        try
        {
            _dbSet.Update(entity);
            _dbContext.SaveChanges();
        }
        catch (DbUpdateException)
        {
            throw new DataException("An error occured when trying to update an entity to the database");
        }
    }

    public void Remove(T entity)
    {
        try
        {
            _dbSet.Remove(entity);
            _dbContext.SaveChanges();
        }
        catch (DbUpdateException)
        {
            throw new DataException("An error occured when trying to remove an entity to the database");
        }
    }

    public int DeleteWhere(Expression<Func<T, bool>> predicate)
    {
        return _dbSet.Where(predicate).ExecuteDelete();
    }

    public IDbContextTransaction BeginTransaction()
    {
        return _dbContext.Database.BeginTransaction();
    }
}