using Mango.Services.CouponAPI.Data;
using Mango.Services.CouponAPI.Models;
using Mango.Services.CouponAPI.Repository.IRepository;

namespace Mango.Services.CouponAPI.Repository
{
    public class CommonRepository : ICommonRepository
    {
        private AppDbContext _db;
        public ICouponRepository Coupon { get; private set; }
        public CommonRepository(AppDbContext db)
        {
            _db = db;
            Coupon = new CouponRepository(_db);
        }

        

        public void Save()
        {
           _db.SaveChanges();
        }
    }
}
