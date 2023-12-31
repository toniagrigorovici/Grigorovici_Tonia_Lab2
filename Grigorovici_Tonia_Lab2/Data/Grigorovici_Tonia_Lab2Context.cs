﻿using System;
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

        public DbSet<Grigorovici_Tonia_Lab2.Models.Book> Book { get; set; } = default!;

        public DbSet<Grigorovici_Tonia_Lab2.Models.Publisher>? Publisher { get; set; }

        public DbSet<Grigorovici_Tonia_Lab2.Models.Author>? Author { get; set; }
    }
}
