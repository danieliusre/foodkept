﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using FoodKept.Data;
using FoodKept.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using FoodKept.ViewModels;

namespace FoodKept.Pages.DiscountPages
{
    public class CreateModel : PageModel
    {
        private readonly FoodKept.Data.ShopContext _context;

        public CreateModel(FoodKept.Data.ShopContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Discount Discount { get; set; }
        [BindProperty]
        public Food Food { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            
            Food = await _context.FoodData.FirstOrDefaultAsync(m => m.ID == id);
            
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            //If a user adds a discount to a food, that discount is linked to the food by id
            Food = await _context.FoodData.FirstOrDefaultAsync(m => m.ID == id);
            Discount.FoodId = Food.ID;

            //Calculate Discount Percent
            CalculatePercentage(Food, Discount);

            _context.DiscountData.Add(Discount);
            await _context.SaveChangesAsync();

            string url = "/DiscountPages?id=" + id;
            return Redirect(url);
        }

        private static void CalculatePercentage(Food food, Discount discount)
        {
            int discountpart = (int) Math.Round((1 - (discount.DiscountPrice / food.Price)) * 100);
            discount.DiscountPercent = discountpart;
        }
    }
}
