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

        public DbSet<Food> FoodData { get; set; }
        public virtual DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<ShoppingCart> Cart { get; set; }
        public DbSet<Discount> DiscountData { get; set; }
        public DbSet<Feedback> Feedbacks { get; set; }
        public DbSet<StarRating> StarRating { get; set; }
        public DbSet<Tokens> Tokens { get; set; }

        public DbSet<User> User { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Food>().ToTable("Food");
            modelBuilder.Entity<ShoppingCart>().ToTable("ShoppingCart");
            modelBuilder.Entity<Discount>().ToTable("Discount");
            modelBuilder.Entity<Feedback>().ToTable("Feedback");
            modelBuilder.Entity<StarRating>().ToTable("StarRating");
            modelBuilder.Entity<Tokens>().ToTable("Tokens");
            modelBuilder.Entity<User>().ToTable("Users");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }
    }
}
