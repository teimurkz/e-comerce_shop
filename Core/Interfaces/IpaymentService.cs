using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entites;

namespace Core.Interfaces
{
    public interface IpaymentService
    {
        Task<ShoppingCart?> CreateOrUpdatePaymentsIntent(string cartId);
    }
}