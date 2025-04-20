using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ServiceAbstraction;
using Shared;
using Shared.DataTransferObjects;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[Controller]")] // BaseUrl/api/products
    public class ProductsController(IServiceManager _serviceManager) : ControllerBase
    {
        // Get All Products
        // GET: BaseUrl/api/products
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetAllProducts(int? brandId, int? typeId, ProductSortingOptions sortingOptions)
        {
            var products = await _serviceManager.ProductService.GetAllProductsAsync(brandId, typeId, sortingOptions);
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
