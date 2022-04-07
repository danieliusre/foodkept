using Microsoft.EntityFrameworkCore;
using FoodKept.Models;

namespace FoodKept.Data
{
    public class ShopContext : DbContext
    {
        public ShopContext(DbContextOptions<ShopContext> opt) : base(opt)
        {

        }
        public DbSet<Food> Food { get; set; }

        public DbSet<Tokens> Tokens { get; set; }
        public DbSet<User> Users { get; set; }

        


    }
}
