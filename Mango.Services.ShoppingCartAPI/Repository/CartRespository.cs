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



        public void UpdateCartDetails(CartDetailsDTO cartDetails)
        {
            _db.Update(cartDetails);
        }

        public void UpdateCartHeader(CartHeaderDTO cartHeader)
        {
            _db.Update(cartHeader);
        }
    }
}
