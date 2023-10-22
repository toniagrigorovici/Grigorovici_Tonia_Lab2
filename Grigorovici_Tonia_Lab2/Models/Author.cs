﻿using System.ComponentModel.DataAnnotations;

namespace Grigorovici_Tonia_Lab2.Models
{
    public class Author
    {
        public int ID { get; set; }
        public string FirstName {  get; set; }
        public string LastName { get; set; }
        [Display(Name ="Author")]
        public string AuthorName { get { return FirstName + " " + LastName; } }
        public ICollection<Book>? Books { get; set; }
    }
}
