﻿using System.ComponentModel.DataAnnotations;

namespace Grigorovici_Tonia_Lab2.Models
{
    public class Publisher
    {
        public int ID { get; set; }
        [Display(Name = "Publisher")]
        public string PublisherName {  get; set; }
        public ICollection<Book>? Books { get; set; }
    }
}
