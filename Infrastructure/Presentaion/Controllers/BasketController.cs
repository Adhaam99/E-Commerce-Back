using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ServiceAbstraction;
using Shared.DataTransferObjects.BasketModuleDtos;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BasketController(IServiceManager _serviceManager) : ControllerBase
    {

        // Create OR Update a new basket

        [HttpPost] // POST: api/basket
        public async Task<ActionResult<BasketDto>> CreateOrUpdateBasket(BasketDto basket)
        {
            var Basket = await _serviceManager.BasketService.CreatedOrUpdateBasketAsync(basket);
            return Ok(Basket);
        }

        // Get Basket

        [HttpGet] // GET: api/basket/Key
        public async Task<ActionResult<BasketDto>> GetBasket(string key)
        {
            var Basket = await _serviceManager.BasketService.GetBasketAsync(key);
            return Ok(Basket);
        }


        // Delete a basket

        [HttpDelete("{key}")] // DELETE: api/basket
        public async Task<ActionResult<bool>> DeleteBasket(string key)
        {
            var Result = await _serviceManager.BasketService.DeleteBasketAsync(key);
            return Ok(Result);
        }
    }
}
