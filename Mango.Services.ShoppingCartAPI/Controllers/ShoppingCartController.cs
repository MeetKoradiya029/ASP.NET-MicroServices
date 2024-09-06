using AutoMapper;
using Azure;
using Mango.Services.ShoppingCartAPI.Data;
using Mango.Services.ShoppingCartAPI.Models;
using Mango.Services.ShoppingCartAPI.Models.DTO;
using Mango.Services.ShoppingCartAPI.Repository.IRepository;
using Mango.Services.ShoppingCartAPI.Service.IService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Reflection.PortableExecutable;

namespace Mango.Services.ShoppingCartAPI.Controllers
{
    [Route("api/cart")]
    [ApiController]
    public class ShoppingCartController : ControllerBase
    {
        private  IMapper _mapper;
        private ResponseDTO _responseDTO;
        private ICommonRepositry _commonRepository;
        private readonly AppDbContext _db;
        private IProductService _productService;
         


        public ShoppingCartController(
                IMapper mapper, 
                ICommonRepositry commonRepositry, 
                IProductService productService,
                AppDbContext db
            )
        {
            _mapper = mapper;
            _commonRepository = commonRepositry;
            _db = db;
            _productService = productService;
            _responseDTO = new ResponseDTO();
        }

        [HttpGet("GetCart/{userId}")]
        public async Task<ResponseDTO> GetCart(string userId)
        {
            try
            {
                CartDTO cart = new()
                {
                    CartHeader = _mapper.Map<CartHeaderDTO>(_db.CartHeaders.First(u => u.UserId == userId))
                };

                cart.CartDetails = _mapper.Map<IEnumerable<CartDetailsDTO>>(
                        _db.CartDetails.Where(u => u.CartHeaderId == cart.CartHeader.CartHeaderId));

                IEnumerable<ProductDTO> products = await _productService.GetProducts();

                foreach (var item in cart.CartDetails)
                {
                    item.Product = products.FirstOrDefault(p => p.ProductId == item.ProductId);
                    cart.CartHeader.CartTotal += (item.Count * item.Product.Price);
                }

                _responseDTO.Result = cart;
                //ProductDTO productDTOs =

            }
            catch (Exception ex)
            {
                _responseDTO.Message = ex.Message.ToString();
                _responseDTO.IsSuccess = false;
            }
            return _responseDTO;
        }

        [HttpPost("CartUpsert")]
        public async Task<ResponseDTO> CartUpsert(CartDTO cartDto)
        {
            try
            {
                var cartHeaderFromDb = await _db.CartHeaders.AsNoTracking().FirstOrDefaultAsync(h => h.UserId == cartDto.CartHeader.UserId);
                if (cartHeaderFromDb == null)
                {
                    //Add new Cart Header and cart details
                    CartHeader cartHeader = _mapper.Map<CartHeader>(cartDto.CartHeader);
                    _db.CartHeaders.Add(cartHeader);
                    await _db.SaveChangesAsync();
                    cartDto.CartDetails.First().CartHeaderId = cartHeader.CartHeaderId;
                    _db.CartDetails.Add(_mapper.Map<CartDetails>(cartDto.CartDetails.First()));
                    await _db.SaveChangesAsync();
                } 
                else
                {
                    //check if product details have same product

                    var cartDetailsFromDb = await _db.CartDetails.AsNoTracking().FirstOrDefaultAsync(d => d.ProductId == cartDto.CartDetails.First().ProductId && d.CartHeaderId == cartHeaderFromDb.CartHeaderId);
                    if (cartDetailsFromDb == null)
                    {
                        // add new cart Details when cart header is already eixist
                        cartDto.CartDetails.First().CartHeaderId = cartHeaderFromDb.CartHeaderId;
                        _db.CartDetails.Add(_mapper.Map<CartDetails>(cartDto.CartDetails.First()));
                        await _db.SaveChangesAsync();
                    } 
                    else
                    {
                         cartDto.CartDetails.First().Count += cartDetailsFromDb.Count;
                         cartDto.CartDetails.First().CartHeaderId = cartDetailsFromDb.CartHeaderId;
                         cartDto.CartDetails.First().CartDetailId = cartDetailsFromDb.CartDetailId;

                        _db.CartDetails.Update(_mapper.Map<CartDetails>(cartDto.CartDetails.First()));
                        await _db.SaveChangesAsync(); 
                    }

                }
                _responseDTO.Result = cartDto;
            } catch (Exception ex)
            {
                _responseDTO.Message = ex.Message.ToString();
                _responseDTO.IsSuccess = false;
            }
            return _responseDTO;
        }

        [HttpPost("CartDelete")]
        public async Task<ResponseDTO> RemoveCart([FromBody]int cartDetailsId)
        {
            try
            {
                CartDetails cartDetails = _db.CartDetails.First(d => d.CartDetailId == cartDetailsId);
                _db.CartDetails.Remove(cartDetails);
                int totalCartDetailsCount = _db.CartDetails.Where(u => u.CartHeaderId == cartDetails.CartHeaderId).Count();
                if (totalCartDetailsCount == 1)
                {
                    CartHeader cartHeader = await _db.CartHeaders.FirstOrDefaultAsync(h => h.CartHeaderId == cartDetails.CartHeaderId);
                    _db.CartHeaders.Remove(cartHeader);
                }
                await _db.SaveChangesAsync();

                _responseDTO.Message = "Cart Deleted Successfully";
                _responseDTO.Result = true;
            }
            catch (Exception ex)
            {
                _responseDTO.Message = ex.Message.ToString();
                _responseDTO.IsSuccess = false;
            }
            return _responseDTO;
        }
    }
}
