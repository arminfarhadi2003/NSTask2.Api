using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NSTask2.Domin.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSTask2.Domin.Data
{
    public class NSTaskDb: IdentityDbContext<User, Role, string>
    {
        public NSTaskDb(DbContextOptions<NSTaskDb> options) : base(options)
        {

        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Captcha> Captchas { get; set; }
    }
    
}
