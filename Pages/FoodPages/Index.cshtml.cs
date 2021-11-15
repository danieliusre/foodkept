﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using FoodKept.Data;
using FoodKept.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using static System.Net.Mime.MediaTypeNames;
using FoodKept.ViewModels;
using FoodKept.Helpers;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FoodKept.Pages.FoodPages
{
    [Authorize(Roles ="Admin, Restaurant")]
    public class IndexModel : PageModel
    {
        private readonly FoodKept.Data.ShopContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public IndexModel(FoodKept.Data.ShopContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [BindProperty]
        public PaginatedList<Food> Food { get; set; }
        [BindProperty]
        public List<SelectListItem> FoodCategory { get; set; }
        public string NameSort { get; set; }
        public string PriceSort { get; set; }
        public string DiscountSort { get; set; }
        public string QuantitySort { get; set; }
        public string FoodCategorySort { get; set; }
        public string CurrentFilter { get; set; }
        public string CurrentSort { get; set; }
        public string CurrentCategory { get; set; }

        public async Task OnGetAsync(string sortOrder, string currentFilter, string searchString, string currentCategory, int? pageIndex)
        {
            //Load food categories
            FoodCategory = Enum.GetValues(typeof(FoodCategories.Category)).Cast<FoodCategories.Category>().Select(v => new SelectListItem
            {
                Text = v.ToString(),
                Value = v.ToString()
            }).ToList();

            //Sorting system
            CurrentSort = sortOrder;
            NameSort = string.IsNullOrEmpty(sortOrder) ? "_FoodName" : "FoodName";
            PriceSort = sortOrder == "Price" ? "_Price" : "Price";
            DiscountSort = sortOrder == "Discount" ? "_Discount" : "Discount";
            QuantitySort = sortOrder == "Quantity" ? "_Quantity" : "Quantity";
            FoodCategorySort = string.IsNullOrEmpty(sortOrder) ? "_FoodCategory" : "FoodCategory";

            if (searchString != null)
            {
                pageIndex = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            CurrentFilter = searchString;
            CurrentCategory = currentCategory;

            //Query for food from current user
            ApplicationUser applicationUser = await _userManager.GetUserAsync(User);
            IQueryable<Food> foodIQ = _context.FoodData.Include(c => c.ApplicationUser).Where(c => c.ApplicationUserId == applicationUser.Id);

            //Filter food
            if (!string.IsNullOrEmpty(searchString))
            {
                foodIQ = foodIQ.Where(s => s.FoodName.Contains(searchString));
            }
            else if (!string.IsNullOrEmpty(CurrentCategory))
            {
                if (CurrentCategory != "None")
                    foodIQ = foodIQ.Where(s => s.FoodCategory.Contains(CurrentCategory));
            }

            //Sort everything
            if (sortOrder == null)
            {
                foodIQ = FoodSortHelper.SortCommandHandler["FoodName"]("FoodName", foodIQ);
            }
            else if (sortOrder[0] == '_')
            {
                foodIQ = FoodSortHelper.SortCommandHandler[sortOrder](sortOrder.Substring(1), foodIQ);
            }
            else
            {
                foodIQ = FoodSortHelper.SortCommandHandler[sortOrder](sortOrder, foodIQ);
            }

            
            Food = await PaginatedList<Food>.CreateAsync(foodIQ, pageIndex ?? 1, 5);

            //Calculate Discounts
            Food = await CalculateCurrentPrice.CalculatePriceForFoodListAsync(Food: Food);
        }
    }
}
