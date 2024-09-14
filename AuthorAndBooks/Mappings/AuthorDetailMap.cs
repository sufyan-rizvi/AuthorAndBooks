using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AuthorAndBooks.Models;
using FluentNHibernate.Mapping;
using NHibernate.Mapping.ByCode.Impl;

namespace AuthorAndBooks.Mappings
{
    public class AuthorDetailMap:ClassMap<AuthorDetail>
    {
        public AuthorDetailMap()
        {
            Table("Details");
            Id(d => d.Id).GeneratedBy.Identity();
            Map(d=> d.Street);
            Map(d=> d.City);
            Map(d=> d.State);
            Map(d=> d.Country);
            References(c => c.Author).Column("AuthorId").Nullable().Cascade.None();
        }
    }
}