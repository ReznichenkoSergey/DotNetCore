using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCSample.Models.Infestation
{
    public class InfestationContext : DbContext
    {
        public DbSet<Human> Humans { get; set; }
        public DbSet<Country> Countries { get; set; }

        public InfestationContext(DbContextOptions options):base(options)
        { }

    }
}
