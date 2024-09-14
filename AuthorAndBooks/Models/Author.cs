using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AuthorAndBooks.Models
{
    public class Author
    {
        
        public virtual Guid Id { get; set; }
        [Required]        
        public virtual string Password { get; set; }
        [Required]
        [Display(Name="Author Name")]
        public virtual string Name { get; set; }
        [Required]
        public virtual string Email { get; set; }
        [Required]
        public virtual int Age { get; set; }
        public virtual IList<Book> Books { get; set; } = new List<Book>();
        public virtual AuthorDetail Detail { get; set; } = null;
        
    }
}