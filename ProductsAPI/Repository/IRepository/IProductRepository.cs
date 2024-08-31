using ProductsAPI.Models;

namespace ProductsAPI.Repository.IRepository
{
    public interface IProductRepository:IBaseRepository<Product>
    {
        public void Update(Product product);
            
    }
}
