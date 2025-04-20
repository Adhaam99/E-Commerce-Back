using DomainLayer.Models;
using Shared;

namespace Service.Specifications
{
    internal class ProductWithBrandAndTypeSpecifications : BaseSpecification<Product, int>
    {
        public ProductWithBrandAndTypeSpecifications(int? brandId, int? typeId, ProductSortingOptions sortingOptions)
            : base(P => (!brandId.HasValue || P.BrandId == brandId) &&
            (!typeId.HasValue || P.TypeId == typeId))
        {
            AddInclude(P => P.ProductBrand);
            AddInclude(P => P.ProductType);

            switch (sortingOptions)
            {
                case ProductSortingOptions.NameAsc:
                    AddOrderBy(P => P.Name);
                    break;
                case ProductSortingOptions.NameDesc:
                    AddOrderByDescending(P => P.Name);
                    break;
                case ProductSortingOptions.PriceAsc:
                    AddOrderBy(P => P.Price);
                    break;
                case ProductSortingOptions.PriceDesc:
                    AddOrderByDescending(P => P.Price);
                    break;
                default:
                    break;
            }
        }
        public ProductWithBrandAndTypeSpecifications(int id) : base(P => P.Id == id)
        {
            AddInclude(P => P.ProductBrand);
            AddInclude(P => P.ProductType);
        }
    }
}
