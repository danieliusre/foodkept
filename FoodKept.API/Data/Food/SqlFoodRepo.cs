

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FoodKept.Data;

namespace FoodKept.Models
{
    public class SQLFoodRepository : IFoodRepository
    {
        private readonly ShopContext _context;

        public SQLFoodRepository(ShopContext context)
        {
            _context = context;
        }

        public Food Add(Food food)
        {
            _context.Food.Add(food);
            _context.SaveChanges();
            return food;
        }

        public Food Delete(int id)
        {
            Food food = _context.Food.Find(id);
            if(food != null)
            {
                _context.Food.Remove(food);
                _context.SaveChanges();
            }
            return food;
        }

        public IEnumerable<Food> GetAllFood()
        {
            return _context.Food.ToList();
        }

        public Food GetFood(int id)
        {
            return _context.Food.Find(id);
        }

        public Food Update(Food foodChanges)
        {
            var food = _context.Food.Attach(foodChanges);
            food.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();
            return foodChanges;
        }
    }
}
