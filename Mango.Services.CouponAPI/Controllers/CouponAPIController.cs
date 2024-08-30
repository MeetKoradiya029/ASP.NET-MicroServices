using AutoMapper;
using Mango.Services.CouponAPI.Data;
using Mango.Services.CouponAPI.Models;
using Mango.Services.CouponAPI.Models.DTO;
using Mango.Services.CouponAPI.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Mango.Services.CouponAPI.Controllers
{
    [Route("api/coupon")]
    [ApiController]
    [Authorize]
    public class CouponAPIController : ControllerBase
    {
        private readonly AppDbContext _db;
        private readonly IMapper _mapper;
        private ResponseDTO _response;
        private ICommonRepository _commonRepo;


        public CouponAPIController(AppDbContext db, IMapper mapper, ICommonRepository commonRepo)
        {
           _db = db;
           _mapper = mapper;
           _response = new ResponseDTO();
           _commonRepo = commonRepo;
        }


        [HttpGet]
        public ResponseDTO Get()
        {
           try
            {
                IEnumerable<Coupon> objList = _commonRepo.Coupon.GetAll();
                var request = _mapper.Map<IEnumerable<CouponDTO>>(objList);
                _response.Result = request;
                
            }
            catch (Exception ex) 
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
            
        }

        [HttpGet]
        [Route("{id:int}")]
        public ResponseDTO Get(int id)
        {
            try
            {
                Coupon obj = _commonRepo.Coupon.Get(f => f.CouponId == id);
                _response.Result = obj;
                
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;

        }


        [HttpGet]
        [Route("getByCode/{code}")]
        public ResponseDTO Get(string code)
        {
            try
            {
                Coupon obj = _commonRepo.Coupon.Get(c => c.CouponCode.ToLower() == code.ToLower());

                if (obj == null)
                {
                    _response.IsSuccess = false;
                }

                _response.Result = _mapper.Map<CouponDTO>(obj);

            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;

        }


        [HttpPost]
        [Route("add")]
        [Authorize(Roles = "ADMIN")]
        public ResponseDTO Add([FromBody] CouponDTO couponDTO)
        {
            try
            {
                Coupon obj = _mapper.Map<Coupon>(couponDTO);



                _commonRepo.Coupon.Add(obj);
                _commonRepo.Save();

                _response.Result = _mapper.Map<CouponDTO>(obj);

            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;

        }

        [HttpPut]
        [Route("edit")]
        [Authorize(Roles = "ADMIN")]
        public ResponseDTO Edit([FromBody] CouponDTO couponDTO)
        {
            try
            {
                Coupon obj = _mapper.Map<Coupon>(couponDTO);



                _commonRepo.Coupon.Update(obj);
                _commonRepo.Save();


                _response.Result = _mapper.Map<CouponDTO>(obj);

            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;

        }

        [HttpDelete]
        [Route("delete/{id:int}")]
        [Authorize(Roles = "ADMIN")]
        public ResponseDTO Delete(int id)
        {
            try
            {
                Coupon obj = _commonRepo.Coupon.Get(f => f.CouponId == id);
                //Coupon obj = _mapper.Map<Coupon>(couponDTO);

                _commonRepo.Coupon.Remove(obj);
                _commonRepo.Save();


                _response.Result = null;
                _response.IsSuccess = true;
                _response.Message = "Coupon Deleted Successfully";

            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;

        }


        //[HttpDelete]
        //[Route("delete")]
        //public ResponseDTO Delete([FromQuery] int id)
        //{
        //    try
        //    {
        //        Coupon obj = _commonRepo.Coupon.Get(f => f.CouponId == id);
        //        //Coupon obj = _mapper.Map<Coupon>(couponDTO);



        //        _commonRepo.Coupon.Remove(obj);
        //        _commonRepo.Save();


        //        _response.Result = null;
        //        _response.IsSuccess = true;
        //        _response.Message = "Coupon Deleted Successfully";

        //    }
        //    catch (Exception ex)
        //    {
        //        _response.IsSuccess = false;
        //        _response.Message = ex.Message;
        //    }
        //    return _response;

        //}


        //[HttpDelete]
        //[Route("delete")]
        //public ResponseDTO Delete([FromBody] deleteReqestBody deleteReq)
        //{
        //    try
        //    {
        //        var reqBody = deleteReq;
        //        Coupon obj = _commonRepo.Coupon.Get(f => f.CouponId == reqBody.Id);
        //        //Coupon obj = _mapper.Map<Coupon>(couponDTO);



        //        _commonRepo.Coupon.Remove(obj);
        //        _commonRepo.Save();


        //        _response.Result = null;
        //        _response.IsSuccess = true;
        //        _response.Message = "Coupon Deleted Successfully";

        //    }
        //    catch (Exception ex)
        //    {
        //        _response.IsSuccess = false;
        //        _response.Message = ex.Message;
        //    }
        //    return _response;

        //}
    }

    //public class deleteReqestBody
    //{
    //    public int Id { get; set; }
    //}
}
