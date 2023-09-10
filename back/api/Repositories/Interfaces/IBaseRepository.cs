using System.Linq.Expressions;

namespace api.Repositories.Interfaces
{
    public interface IBaseRepository<T> where T : class
    {
        Task<T?> GetById(int id);
        Task<List<T>> GetAll();
        Task<List<T>> Find(Expression<Func<T, bool>> expression);
        Task Add(T entity);
        void Update(T entity);
        void Remove(T entity);
        Task SaveChanges();
    }
}