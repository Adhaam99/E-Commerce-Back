using E_Commerce.Web.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.Web.Controllers
{
    [Route("api/[controller]")] // BaseURL/api/product
    [ApiController]
    public class ProductController : ControllerBase
    {
        // GET: BaseURL/api/product
        [HttpGet("{id}")]
        public ActionResult<Product> Get(int id)
        {
            return new Product() { Id = id }; 
        }
        [HttpGet]
        public ActionResult<Product> GetAll(int id)
        {
            return new Product() { Id = 100 };
        }
        [HttpPost]
        public ActionResult<Product> AddProduct(Product product)
        {
            return new Product() ;
        }
        [HttpPut]
        public ActionResult<Product> UpdateProduct(Product product)
        {
            return new Product() ;
        }
        [HttpDelete]
        public ActionResult<Product> DeleteProduct(Product product)
        {
            return new Product() ;
        }
    }
}
