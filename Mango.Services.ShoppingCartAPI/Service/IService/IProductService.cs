using Mango.Services.ShoppingCartAPI.Models.DTO;

namespace Mango.Services.ShoppingCartAPI.Service.IService
{
    public interface IProductService
    {
        public Task<IEnumerable<ProductDTO>> GetProducts();
    }
}
