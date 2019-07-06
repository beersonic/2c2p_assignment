using System;
using System.Collections.Generic;

namespace CustomerService.Models
{
    public partial class Transaction
    {
        public int TransactionId { get; set; }
        public DateTime TransactionDateTime { get; set; }
        public double Amount { get; set; }
        public string CurrencyCode { get; set; }
        public string Status { get; set; }
        public int? CustomerId { get; set; }
    }
}
