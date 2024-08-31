using System.Linq.Expressions;

namespace ProductsAPI.Repository.IRepository
{
    public interface IBaseRepository<T> where T: class
    {
        IEnumerable<T> GetAll();
        T Get(Expression<Func<T, bool>> filter);
        void Add(T entity);
        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entities);
    }
}
