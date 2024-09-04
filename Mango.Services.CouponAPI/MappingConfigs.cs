using AutoMapper;
using Mango.Services.CouponAPI.Models;
using Mango.Services.CouponAPI.Models.DTO;

namespace Mango.Services.CouponAPI
{
    public class MappingConfigs : Profile
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<CouponDTO, Coupon>();
                config.CreateMap<Coupon, CouponDTO>();
                /*config.CreateMap<Product, ProductDTO>()
                .ForMember(x => x.ProductCode1, y => y.MapFrom(x => x.ProductCode));*/
            });

            return mappingConfig;
        }
    }
}
