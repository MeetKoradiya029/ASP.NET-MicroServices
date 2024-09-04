using Mango.Web.Models;
using Mango.Web.Services.IServices;
using Mango.Web.Utility;

namespace Mango.Web.Services
{
    public class ProductService : IProductService
    {

        private readonly IBaseService _baseService;

        public ProductService(IBaseService baseService)
        {
            _baseService = baseService;
        }

        public async Task<ResponseDTO?> CreateProductAsync(ProductDTO ProductDTO)
        {
            return await _baseService.SendAsync(new RequestDTO()
            {
                ApiType = StaticData.ApiType.POST,
                Data = ProductDTO,
                Url = StaticData.ProductAPIBase + "/api/product/add"

            });
        }

        public async Task<ResponseDTO?> GetAllProductsAsync()
        {
            return await _baseService.SendAsync(new RequestDTO()
            {
                ApiType = StaticData.ApiType.GET,
                Url = StaticData.ProductAPIBase + "/api/product"

            });
        }


        public async Task<ResponseDTO?> GetProductByIdAsync(int id)
        {
            return await _baseService.SendAsync(new RequestDTO()
            {
                ApiType = StaticData.ApiType.GET,
                Url = StaticData.ProductAPIBase + "/api/product/" + id

            });
        }

        public async Task<ResponseDTO?> UpdateProductAsync(ProductDTO productDTO)
        {

            return await _baseService.SendAsync(new RequestDTO()
            {
                ApiType = StaticData.ApiType.PUT,
                Data = productDTO,
                Url = StaticData.ProductAPIBase + "/api/product/edit"
            });
        }

       public async Task<ResponseDTO?> DeleteProductAsync(int id)
        {
            return await _baseService.SendAsync(new RequestDTO()
            {
                ApiType = StaticData.ApiType.DELETE,
                Url = StaticData.ProductAPIBase + "/api/product/delete/" + id
            });
        }
    }
}
