using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AuthorAndBooks.Models;

namespace AuthorAndBooks.ViewModels
{
    public class AuthorBookVM
    {
        public IList<Author> Authors { get; set; }
        public IList<Book> Books { get; set; }
        
    }
}