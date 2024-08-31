using ProductsAPI.Data;
using ProductsAPI.Models;
using ProductsAPI.Repository.IRepository;
using System.Linq.Expressions;

namespace ProductsAPI.Repository
{
    public class ProductRepository : BaseRepository<Product>, IProductRepository
    {
        
        private AppDbContext _db;

        public ProductRepository(AppDbContext db):base(db)
        {
            _db = db;
        }
        public void Update(Product product)
        {
            _db.Update(product);
        }
    }
}
