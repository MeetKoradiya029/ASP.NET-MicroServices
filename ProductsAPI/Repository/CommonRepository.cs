using ProductsAPI.Data;
using ProductsAPI.Models;
using ProductsAPI.Repository.IRepository;

namespace ProductsAPI.Repository
{
    public class CommonRepository : ICommonRepository
    {
        private AppDbContext _db;
        public IProductRepository Product {  get; private set; }

        public CommonRepository(AppDbContext db)
        {
            _db = db;
            Product = new ProductRepository(_db);
        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
