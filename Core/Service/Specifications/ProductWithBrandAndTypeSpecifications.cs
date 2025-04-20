using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainLayer.Models;

namespace Service.Specifications
{
    internal class ProductWithBrandAndTypeSpecifications : BaseSpecification<Product, int>
    {
        public ProductWithBrandAndTypeSpecifications(): base(null)
        {
            AddInclude(P => P.ProductBrand);
            AddInclude(P => P.ProductType);
        }
        public ProductWithBrandAndTypeSpecifications(int id) : base(P => P.Id == id)
        {
            AddInclude(P => P.ProductBrand);
            AddInclude(P => P.ProductType);
        }
    }
}
