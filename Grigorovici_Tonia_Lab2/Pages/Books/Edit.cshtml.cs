﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Grigorovici_Tonia_Lab2.Data;
using Grigorovici_Tonia_Lab2.Models;
using System.Net;

namespace Grigorovici_Tonia_Lab2.Pages.Books
{
    public class EditModel : BookCategoriesPageModel 
    {
        private readonly Grigorovici_Tonia_Lab2.Data.Grigorovici_Tonia_Lab2Context _context;

        public EditModel(Grigorovici_Tonia_Lab2.Data.Grigorovici_Tonia_Lab2Context context)
        {
            _context = context;
        }

        [BindProperty]
        public Book Book { get; set; } //= default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Book == null)
            {
                return NotFound();
            }

            /*var book*/
            Book = await _context.Book
                .Include(b => b.Author)
                .Include(b => b.Publisher)
                .Include(b => b.BookCategories).ThenInclude(b => b.Category)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.ID == id);


            if (Book == null)
            {
                return NotFound();
            }

            //Book = book;
            PopulateAssignedCategoryData(_context, Book);

            /*var authorList = _context.Author.Select(x => new
            {
                x.ID,
                FullName = x.LastName + " " + x.FirstName
            });*/

            ViewData["AuthorID"] = new SelectList(_context.Set<Author>(), "ID", "AuthorName");
            ViewData["PublisherID"] = new SelectList(_context.Set<Publisher>(), "ID", "PublisherName");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.


        public async Task<IActionResult> OnPostAsync(int? id, string[] selectedCategories)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookToUpdate = await _context.Book
                .Include(i => i.Author)
                .Include(i => i.Publisher)
                .Include(i => i.BookCategories).ThenInclude(i => i.Category)
                .FirstOrDefaultAsync(m => m.ID == id);

           

            if (bookToUpdate == null)
            {
                return NotFound();
            } 
 

            
            var selectedCategoryIDs = selectedCategories.Select(int.Parse).ToList(); 
            var categoriesToRemove = bookToUpdate.BookCategories.Where(b => !selectedCategoryIDs.Contains(b.CategoryID)).ToList();

            foreach (var category in categoriesToRemove)
            {
                bookToUpdate.BookCategories.Remove(category);
            }


            
            if (await TryUpdateModelAsync<Book>(
                bookToUpdate,
                "Book",
                i => i.Title, i => i.AuthorID,
                i => i.Price, i => i.PublishingDate, i => i.PublisherID))
            {
                UpdateBookCategories(_context, selectedCategories, bookToUpdate);
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }

            UpdateBookCategories(_context, selectedCategories, bookToUpdate);
            PopulateAssignedCategoryData(_context, bookToUpdate);
            return Page();
            /*
             if (!ModelState.IsValid)
             {
                 return Page();
             }

             _context.Attach(Book).State = EntityState.Modified;

             try
             {
                 await _context.SaveChangesAsync();
             }
             catch (DbUpdateConcurrencyException)
             {
                 if (!BookExists(Book.ID))
                 {
                     return NotFound();
                 }
                 else
                 {
                     throw;
                 }
             }

             return RedirectToPage("./Index");
         }

         private bool BookExists(int id)
         {
             return (_context.Book?.Any(e => e.ID == id)).GetValueOrDefault();
         }
         */
        }
    }
}

