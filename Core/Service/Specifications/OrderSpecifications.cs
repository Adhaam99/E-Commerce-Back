using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainLayer.Models.OrderModule;

namespace Service.Specifications
{
    class OrderSpecifications : BaseSpecification<Order, Guid>
    {
        // Get All Orders By Email

        public OrderSpecifications(string email) : base(O => O.UserEmail == email)
        {
            AddInclude(O => O.Items);
            AddInclude(O => O.DeliveryMethod);
            AddOrderByDescending(O => O.OrderDate);
        }

        // Get Order By Id

        public OrderSpecifications(Guid id) : base(O => O.Id == id)
        {
            AddInclude(O => O.Items);
            AddInclude(O => O.DeliveryMethod);
            AddOrderByDescending(O => O.OrderDate);
        }
    }
}
