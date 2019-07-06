using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;

namespace WebApplication1.Models
{

    public class Customer
    {
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string ContactEmail { get; set; }
        public string MobileNo { get; set; }
        public ICollection<Transaction> Transactions { get; set; }
    }

    public class Transaction
    {
        public int TransactionId { get; set; }
        public DateTime TransactionDateTime { get; set; }
        public double Amount { get; set; }
        public string CurrencyCode { get; set; }
        public string Status { get; set; }
    }
}
