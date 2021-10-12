using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using MiniProject2.ClassLib.Models.Types;

namespace MiniProject2.ClassLib.Models
{
    public class Book
    {
        [Key]
        public string? ISBN { get; set; }
        public ISBNType ISBNType { get; set; }
        public string? Title { get; set; }
    }
}