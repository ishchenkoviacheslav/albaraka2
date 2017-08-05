﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
namespace Al_Baraka.Models
{
    public class ProductContext: DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public ProductContext(DbContextOptions<ProductContext> options)
            : base(options)
        {
        }
    }
}
