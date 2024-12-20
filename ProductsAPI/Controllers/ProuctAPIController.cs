using AutoMapper;
using Dapper;
using Mango.Services.ProductsAPI.Models.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using ProductsAPI.Data;
using ProductsAPI.Models;
using ProductsAPI.Models.DTO;
using ProductsAPI.Repository.IRepository;
using System.Data;

namespace ProductsAPI.Controllers
{
    [Route("api/product")]
    [ApiController]
    public class ProuctAPIController : ControllerBase
    {
        private ICommonRepository _commonRepo;
        private ResponseDTO _responseDto;
        private IMapper _mapper;
        private readonly IConfiguration _configuration;
        private string connectionString;

        public ProuctAPIController(IMapper mapper, ICommonRepository commonRepo, IConfiguration configuration)
        {
            _commonRepo = commonRepo;
            _responseDto = new ResponseDTO();
            _mapper = mapper;
            _configuration = configuration;
            connectionString = _configuration.GetConnectionString("DefaultConnection");
        }

        [HttpGet]
        public async Task<ResponseDTO> Get()
        {
            try
            {
                //IEnumerable<Product> productList = _commonRepo.Product.GetAll();
                //var result = _mapper.Map<IEnumerable<ProductDTO>>(productList);

               
				DataTable resultSet = new DataTable();
				using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT * FROM Products";

                    IEnumerable<Product> product  = await connection.QueryAsync<Product>(query);

                    if (product != null)
                    {
                        var result = _mapper.Map<IEnumerable<ProductDTO>>(product);
                        _responseDto.Result = result;
                    }
                }

                
            } catch (Exception ex)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = ex.Message;

            }
            return _responseDto;
        }

        [HttpGet]
        [Route("{id:int}")]
        [Authorize]
        public ResponseDTO Get(int id)
        {
            try
            {
                //Product product = _commonRepo.Product.Get(p => p.ProductId == id);

                //string connectionString = _configuration.GetConnectionString("DefaultConnection");

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT * FROM Products WHERE ProductId=@Id";

                    var product = connection.QuerySingle<Product>(query, new { Id = id });
                    if (product != null)
                    {
                        _responseDto.Result = _mapper.Map<ProductDTO>(product);
                    }
                }

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
            //string connectionString = _configuration.GetConnectionString("DefaultConnection");
            try
            {

                Product product = _mapper.Map<Product>(productDTO);

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "UPDATE Products SET Name=@Name, Price=@Price, Description=@Description, CategoryName=@CategoryName " +
                        "WHERE ProductId=@ProductId";

                   int count = connection.Execute(query, product);
                    if (count > 0)
                    {
                        _responseDto.Result = _mapper.Map<ProductDTO>(product);
                    } else
                    {
                        _responseDto.IsSuccess = false;
                        _responseDto.Message = "Product not found";
						//_responseDto.Message = "Product not found";

					}

				}
                //_commonRepo.Product.Update(product);
                //_commonRepo.Save();
                

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
            //string connectionString = _configuration.GetConnectionString("DefaultConnection");
            try
            {
                //public int ProductId { get; set; }
                //public string Name { get; set; }
                //public double Price { get; set; }
                //public string Description { get; set; }
                //public string CategoryName { get; set; }


               Product product = _mapper.Map<Product>(productDTO);
                //_commonRepo.Product.Add(product);
                //_commonRepo.Save();

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "INSERT INTO Products " + 
                        "(Name, Price, Description, CategoryName) " + 
                        "OUTPUT INSERTED.* " +                          // OUTPUT INSERTED.* returns inserted row.
                        "VALUES (@Name, @Price, @Description, @CategoryName)";

                    //int count = connection.Execute(query, product);
                    Product newProduct = connection.QuerySingleOrDefault<Product>(query, product);
                    if (newProduct != null)
                    {
                        _responseDto.Result = _mapper.Map<ProductDTO>(newProduct);
                    }
                }

              

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
            //string connectionString = _configuration.GetConnectionString("DefaultConnection");
            try
            {
                //Product product = _commonRepo.Product.Get(p => p.ProductId == id);
                //_commonRepo.Product.Remove(product);
                //_commonRepo.Save();

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "DELETE FROM Products WHERE ProductId=@Id";
                    int count = connection.Execute(query, new { Id = id });
                    if (count > 0)
                    {
                        _responseDto.Message = "Product Deleted Successfully";
                        
                    }
                }

                _responseDto.Result = null;
                

                
            } catch (Exception ex)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = ex.Message;

            }

            return _responseDto;
        }
    }
}
