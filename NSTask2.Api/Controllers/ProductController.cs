using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NSTask2.Application.Dtos;
using NSTask2.Application.Services;

namespace NSTask2.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _service;
        public ProductController(IProductService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _service.ShowProductList();

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _service.ShowProduct(id);

            return Ok(result);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Post(CreateProductDto dto)
        {
            dto.UserId = User.Claims.First(p => p.Type == "UserId").Value;

            var result = await _service.CreateProduct(dto);

            if (result != null && result == true)
            {
                return Ok();
            }
            else
            {
                return Unauthorized();
            }
        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, EditProductDto dto)
        {
            dto.UserId = User.Claims.First(p => p.Type == "UserId").Value;

            dto.ProductId = id;

            dto.UserRole = User.Claims.First(p => p.Type == "Role").Value;

            var result = await _service.EditProduct(dto);

            if (result != null && result == true)
            {
                return Ok();
            }
            else
            {
                return Unauthorized();
            }
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id,RemoveProductDto dto)
        {
            var userId = User.Claims.First(p => p.Type == "UserId").Value;

            dto.UserId = userId;
            dto.ProductId = id;
            dto.UserRole = User.Claims.First(p => p.Type == "Role").Value;

            var result = await _service.RemoveProduct(dto);

            if (result != null && result == true)
            {
                return Ok();
            }
            else
            {
                return Unauthorized();
            }
        }
    }
}
