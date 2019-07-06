using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CustomerService.Models
{
    public partial class Customer
    {
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        [EmailAddress]
        public string ContactEmail { get; set; }
        public string MobileNo { get; set; }
        public ICollection<Transaction> Transactions { get; set; }
    }
}
