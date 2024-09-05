using Mango.Services.ShoppingCartAPI.Data;
using Mango.Services.ShoppingCartAPI.Models.DTO;

namespace Mango.Services.ShoppingCartAPI.Repository.IRepository
{
    public interface ICartRepository:IBaseRepository<CartDTO>
    {
        public void Update(CartDTO cart); 
    }
}
