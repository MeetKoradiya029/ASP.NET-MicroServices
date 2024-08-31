using AutoMapper;
using ProductsAPI.Models;
using ProductsAPI.Models.DTO;

namespace ProductsAPI
{
    public class MappingConfigs:Profile
    {

        public static MapperConfiguration RegisterMaps ()
        {

            var mappingConfig = new MapperConfiguration (config =>
            {
                config.CreateMap<ProductDTO, Product>().ReverseMap();
                //config.CreateMap<Product, ProductDTO>();
            });

            return mappingConfig;

        }
    }
}
