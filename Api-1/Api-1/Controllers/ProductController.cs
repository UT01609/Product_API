using Api_1.DTO;
using Api_1.InterFace;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api_1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IproductService _iproductService;

        public ProductController(IproductService iproductService)
        {
            _iproductService = iproductService;
        }

        [HttpPost("AddProduct")]
        public async Task<IActionResult> AddProduct(ProductRequest request)
        {
            var data = await _iproductService.AddProduct(request);
            return Ok(data);
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllProduct()
        {
            var data = await _iproductService.GetAllProduct();
            return Ok(data);
        }

        [HttpGet("GetById")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var data = await _iproductService.GetById(id);
            return Ok(data);
        }
        [HttpDelete("DeleteById")]
        public async Task<IActionResult> DeleteByID(Guid id)
        {
            var data = await _iproductService.DeleteByID(id);
            return Ok(data);
        }
        [HttpPut("EditById")]
        public async Task<IActionResult> UpdateProductAsync(Guid id , ProductRequest request)
        {
            var data = await _iproductService.UpdateProductAsync(id , request);
            return Ok(data);

        }
    }
}
