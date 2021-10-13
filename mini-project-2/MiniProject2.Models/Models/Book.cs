using System.ComponentModel.DataAnnotations;
using MiniProject2.Models.Models.Types;

namespace MiniProject2.Models.Models
{
    public class Book
    {
        [Key]
        public string ISBN { get; set; }
        public ISBNType ISBNType { get; set; }
        public string Title { get; set; }
    }
}