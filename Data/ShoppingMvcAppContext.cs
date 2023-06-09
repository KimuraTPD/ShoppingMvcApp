using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ShoppingMvcApp.Models;

    public class ShoppingMvcAppContext : DbContext
    {
        public ShoppingMvcAppContext (DbContextOptions<ShoppingMvcAppContext> options)
            : base(options)
        {
        }

        public DbSet<ShoppingMvcApp.Models.Product> Product { get; set; }

        public DbSet<ShoppingMvcApp.Models.User> User { get; set; }
    }
