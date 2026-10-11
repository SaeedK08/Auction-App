using System.Linq.Expressions;

namespace AuctionApp.Persistence;

public interface IRepository<T> where T : class
{
    T GetById(int id);
    IEnumerable<T> GetAll();
    IQueryable<T> Query(); 
    void Add(T entity);
    void Update(T entity);
    void Remove(T entity);
}