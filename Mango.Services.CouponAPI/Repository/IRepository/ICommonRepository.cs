namespace Mango.Services.CouponAPI.Repository.IRepository
{
    public interface ICommonRepository
    {
        ICouponRepository Coupon {  get; }

        void Save();
    }
}
