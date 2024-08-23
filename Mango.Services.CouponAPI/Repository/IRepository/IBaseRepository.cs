using System.Linq.Expressions;

namespace Mango.Services.CouponAPI.Repository.IRepository
{
    public interface IBaseRepository<T> where T : class
    {
        //public
        IEnumerable<T> GetAll();

        T Get(Expression<Func<T, bool>> filter);

        void Add(T entity);

        void Remove(T entity);

        void RemoveRange(IEnumerable<T> entities);
    }
}
