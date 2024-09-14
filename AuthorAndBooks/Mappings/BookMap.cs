using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AuthorAndBooks.Models;
using FluentNHibernate.Mapping;

namespace AuthorAndBooks.Mappings
{
    public class BookMap : ClassMap<Book>
    {
        public BookMap()
        {
            Table("Books");
            Id(b => b.Id).GeneratedBy.Identity();
            Map(b => b.Name);
            Map(b => b.Description);
            Map(b => b.Genre);
            References(c => c.Author).Column("AuthorId").Nullable().Cascade.None();
        }
    }
}