using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using NSTask2.Application.Dtos;
using NSTask2.Domin.Data;
using NSTask2.Domin.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace NSTask2.Application.Services
{
    public class UserService : IUserService
    {
        private readonly NSTaskDb _db;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly RoleManager<Role> _roleManager;

        public UserService (NSTaskDb db, UserManager<User> userManager, SignInManager<User> signInManager, RoleManager<Role> roleManager)
        {
            _db = db;
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }


        public async Task<LoginResultDto> Login(LoginDto dto)
        {
            var user = await _userManager.FindByEmailAsync(dto.Email);

            var result = await _signInManager.PasswordSignInAsync(user, dto.Password, false, false);

            var captcha= await CaptchaValidator(dto.Captcha);

            if (result.Succeeded == true && captcha==true)
            {
                var token= await CreateToken(user.Id);

                var tokenDto = new LoginResultDto
                {
                    Token = token,
                };

                return tokenDto;
            }
            else
            {
                return null;
            }
        }

        public async Task<bool> Logout()
        {
            await _signInManager.SignOutAsync();

            return true;
        }

        public async Task<bool> SingUp(SingUpDto dto)
        {
            User newUser=new User
            {
                Email= dto.Email,
                UserName=dto.Username,
                PhoneNumber=dto.PhoneNumber,
            };

            //var usersInRole = _userManager.GetUsersInRoleAsync(Name).Result;


            var result=await _userManager.CreateAsync(newUser,dto.Password);

            var captcha = await CaptchaValidator(dto.Captcha);

            if (result.Succeeded == true && captcha==true)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private async Task<string> CreateToken(string userId)
        {
            var roleId=await _db.UserRoles.FirstOrDefaultAsync(p=>p.UserId==userId);

            var role = await _roleManager.FindByIdAsync(roleId.RoleId);

            var claims = new List<Claim>
                {
                    new Claim ("UserId", userId),
                    new Claim ("Role", role.NormalizedName),
                };

            string key = "{16D9BBF8-FA00-4D89-9BB5-99610E95BA70}";
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var credentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
            var tokenexp = DateTime.Now.AddDays(10);
            var token = new JwtSecurityToken(
                issuer: "test",
                audience: "test",
                expires: tokenexp,
                notBefore: DateTime.Now,
                claims: claims,
                signingCredentials: credentials
                );
            var jwtToken = new JwtSecurityTokenHandler().WriteToken(token);

            //var tokenResult = new TokenResultDto
            //{
            //    Token = jwtToken,
            //    TokenExp = tokenexp,
            //};


            return jwtToken;
        }

        private async Task<bool> CaptchaValidator(string value)
        {
            var captcha = await _db.Captchas.FirstOrDefaultAsync(p => p.Value==value);
            
            if(captcha != null)
            {
                if (captcha.TimeExp > DateTime.Now || captcha.Exp==true)
                {
                    _db.Captchas.Remove(captcha);
                    _db.SaveChanges();

                    return false;

              
                }
                if (captcha.Exp != true && captcha.Value == value)
                {
                    return true;
                }
                else if (captcha.Exp != false && captcha.Value == value)
                {
                    captcha.Exp = true;

                    _db.Update(captcha);
                    _db.SaveChanges();

                    return false;

                    
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public async Task<CaptchaDto> CreateCaptcha()
        {
            var randomNum = new Random();

            var newCaptcha = new Captcha
            {
                Exp = false,
                Value = Convert.ToString(randomNum.Next()),
                TimeExp = DateTime.Now.AddMinutes(10),
            };

            await _db.Captchas.AddAsync(newCaptcha);
            _db.SaveChanges();

            var captcha = await _db.Captchas.FirstOrDefaultAsync(p => p.Value == newCaptcha.Value && p.TimeExp == newCaptcha.TimeExp);


            var dto = new CaptchaDto
            {
                CaptchaValue=captcha.Value,
            };

            return dto;
        }
    }
}
