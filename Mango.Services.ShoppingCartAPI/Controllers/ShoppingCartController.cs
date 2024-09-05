using AutoMapper;
using Mango.Services.ShoppingCartAPI.Models.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Mango.Services.ShoppingCartAPI.Controllers
{
    [Route("api/cart")]
    [ApiController]
    public class ShoppingCartController : ControllerBase
    {
        private  IMapper _mapper;
        private ResponseDTO _responseDTO;

        public ShoppingCartController(IMapper mapper)
        {
            _mapper = mapper;
            _responseDTO = new ResponseDTO();
        }
    }
}
