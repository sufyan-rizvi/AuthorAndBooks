using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AuthorAndBooks.Models;
using FluentNHibernate.Mapping;

namespace AuthorAndBooks.Mappings
{
    public class AuthorMap : ClassMap<Author>
    {
        public AuthorMap()
        {
            Table("Authors");
            Id(a=>a.Id).GeneratedBy.GuidComb();
            Map(a => a.Password);
            Map(a => a.Name);
            Map(a => a.Age);
            Map(a => a.Email);
            HasOne(a=>a.Detail).Cascade.All().PropertyRef(u=>u.Author);
            HasMany(a => a.Books).Inverse().Cascade.All();
        }
    }
}