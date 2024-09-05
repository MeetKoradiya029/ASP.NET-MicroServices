using Mango.Services.ShoppingCartAPI.Data;
using Mango.Services.ShoppingCartAPI.Repository.IRepository;

namespace Mango.Services.ShoppingCartAPI.Repository
{
    public class CommonRepository : ICommonRepositry
    {
        public ICartRepository cart { get; private set; }
        private AppDbContext _db ;

        public CommonRepository(AppDbContext db)
        {
            _db = db;
            cart = new CartRespository(_db);

        }

        public void Save()
        {
            _db.SaveChanges();  
        }
    }
}
