using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AuthorAndBooks.Models
{
    public class AuthorDetail
    {
        public virtual int Id { get; set; }
        [Required]
        public virtual string Street { get; set; }
        [Required]
        public virtual string City { get; set; }
        [Required]
        public virtual string State { get; set; }
        [Required]
        public virtual string Country { get; set; }
        public virtual Author Author { get; set; }
    }
}