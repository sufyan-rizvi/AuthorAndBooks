using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AuthorAndBooks.Models
{
    public class Book
    {
        public virtual int Id { get; set; }
        [Required]
        public virtual string Name { get; set; }
        [Required]
        public virtual string Genre { get; set; }
        [Required]
        public virtual string Description { get; set; }
        public virtual Author Author { get; set; }

    }
}