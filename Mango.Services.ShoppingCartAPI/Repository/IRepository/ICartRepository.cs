using Mango.Services.ShoppingCartAPI.Data;
using Mango.Services.ShoppingCartAPI.Models.DTO;

namespace Mango.Services.ShoppingCartAPI.Repository.IRepository
{
    public interface ICartRepository:IBaseRepository<CartDTO>
    {
        public void UpdateCartHeader(CartHeaderDTO cartHeader); 
        public void UpdateCartDetails(CartDetailsDTO cartDetails); 
    }
}
