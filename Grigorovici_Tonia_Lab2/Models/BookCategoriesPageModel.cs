﻿using Microsoft.AspNetCore.Mvc.RazorPages;
using Grigorovici_Tonia_Lab2.Data;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Net.WebSockets;
using System.Security.Cryptography.X509Certificates;

namespace Grigorovici_Tonia_Lab2.Models
{
    public class BookCategoriesPageModel:PageModel
    {
        public List<AssignedCategoryData> AssignedCategoryDataList;
        public void PopulateAssignedCategoryData(Grigorovici_Tonia_Lab2Context context, Book book)
        {
            var allCategories = context.Category;
            var bookCategories = new HashSet<int>(book.BookCategories.Select(c => c.CategoryID));
            AssignedCategoryDataList = new List<AssignedCategoryData>();
            foreach (var cat in allCategories)
            {
                AssignedCategoryDataList.Add(new AssignedCategoryData
                {
                    CategoryID = cat.ID,
                    Name = cat.CategoryName,
                    Assigned = bookCategories.Contains(cat.ID)
                });
            }
        }

        public void UpdateBookCategories(Grigorovici_Tonia_Lab2Context context, string[] selectedCategories, Book bookToUpdate)
        {
            if (selectedCategories == null)
            {
                bookToUpdate.BookCategories = new List<BookCategory>();
                return;
            }

            var selectedCategoriesHS = new HashSet<string>(selectedCategories);
            var bookCategories = new HashSet<int>(bookToUpdate.BookCategories.Select(c => c.CategoryID));
            foreach (var cat in context.Category)
            {
                if (selectedCategoriesHS.Contains(cat.ID.ToString()))
                {
                    bookToUpdate.BookCategories.Add(
                        new BookCategory
                        {
                            BookID = bookToUpdate.ID,
                            CategoryID = cat.ID
                        });
                }
                else
                {
                    if (bookCategories.Contains(cat.ID))
                    {
                        /*BookCategory courseToRemove = bookToUpdate
                            .BookCategories
                            .SingleOrDefault(i => i.CategoryID == cat.ID);*/
                        IEnumerable<BookCategory> coursesToRemove = bookToUpdate
                            .BookCategories
                            .Where(i => i.CategoryID == cat.ID);

                    }
                }
            }
        }
    }
}
