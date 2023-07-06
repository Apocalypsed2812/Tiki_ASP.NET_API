using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ToDoAPI.Models;
using ToDoAPI.Repositories.ProductRepository;
using ToDoAPI.Services;
using ToDoAPI.Specifications;
using ToDoAPI.Specifications.ProductSpecification;

namespace ToDoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepo;

        public ProductController(IProductRepository service)
        {
            _productRepo = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            try
            {
                return Ok(await _productRepo.GetAllProductsAsync());
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            var product = await _productRepo.GetProductAsync(id);
            return product == null ? NotFound() : Ok(product);
        }

        [HttpPost]
        public async Task<IActionResult> AddNewProduct(ProductModel model)
        {
            try
            {
                var newProductId = await _productRepo.AddProductAsync(model);
                //if(newProductId == -1)
                //{
                    //return BadRequest("Category Id Not Found");
                //}
                var newProduct = await _productRepo.GetProductAsync(newProductId);
                return newProduct == null ? NotFound() : Ok(newProduct);
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, ProductModel model)
        {
            try
            {
                if (id != model.Id)
                {
                    return NotFound();
                }
                await _productRepo.UpdateProductAsync(id, model);
                var products = await _productRepo.GetAllProductsAsync();
                return Ok(products);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            try
            {
                await _productRepo.DeleteProductAsync(id);
                var products = await _productRepo.GetAllProductsAsync();
                return Ok(products);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPost("get-product-greater-than-price")]
        public async Task<IActionResult> GetProductGreaterThanPrice(int price)
        {
            try
            {
                var priceConditon = new ProductSpecification(price);
                var result = await _productRepo.GetProđuctGreaterThanPrice(priceConditon);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Has Orrcurred Error: {ex.Message}");
            }
        }
    }
}
