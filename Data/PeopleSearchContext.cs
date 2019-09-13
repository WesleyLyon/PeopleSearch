using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace PeopleSearch.Models
{
    public class PeopleSearchContext : DbContext
    {
        public PeopleSearchContext (DbContextOptions<PeopleSearchContext> options)
            : base(options)
        {
        }

        public DbSet<PeopleSearch.Models.Person> Person { get; set; }
    }
}
