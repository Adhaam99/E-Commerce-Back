using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared;
using Shared.DataTransferObjects;

namespace ServiceAbstraction
{
    public interface IProductService
    {
        // Get All Products

        Task<IEnumerable<ProductDto>> GetAllProductsAsync(ProductQueryParams queryParams);

        // Get Product By Id

        Task<ProductDto> GetProductByIdAsync(int id);

        // Get All ProductBrands
        Task<IEnumerable<BrandDto>> GetAllProductBrandsAsync();

        // Get All ProductTypes
        Task<IEnumerable<TypeDto>> GetAllProductTypesAsync();

    }
}
