using Mango.Services.CouponAPI.Models;

namespace Mango.Services.CouponAPI.Repository.IRepository
{
    public interface ICouponRepository:IBaseRepository<Coupon>
    {
        public void Update(Coupon obj);
    }
}
