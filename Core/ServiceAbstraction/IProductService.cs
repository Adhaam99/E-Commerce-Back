using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared;

namespace ServiceAbstraction
{
    public interface IProductService
    {
        // Get All Products

        Task<IEnumerable<ProductDto>> GetAllProductsAsync(int? brandId, int? typeId);

        // Get Product By Id

        Task<ProductDto> GetProductByIdAsync(int id);

        // Get All ProductBrands
        Task<IEnumerable<BrandDto>> GetAllProductBrandsAsync();

        // Get All ProductTypes
        Task<IEnumerable<TypeDto>> GetAllProductTypesAsync();

    }
}
