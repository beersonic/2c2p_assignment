using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerService.Models
{
    public class CustomerRequest
    {
        [Range(1,9999999999)]
        public int? CustomerId { get; set; }
        [EmailAddress]
        [MaxLength(25)]
        public String Email { get; set; }
    }
}
