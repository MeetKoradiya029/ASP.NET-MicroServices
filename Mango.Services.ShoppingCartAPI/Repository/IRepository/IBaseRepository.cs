using System.Linq.Expressions;

namespace Mango.Services.ShoppingCartAPI.Repository.IRepository
{
    public interface IBaseRepository<T> where T:class 
    {
        public IEnumerable<T> GetAll();
        public T Get(Expression<Func<T, bool>> filter);

        void Add(T entity);

        void Remove(T entity);

        void RemoveRange(IEnumerable<T> entities);
    }
}
