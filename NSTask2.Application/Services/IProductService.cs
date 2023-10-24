using NSTask2.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSTask2.Application.Services
{
    public interface IProductService
    {
        public Task<IEnumerable<ShowProductDto>> ShowProductList();
        public Task<ShowProductDto> ShowProduct(int Id);
        public Task<bool> EditProduct(EditProductDto dto);
        public Task<bool> CreateProduct(CreateProductDto dto);
        public Task<bool> RemoveProduct(RemoveProductDto dto);
    }
}
