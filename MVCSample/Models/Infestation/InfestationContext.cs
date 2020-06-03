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
        public DbSet<WorldPart> WorldParts { get; set; }
        public DbSet<News> News { get; set; }

        public InfestationContext(DbContextOptions options):base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<WorldPart>().HasData(
                new WorldPart[] {
                    new WorldPart{ Id = 1, Name = "Africa"},
                    new WorldPart{ Id = 2, Name = "Eurasia"},
                    new WorldPart{ Id = 3, Name = "North America"},
                    new WorldPart{ Id = 4, Name = "South America"},
                    new WorldPart{ Id = 5, Name = "Antarctica"},
                    new WorldPart{ Id = 6, Name = "Australia"}
                });
        }
    }
}
