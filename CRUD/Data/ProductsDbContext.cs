using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CRUD.Models;

namespace CRUD.Data
{
    public class ProductsDbContext : DbContext
    {
        public ProductsDbContext(DbContextOptions<ProductsDbContext>options):base(options)
        {

        }
        public DbSet<Product> Products { get; set; }
    }
}
