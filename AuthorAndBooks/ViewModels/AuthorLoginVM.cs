using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AuthorAndBooks.ViewModels
{
    public class AuthorLoginVM
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Password { get; set; }

    }
}