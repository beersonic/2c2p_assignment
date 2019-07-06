using System;
using System.Linq;
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

        public void Seed()
        {
            this.Customer.Add(new Models.Customer
            { CustomerId = 1, ContactEmail = "beer1@gmail.com", CustomerName = "beer1", MobileNo = "0811111111" });

            this.Customer.Add(new Models.Customer
            { CustomerId = 2, ContactEmail = "beer2@gmail.com", CustomerName = "beer2", MobileNo = "0822222222" });

            this.Transaction.Add(new Models.Transaction
            { TransactionId = 1, TransactionDateTime = DateTime.Now, Amount = new decimal(100.01), CurrencyCode = "USD", Status = StatusEnum.Success, CustomerId = 1 });

            this.Transaction.Add(new Models.Transaction
            { TransactionId = 2, TransactionDateTime = DateTime.Now, Amount = new decimal(100.001), CurrencyCode = "THB", Status = StatusEnum.Canceled, CustomerId = 1 });

            this.Transaction.Add(new Models.Transaction
            { TransactionId = 3, TransactionDateTime = DateTime.Now, Amount = new decimal(100.005), CurrencyCode = "CNY", Status = StatusEnum.Success, CustomerId = 1 });

            this.Transaction.Add(new Models.Transaction
            { TransactionId = 4, TransactionDateTime = DateTime.Now, Amount = new decimal(100.0049), CurrencyCode = "GBP", Status = StatusEnum.Failed, CustomerId = 2 });
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

            foreach (var property in modelBuilder.Model.GetEntityTypes()
                .SelectMany(t => t.GetProperties())
                .Where(p => p.ClrType == typeof(decimal)))
                {
                    property.Relational().ColumnType = "decimal(18, 2)";
                }
            /*
            modelBuilder.Entity<Customer>().HasData(new Customer { CustomerId = 1, ContactEmail = "beer1@gmail.com", CustomerName = "beer1", MobileNo = "0811111111" });
            modelBuilder.Entity<Customer>().HasData(new Customer { CustomerId = 2, ContactEmail = "beer2@gmail.com", CustomerName = "beer2", MobileNo = "0812222222" });
            modelBuilder.Entity<Customer>().HasData(new Customer { CustomerId = 3, ContactEmail = "beer3@gmail.com", CustomerName = "beer3", MobileNo = "0813333333" });
            modelBuilder.Entity<Customer>().HasData(new Customer { CustomerId = 4, ContactEmail = "beer4@gmail.com", CustomerName = "beer4", MobileNo = "0814444444" });
            */

            base.OnModelCreating(modelBuilder);
        }
    }
}
