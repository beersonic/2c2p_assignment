﻿using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

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
        
        public Decimal Amount { get; set; }
        [MaxLength(3)]
        [MinLength(3)]
        public string CurrencyCode { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public StatusEnum? Status { get; set; }
        public int? CustomerId { get; set; }
    }
}
