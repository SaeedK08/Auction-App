using System.Data;
using AuctionApp.Persistence;
using Microsoft.EntityFrameworkCore;

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
        return  _dbSet.Find(id);
    }

    public IEnumerable<T> GetAll()
    {
        return _dbSet.ToList();
    }

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
}