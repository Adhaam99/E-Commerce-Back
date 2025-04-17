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

        Task<IEnumerable<ProductDto>> GetAllProducts();

        // Get Product By Id

        Task<ProductDto> GetProductById(int id);

        // Get All ProductBrands
        Task<IEnumerable<BrandDto>> GetAllProductBrands();

        // Get All ProductTypes
        Task<IEnumerable<TypeDto>> GetAllProductTypes();

    }
}
