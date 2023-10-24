using NSTask2.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSTask2.Application.Services
{
    public interface IUserService
    {
        public Task<LoginResultDto> Login(LoginDto dto);
        public Task<bool> Logout();
        public Task<bool> SingUp(SingUpDto dto);
        public Task<CaptchaDto> CreateCaptcha();
    }
}
