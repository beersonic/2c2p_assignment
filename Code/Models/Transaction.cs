using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;

namespace CustomerService.Models
{
    public enum StatusEnum
    {
        Success = 1,
        Failed = 2,
        Canceled = 3
    }
    public partial class Transaction
    {
        public int TransactionId { get; set; }
        public DateTime TransactionDateTime { get; set; }
        public double Amount { get; set; }
        public string CurrencyCode { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public StatusEnum? Status { get; set; }
        public int? CustomerId { get; set; }
    }
}
