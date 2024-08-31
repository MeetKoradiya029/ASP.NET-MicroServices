using ProductsAPI.Models;

namespace ProductsAPI.Repository.IRepository
{
    public interface ICommonRepository
    {
        IProductRepository Product { get; }
        void Save();
    }
}
