using Mango.Services.CouponAPI.Data;
using Mango.Services.CouponAPI.Models;
using Mango.Services.CouponAPI.Repository.IRepository;

namespace Mango.Services.CouponAPI.Repository
{
    public class CouponRepository : BaseRepository<Coupon>, ICouponRepository
    {

        private AppDbContext _db;

        public CouponRepository(AppDbContext db):base(db)
        {
            _db = db;
        }
        public void Update(Coupon obj)
        {
            _db.Coupons.Update(obj);
        }

       
    }
}
