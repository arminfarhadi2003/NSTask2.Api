using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NSTask2.Api.Controllers;
using NSTask2.Application.Dtos;
using NSTask2.Application.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSTask2.Test
{
    public class UserTest
    {
        [Fact]
        public async void User_Login_OnSuccess()
        {
            var mockUserServies = new Mock<IUserService>();

            var dto = new LoginDto();

            mockUserServies.Setup(service=>service.Login(dto)).ReturnsAsync(new LoginResultDto());

            var sut = new UserController(mockUserServies.Object);

            var result = (OkObjectResult)await sut.LogIn(dto);
           
            Assert.NotNull(result);

        }

        [Fact]
        public async void User_SingUp_OnSuccess()
        {
            var mockUserServies = new Mock<IUserService>();

            var dto = new SingUpDto();

            mockUserServies.Setup(service => service.SingUp(dto));

            var sut = new UserController(mockUserServies.Object);

            var result = (OkObjectResult)await sut.SingUp(dto);

            Assert.NotNull(result);

        }


    }
}
