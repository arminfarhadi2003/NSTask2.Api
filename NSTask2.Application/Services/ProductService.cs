using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NSTask2.Application.Dtos;
using NSTask2.Domin.Data;
using NSTask2.Domin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSTask2.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly NSTaskDb _db;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;

        public ProductService(NSTaskDb db,UserManager<User> userManager, RoleManager<Role> roleManager)
        {
            _db = db;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<bool> CreateProduct(CreateProductDto dto)
        {
            var user = await _userManager.FindByIdAsync(dto.UserId);

            if (user != null)
            {
                var newProduct = new Product
                {
                    IsAvailable = dto.IsAvailable,
                    Name = dto.Name,
                    ProduceDate = DateTime.Now,
                    ManufactureEmail = user.Email,
                    ManufacturePhone = user.PhoneNumber,
                };

                var productCheck = await _db.Products.Where(p => p.ManufactureEmail == newProduct.ManufactureEmail)
                    .FirstOrDefaultAsync(t => t.ProduceDate == newProduct.ProduceDate);
                if (productCheck == null)
                {

                    await _db.AddAsync<Product>(newProduct);
                    await _db.SaveChangesAsync();

                    return true;
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

        public async Task<bool> EditProduct(EditProductDto dto)
        {
            var product=await _db.Products.FirstOrDefaultAsync(p=>p.Id==dto.ProductId);
            var user = await _userManager.FindByIdAsync(dto.UserId);

            if (user != null && product != null)
            {
                if (product.ManufacturePhone == user.PhoneNumber && product.ManufactureEmail == user.PhoneNumber || dto.UserRole=="Admin")
                {
                    if (dto.IsAvailable != null)
                    {
                        product.IsAvailable = dto.IsAvailable;
                    }
                    if (dto.Name != null)
                    {
                        product.Name = dto.Name;
                    }

                    _db.Products.Update(product);
                    _db.SaveChanges();

                    return true;
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

        public async Task<bool> RemoveProduct(RemoveProductDto dto)
        {
            var product = await _db.Products.FirstOrDefaultAsync(p => p.Id == dto.ProductId);

            var user = await _userManager.FindByIdAsync(dto.UserId);

            if (user != null && product != null )
            {
                if (product.ManufacturePhone == user.PhoneNumber && product.ManufactureEmail == user.PhoneNumber || dto.UserRole == "Admin")
                {
                    _db.Products.Remove(product);
                    _db.SaveChanges();
                    return true;
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

        public async Task<ShowProductDto> ShowProduct(int Id)
        {
            var product = await _db.Products.SingleOrDefaultAsync(p => p.Id == Id);

            var productResult = new ShowProductDto
            {
               IsAvailable = product.IsAvailable,
               Name = product.Name,
               ProduceDate = product.ProduceDate,


            };

            return productResult;

        }

        public async Task<IEnumerable<ShowProductDto>> ShowProductList()
        {
            var products = await _db.Products.ToListAsync();

            var productList = new List<ShowProductDto>();

            foreach (var item in products)
            {
                var product = new ShowProductDto
                {
                    IsAvailable = item.IsAvailable,
                    Name = item.Name,
                    ProduceDate = item.ProduceDate
                };

                productList.Add(product);
            }

            return productList;
        }
    }
}
