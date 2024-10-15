using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Entites.OrderAggregate
{
    public class PaymentSummary
    {
        public int Last4 { get; set; }
        public required string Brand { get; set; }
        public int ExpMonth { get; set; }
        public int ExpYear { get; set; }
    }
}