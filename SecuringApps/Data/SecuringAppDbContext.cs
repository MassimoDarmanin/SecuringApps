using Microsoft.EntityFrameworkCore;
using SecuringApps.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SecuringApps.Data
{
    public class SecuringAppDbContext : DbContext
    {
        public SecuringAppDbContext(DbContextOptions<SecuringAppDbContext> options) : base(options)
        {

        }

        public DbSet<Tasked> Taskeds { get; set; }
        public DbSet<File> Files { get; set; }
        public DbSet<Comment> Comments { get; set; }        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Tasked>().Property(x => x.Id).HasDefaultValueSql("NEWID()");

            modelBuilder.Entity<File>().Property(x => x.Id).HasDefaultValueSql("NEWID()");

            modelBuilder.Entity<Comment>().Property(x => x.Id).HasDefaultValueSql("NEWID()");

            //for the property ID of the product, it should have a default value by a default function called NEWID
            //Generatres a new GUID
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
        }
    }
}
