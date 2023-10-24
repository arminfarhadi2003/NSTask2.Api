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
    public class ProductTest
    {
        [Fact]
        public async void Product_ShowProductList_OnSuccess()
        {
            var mockProductServies = new Mock<IProductService>();

            mockProductServies.Setup(service => service.ShowProductList()).ReturnsAsync(new List<ShowProductDto>());

            var sut = new ProductController(mockProductServies.Object);

            var result = (OkObjectResult)await sut.Get();

            Assert.NotNull(result);

        }
        [Fact]
        public async void Product_CreateProduct_OnSuccess()
        {
            var mockProductServies = new Mock<IProductService>();

            var dto = new CreateProductDto
            {
                IsAvailable = true,
                Name="test",
                UserId="test",
            };

            mockProductServies.Setup(service => service.CreateProduct(dto));

            var sut = new ProductController(mockProductServies.Object);

            var result = (OkObjectResult)await sut.Post(dto);

        }

        [Fact]
        public async void Product_EditProduct_OnSuccess()
        {
            var mockProductServies = new Mock<IProductService>();

            var dto = new EditProductDto();

            int Id = 2;

            mockProductServies.Setup(service => service.EditProduct(dto));

            var sut = new ProductController(mockProductServies.Object);

            var result = (OkObjectResult)await sut.Put(Id,dto);

            Assert.NotNull(result);

        }
    }
}
