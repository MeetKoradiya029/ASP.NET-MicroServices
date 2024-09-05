using AutoMapper;
using Mango.Services.ShoppingCartAPI.Models;
using Mango.Services.ShoppingCartAPI.Models.DTO;


namespace ProductsAPI
{
    public class MappingConfigs:Profile
    {

        public static MapperConfiguration RegisterMaps ()
        {

            var mappingConfig = new MapperConfiguration (config =>
            {
                config.CreateMap<CartHeaderDTO, CartHeader>().ReverseMap();
                config.CreateMap<CartDetailsDTO, CartDetails>().ReverseMap();
                //config.CreateMap<Product, ProductDTO>();
            });

            return mappingConfig;

        }
    }
}
