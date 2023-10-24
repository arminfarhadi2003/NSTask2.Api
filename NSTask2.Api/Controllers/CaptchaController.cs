using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NSTask2.Application.Services;

namespace NSTask2.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CaptchaController : ControllerBase
    {
        private readonly IUserService _service;
        public CaptchaController(IUserService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetCaptcha()
        {
            var result = await _service.CreateCaptcha();

            return Ok(result);
        }
    }
}
