using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DomainLayer.Contracts;
using DomainLayer.Exceptions;
using DomainLayer.Models.BasketModule;
using DomainLayer.Models.OrderModule;
using DomainLayer.Models.ProductModule;
using ServiceAbstraction;
using Shared.OrderDtos;

namespace Service
{
    public class OrderService(IMapper _mapper, IBasketRepository _basketRepository, IUnitOfWork _unitOfWork) : IOrderService
    {
        public async Task<OrderToReturnDto> CreateOrderAsync(OrderDto orderDto, string email)
        {
            // Map Address To Order Address
            var orderAddress = _mapper.Map<OrderAddress>(orderDto.Address);

            // Get Basket
            var basket = await _basketRepository.GetCustomerBasketAsync(orderDto.BasketId) 
                ?? throw new BasketNotFoundException(orderDto.BasketId);

            // Create OrderItem List 
            List<OrderItem> orderItems = [];
            var ProductRepo = _unitOfWork.GetRepository<Product, int>();

            foreach (var item in basket.Items)
            {
                var Product = await ProductRepo.GetByIdAsync(item.Id)
                    ?? throw new ProductNotFoundException(item.Id);
                orderItems.Add(CreateOrderItem(item, Product));

            };

            // Get Delivry Method
            var deliveryMethod = await _unitOfWork.GetRepository<DeliveryMethod, int>()
                .GetByIdAsync(orderDto.DeliveryMethodId)
                ?? throw new DeliveryMethodNotFoundException(orderDto.DeliveryMethodId);

            // Calculate Sub Total
            var subTotal = orderItems.Sum(item => item.Price * item.Quantity);

            var Order = new Order(email, orderAddress, deliveryMethod, orderItems, subTotal);

            await _unitOfWork.GetRepository<Order, Guid>().AddAsync(Order);

            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<OrderToReturnDto>(Order);
        }

        private static OrderItem CreateOrderItem(BasketItem item, Product Product)
        {
            var orderItem = new OrderItem()
            {
                Product = new ProductItemOrdered() { ProductId = Product.Id, ProductName = Product.Name, PictureUrl = Product.PictureUrl },
                Price = Product.Price,
                Quantity = item.Quantity
            };

            return orderItem;
        }
    }
}
