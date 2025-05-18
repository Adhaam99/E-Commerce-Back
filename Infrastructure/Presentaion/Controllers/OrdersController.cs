using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using DomainLayer.Models.OrderModule;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceAbstraction;
using Shared.OrderDtos;

namespace Presentation.Controllers
{
    [Authorize]
    public class OrdersController(IServiceManager _serviceManager) : ApiBaseController
    {
        // Create Order
        [HttpPost]
        public async Task<ActionResult<OrderToReturnDto>> CreateOrder(OrderDto orderDto)
        {

            var Order = await _serviceManager.OrderService.CreateOrderAsync(orderDto, GetEmailFromToken());

            return Ok(Order);
        }

        // Get Delivery Methods
        [AllowAnonymous]
        [HttpGet("DeliveryMethods")]
        public async Task<ActionResult<IEnumerable<DelivreyMethodDto>>> GetDeliverMethods()
        {
            var deliveryMethods = await _serviceManager.OrderService.GetDeliveryMethodsAsync();
            return Ok(deliveryMethods);
        }

        // Get All Orders By Email
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderToReturnDto>>> GetAllOrdersByEmail()
        {
            var Orders = await _serviceManager.OrderService.GetAllOrdersAsync(GetEmailFromToken());
            return Ok(Orders);
        }

        // Get Order by Id
        [HttpGet("{id:Guid}")]
        public async Task<ActionResult<IEnumerable<OrderToReturnDto>>> GetOrderById(Guid id)
        {
            var Orders = await _serviceManager.OrderService.GetAllOrdersAsync(GetEmailFromToken());
            return Ok(Orders);
        }
    }
}
