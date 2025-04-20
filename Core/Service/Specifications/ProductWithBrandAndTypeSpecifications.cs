using DomainLayer.Models;

namespace Service.Specifications
{
    internal class ProductWithBrandAndTypeSpecifications : BaseSpecification<Product, int>
    {
        public ProductWithBrandAndTypeSpecifications(int? brandId, int? typeId)
            : base(P => (!brandId.HasValue || P.BrandId == brandId) &&
            (!typeId.HasValue || P.TypeId == typeId))
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
