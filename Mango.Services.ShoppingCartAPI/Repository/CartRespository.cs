using Mango.Services.ShoppingCartAPI.Data;
using Mango.Services.ShoppingCartAPI.Models.DTO;
using Mango.Services.ShoppingCartAPI.Repository.IRepository;

namespace Mango.Services.ShoppingCartAPI.Repository
{
    public class CartRespository : BaseRepository<CartDTO>, ICartRepository
    {
        private AppDbContext _db;

        public CartRespository(AppDbContext db):base(db)
        {
            _db = db;
        }

        public void Update(CartDTO cart)
        {
            _db.Update(cart.CartHeader);
            _db.Update(cart.CartDetails);
        }
    }
}
