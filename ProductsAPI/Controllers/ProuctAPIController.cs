using AutoMapper;
using Mango.Services.ProductsAPI.Models.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductsAPI.Data;
using ProductsAPI.Models;
using ProductsAPI.Models.DTO;
using ProductsAPI.Repository.IRepository;

namespace ProductsAPI.Controllers
{
    [Route("api/product")]
    [ApiController]
    [Authorize]
    public class ProuctAPIController : ControllerBase
    {
        private ICommonRepository _commonRepo;
        private ResponseDTO _responseDto;
        private IMapper _mapper;

        public ProuctAPIController(IMapper mapper, ICommonRepository commonRepo)
        {
            _commonRepo = commonRepo;
            _responseDto = new ResponseDTO();
            _mapper = mapper;
        }

        [HttpGet]
        public ResponseDTO Get()
        {
            try
            {
                IEnumerable<Product> productList = _commonRepo.Product.GetAll();
                var result = _mapper.Map<IEnumerable<ProductDTO>>(productList);
                _responseDto.Result = result;
                

            } catch (Exception ex)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = ex.Message;

            }
            return _responseDto;

        }

        [HttpGet]
        [Route("{id:int}")]
        public ResponseDTO Get(int id)
        {
            try
            {
                Product product = _commonRepo.Product.Get(p => p.ProductId == id);
                _responseDto.Result = product;
            }
            catch (Exception ex)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = ex.Message;
            }
            return _responseDto;

        }

        [HttpPut]
        [Route("edit")]
        [Authorize(Roles = "ADMIN")]
        public ResponseDTO Edit([FromBody] ProductDTO productDTO)
        {
            try
            {
                Product product = _mapper.Map<Product>(productDTO);
                _commonRepo.Product.Update(product);
                _commonRepo.Save();
                _responseDto.Result = _mapper.Map<ProductDTO>(product);

            } catch (Exception ex)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = ex.Message;
            }

            return _responseDto;
        }

        [HttpPost]
        [Route("add")]
        [Authorize(Roles = "ADMIN")]
        public ResponseDTO Add([FromBody] ProductDTO productDTO)
        {
            try
            {
                Product product = _mapper.Map<Product>(productDTO);
                _commonRepo.Product.Add(product);
                _commonRepo.Save();
                _responseDto.Result = _mapper.Map<ProductDTO>(product);

            }
            catch (Exception ex)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = ex.Message;
            }

            return _responseDto;
        }

        [HttpDelete]
        [Route("delete/{id:int}")]
        [Authorize(Roles = "ADMIN")]
        public ResponseDTO Delete (int id)
        {
            try
            {
                Product product = _commonRepo.Product.Get(p => p.ProductId == id);
                _commonRepo.Product.Remove(product);
                _commonRepo.Save();

                _responseDto.Result = null;
                _responseDto.Message = "Product deleted succesfully";

                
            } catch (Exception ex)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = ex.Message;

            }

            return _responseDto;
        }
    }
}
