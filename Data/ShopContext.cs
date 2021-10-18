﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using FoodKept.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace FoodKept.Data
{
    public class ShopContext : IdentityDbContext<ApplicationUser>
    {
        public ShopContext (DbContextOptions<ShopContext> options)
            : base(options)
        {
        }

        public DbSet<FoodKept.Models.Food> FoodData { get; set; }
        public virtual DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<FoodKept.Models.ShoppingCart> Cart { get; set; }
        public DbSet<Discount> DiscountData { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Food>().ToTable("Food");
            modelBuilder.Entity<ShoppingCart>().ToTable("ShoppingCart");
            modelBuilder.Entity<Discount>().ToTable("Discount");
        }
    }
}
