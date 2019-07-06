using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace CustomerService.Models
{
    public partial class DB2C2PContext : DbContext
    {
        public DB2C2PContext()
        {
        }

        public DB2C2PContext(DbContextOptions<DB2C2PContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Customer> Customer { get; set; }
        public virtual DbSet<Transaction> Transaction { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlite("Data Source=DB2C2P.db");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.4-servicing-10062");

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.Property(e => e.CustomerId).ValueGeneratedNever();
            });

            modelBuilder.Entity<Transaction>(entity =>
            {
                entity.Property(e => e.TransactionId).ValueGeneratedNever();

                entity.Property(e => e.CurrencyCode).HasMaxLength(3);

                entity.Property(e => e.Status).HasMaxLength(8);
            });

            /*
            modelBuilder.Entity<Customer>().HasData(new Customer { CustomerId = 1, ContactEmail = "beer1@gmail.com", CustomerName = "beer1", MobileNo = "0811111111" });
            modelBuilder.Entity<Customer>().HasData(new Customer { CustomerId = 2, ContactEmail = "beer2@gmail.com", CustomerName = "beer2", MobileNo = "0812222222" });
            modelBuilder.Entity<Customer>().HasData(new Customer { CustomerId = 3, ContactEmail = "beer3@gmail.com", CustomerName = "beer3", MobileNo = "0813333333" });
            modelBuilder.Entity<Customer>().HasData(new Customer { CustomerId = 4, ContactEmail = "beer4@gmail.com", CustomerName = "beer4", MobileNo = "0814444444" });
            */
        }
    }
}
