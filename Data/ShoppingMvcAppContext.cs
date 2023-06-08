using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ShoppingApp.Models;

    public class ShoppingMvcAppContext : DbContext
    {
        public ShoppingMvcAppContext (DbContextOptions<ShoppingMvcAppContext> options)
            : base(options)
        {
        }

        public DbSet<ShoppingApp.Models.Product> Product { get; set; }

        public DbSet<ShoppingApp.Models.User> User { get; set; }
    }
