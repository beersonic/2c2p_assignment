using CustomerService.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace TestDB2C2P
{
    public class DbContextMocker
    {
        public static DB2C2PContext GetMockedDB2C2PContext(string dbName)
        {
            var options = new DbContextOptionsBuilder<DB2C2PContext>()
                .UseInMemoryDatabase(databaseName: dbName)
                .Options;
                /*
            var options = new DbContextOptionsBuilder<DB2C2PContext>()
                .UseSqlite("DataSource=:memory:")
                //.UseSqlite("Data Source = unittest.db")
                .Options;
                */
            var dbContext = new DB2C2PContext(options);
            //dbContext.Database.Migrate();
            dbContext.Database.EnsureCreated();
            dbContext.Seed();

            return dbContext;
        }
    }
}
