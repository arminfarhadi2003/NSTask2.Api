using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NSTask2.Application.Dtos;
using NSTask2.Application.Services;

namespace NSTask2.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> LogIn([FromBody] LoginDto dto)
        {
            var login = await _userService.Login(dto);

            if (login!=null)
            {
                return Ok(login);
            }
            else
            {
                return Unauthorized();
            }
        }

        [HttpPost]
        public async Task<IActionResult> SingUp([FromBody] SingUpDto dto)
        {
            var singup = await _userService.SingUp(dto);
            if (singup != false)
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
