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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Country>().HasData(
                new Country { Id =1, Name = "US", Population = 328200000, SickCount = 1647741, DeadCount = 97811, RecoveredCount = 376266, Vaccine = false }
                );
            modelBuilder.Entity<Human>().HasData(
                new Human { Id = 1, FirstName = "Obi-wan", LastName = "Kenobi", Age = 38, IsSick = false, Gender = "Male", CountryId = 1 },
                new Human { Id = 2, FirstName = "Sanwise", LastName = "Gamgee", Age = 54, IsSick = false, Gender = "Male", CountryId = 1 }
                );
        }
    }
}
