using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Database
{
    class IMDBContext : DbContext
    {
        public DbSet<Title> Titles { get; set; }

        public IMDBContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=imdb;Trusted_Connection=True;");
        }
    }
}
