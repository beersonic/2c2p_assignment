using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CustomerService.Models
{
    public partial class Customer
    {
        [Range(1,9999999999)]
        public int CustomerId { get; set; }
        [MaxLength(30)]
        public string CustomerName { get; set; }
        [EmailAddress]
        [MaxLength(25)]
        public string ContactEmail { get; set; }
        [Range(1000000000, 9999999999)]
        public string MobileNo { get; set; }
        public ICollection<Transaction> Transactions { get; set; }
        
    }
}
