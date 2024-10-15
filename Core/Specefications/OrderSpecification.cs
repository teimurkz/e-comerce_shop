using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entites.OrderAggregate;

namespace Core.Specefications
{
    public class OrderSpecification : BaseSpecefication<Order>
    {
        public OrderSpecification(string email) : base(x => x.BuyerEmail == email)
        {
            AddInclude(x => x.OrderItems);
            AddInclude(x => x.DeliveryMethod);
            AddOrderByDescending(x => x.OrderDate);
        }
        public OrderSpecification(string email, int id) : base(x => x.BuyerEmail == email && x.Id == id)
        {
            AddInclude("OrderItems");
            AddInclude("DeliveryMethod");
        }
    }
}