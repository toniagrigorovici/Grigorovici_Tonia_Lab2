using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Grigorovici_Tonia_Lab2.Models;

namespace Grigorovici_Tonia_Lab2.Data
{
    public class Grigorovici_Tonia_Lab2Context : DbContext
    {
        public Grigorovici_Tonia_Lab2Context (DbContextOptions<Grigorovici_Tonia_Lab2Context> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>()
                .HasOne(e => e.Borrowing)
            .WithOne(e => e.Book)
                .HasForeignKey<Borrowing>("BookID");
        }

        public DbSet<Grigorovici_Tonia_Lab2.Models.Book> Book { get; set; } = default!;

        public DbSet<Grigorovici_Tonia_Lab2.Models.Publisher>? Publisher { get; set; }

        public DbSet<Grigorovici_Tonia_Lab2.Models.Author>? Author { get; set; }

        public DbSet<Grigorovici_Tonia_Lab2.Models.Category>? Category { get; set; }

        public DbSet<Grigorovici_Tonia_Lab2.Models.Member>? Member { get; set; }

        public DbSet<Grigorovici_Tonia_Lab2.Models.Borrowing>? Borrowing { get; set; }
    }
}
