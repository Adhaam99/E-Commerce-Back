using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DomainLayer.Models.OrderModule;
using DomainLayer.Models.ProductModule;
using Microsoft.Extensions.Configuration;
using Shared.DataTransferObjects.PrdouctModuleDtos;
using Shared.OrderDtos;

namespace Service.MappingProfiles
{
    class OrderItemPictureUrlResolver(IConfiguration _configuration) : IValueResolver<OrderItem, OrderItemDto, string>
    {
        public string Resolve(OrderItem source, OrderItemDto destination, string destMember, ResolutionContext context)
        {
            if (string.IsNullOrEmpty(source.Product.PictureUrl))
                return string.Empty;
            else
            {
                var Url = $"{_configuration.GetSection("Urls")["BaseUrl"]}{source.Product.PictureUrl}";
                return Url;
            }
        }
    }
}
