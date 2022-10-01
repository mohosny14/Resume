using Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastrucure
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Owner>().Property(x => x.Id).HasDefaultValueSql("NEWID()"); // to set Guid Id ==> Idenetity Increcse Autmatio
            /*
             * we don't need that if we set Id as int instead of Guid
             */

            modelBuilder.Entity<Projects>().Property(x => x.Id).HasDefaultValueSql("NEWID()");


            // add Initial Data for Owner Table
            #region Initial_Data
            modelBuilder.Entity<Owner>().HasData(
                new Owner
                {
                    Id = Guid.NewGuid(),
                    Name = "Mohamed Hosny",
                    Image = "mo_img.jpg",
                    Title = ".NET Full stack Developer"
                }
                );
            #endregion
        }

        public DbSet<Owner> owners { get; set; }

        public DbSet<Projects> projects { get; set; }
    }
}
