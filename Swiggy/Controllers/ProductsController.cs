using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swiggy.Core.Repository;
using Swiggy.Data;
using Swiggy.Models;

namespace Swiggy.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : Controller
    {
        private readonly SwiggyDbContext swiggyDbContext;
        private readonly ProductRepository _productRepository;

        public ProductsController(SwiggyDbContext swiggyDbContext, ProductRepository productRepository)
        {
            
            this.swiggyDbContext = swiggyDbContext;
            _productRepository = productRepository;
        }
        [HttpGet]
        public Task<List<ProductsModel>> GetProducts()
        {
            try
            {
                return _productRepository.GetProducts();
            }
            catch(Exception e)
            {
                return null;
            }
            
        }
        [HttpPost]
        public async Task<IActionResult> AddProduct([FromBody] AddProductModel addProductModel)
        {
            try
            {
                var result = _productRepository.AddProduct(addProductModel);
                return Ok(result);
            }
            catch(Exception e)
            {
                return Ok(e);
            }
        }
        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> UpdateProduct([FromRoute] Guid id, AddProductModel addProductModel)
        {
            try
            {
                var result = await _productRepository.UpdateProduct(id, addProductModel);
                return Ok(result);
            }
            catch(Exception e)
            {
                return BadRequest(e);
            }
        }
        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> DeleteProduct([FromRoute] Guid id)
        {
            try
            {
                var result = await _productRepository.DeleteProduct(id);
                return Ok(result);
            }
            catch(Exception e)
            {
                return NotFound();
            }
        }
    }
}
