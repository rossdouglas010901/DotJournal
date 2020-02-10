using DotJournal.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotJournal.Data
{
    public class JournalDbContext : DbContext
    {
        public JournalDbContext(DbContextOptions<JournalDbContext> options)
            : base(options)
        {
        }

        public DbSet<Book> books { get; set; }
        public DbSet<Page> pages { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>()
                .Property(p => p.dateCreated)
                .HasDefaultValueSql("getDate()");

            modelBuilder.Entity<Page>()
               .Property(p => p.dateCreated)
               .HasDefaultValueSql("getDate()");



            modelBuilder.Entity<Book>()
                .Property(p => p.dateUpdated)
                 .HasDefaultValueSql("getDate()");
            modelBuilder.Entity<Page>()

               .Property(p => p.dateUpdated)
               .HasDefaultValueSql("getDate()");
        }
    }
}
