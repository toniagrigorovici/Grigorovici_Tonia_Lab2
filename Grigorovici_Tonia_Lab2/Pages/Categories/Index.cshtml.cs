using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Grigorovici_Tonia_Lab2.Data;
using Grigorovici_Tonia_Lab2.Models;
using Grigorovici_Tonia_Lab2.Models.ViewModels;

namespace Grigorovici_Tonia_Lab2.Pages.Categories
{
    public class IndexModel : BookCategoriesPageModel
    {
        private readonly Grigorovici_Tonia_Lab2.Data.Grigorovici_Tonia_Lab2Context _context;

        public IndexModel(Grigorovici_Tonia_Lab2.Data.Grigorovici_Tonia_Lab2Context context)
        {
            _context = context;
        }

        public IList<Category> Category { get;set; } = default!;

        public CategoryIndexData CategoryData { get; set; }
        public int CategoryID { get; set; }
        public int BookID { get; set; }
        public async Task OnGetAsync(int? id, int? bookID)
        {
            CategoryData = new CategoryIndexData();
            CategoryData.Categories = await _context.Category
                .Include(i => i.BookCategories)
                    .ThenInclude(i => i.Book)
                          .ThenInclude(b => b.Author)
                .OrderBy(i => i.CategoryName)
                .ToListAsync();

            if (id != null)
            {
                CategoryID = id.Value;
                var category = CategoryData.Categories
                    .SingleOrDefault(i => i.ID == id.Value);

                if (category != null)
                {
                    CategoryData.Books = category.BookCategories.Select(bc => bc.Book).ToList();
                }
            }
        }



        /*
        if (_context.Category != null)
        {
            Category = await _context.Category.ToListAsync();
        }
        */
    }
}

