using Mango.Web.Models;
using Mango.Web.Services.IServices;
using Mango.Web.Utility;

namespace Mango.Web.Services
{
    public class CouponService : ICouponService
    {

        private readonly IBaseService _baseService;

        public CouponService(IBaseService baseService)
        {
            _baseService = baseService;
        }

        public async Task<ResponseDTO?> CreateCouponAsync(CouponDTO couponDTO)
        {
            return await _baseService.SendAsync(new RequestDTO()
            {
                ApiType = StaticData.ApiType.POST,
                Data = couponDTO,
                Url = StaticData.CouponAPIBase + "/api/coupon/add"

            });
        }

        public async Task<ResponseDTO?> GetAllCouponsAsync()
        {
            return await _baseService.SendAsync(new RequestDTO()
            {
                ApiType = StaticData.ApiType.GET,
                Url = StaticData.CouponAPIBase + "/api/coupon"

            });
        }

        public async Task<ResponseDTO?> GetCouponByCodeAsync(string code)
        {
            return await _baseService.SendAsync(new RequestDTO()
            {
                ApiType = StaticData.ApiType.GET,
                Url = StaticData.CouponAPIBase + "/api/coupon/getByCode/" + code

            });
        }

        public async Task<ResponseDTO?> GetCouponByIdAsync(int id)
        {
            return await _baseService.SendAsync(new RequestDTO()
            {
                ApiType = StaticData.ApiType.GET,
                Url = StaticData.CouponAPIBase + "/api/coupon/" + id

            });
        }

        public async Task<ResponseDTO?> UpdateCouponAsync(CouponDTO couponDTO)
        {
            return await _baseService.SendAsync(new RequestDTO()
            {
                ApiType = StaticData.ApiType.PUT,
                Data = couponDTO,
                Url = StaticData.CouponAPIBase + "/api/coupon/edit/"
            });
        }

       public async Task<ResponseDTO?> DeleteCouponAsync(int id)
        {
            return await _baseService.SendAsync(new RequestDTO()
            {
                ApiType = StaticData.ApiType.DELETE,
                Url = StaticData.CouponAPIBase + "/api/coupon/delete/" + id
            });
        }
    }
}
