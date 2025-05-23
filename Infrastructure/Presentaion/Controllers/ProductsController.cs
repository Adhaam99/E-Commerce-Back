
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Presentation.Attributes;
using ServiceAbstraction;
using Shared;
using Shared.DataTransferObjects.PrdouctModuleDtos;

namespace Presentation.Controllers
{
    public class ProductsController(IServiceManager _serviceManager) : ApiBaseController
    {
        // Get All Products
        // GET: BaseUrl/api/products
        [HttpGet]
        [Cache]
        public async Task<ActionResult<PaginatedResult<ProductDto>>> GetAllProducts([FromQuery] ProductQueryParams queryParams)
        {
            var products = await _serviceManager.ProductService.GetAllProductsAsync(queryParams);
            return Ok(products);
        }

        // Get Product By Id
        // GET: BaseUrl/api/products/{id}
        [HttpGet("{id:int}")]
        public async Task<ActionResult<ProductDto>> GetProductById(int id)
        {
            var product = await _serviceManager.ProductService.GetProductByIdAsync(id);

            if(product is not null)
            {
                return Ok(product);
            }
            return NotFound();
        }


        // Get Product Types
        // GET: BaseUrl/api/products/types
        [HttpGet("types")]
        public async Task<ActionResult<IEnumerable<TypeDto>>> GetAllProductTypes()
        {
            var types = await _serviceManager.ProductService.GetAllProductTypesAsync();
            return Ok(types);
        }


        // Get Product Brands
        // GET: BaseUrl/api/products/brands
        [HttpGet("brands")]
        public async Task<ActionResult<IEnumerable<BrandDto>>> GetAllProductBrands()
        {
            var brands = await _serviceManager.ProductService.GetAllProductBrandsAsync();
            return Ok(brands);
        }

    }
}
