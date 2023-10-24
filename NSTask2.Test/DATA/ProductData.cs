using NSTask2.Domin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSTask2.Test.DATA
{
    public class ProductData
    {
        public List<Product> userData()
        {
            return new List<Product>
            {
                new Product
                {
                    Id = 1,
                    IsAvailable = true,
                    ManufactureEmail="Test@test.test",
                    ManufacturePhone="123456789",
                    ProduceDate=DateTime.Now,
                    Name="testProduct",

                },
            };
        }
    }
}
