using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DomainLayer.Models.IdentityModule;
using DomainLayer.Models.OrderModule;
using Shared.DataTransferObjects.IdentityDtos;
using Shared.OrderDtos;

namespace Service.MappingProfiles
{
    public class OrderMappingProfile : Profile
    {
        public OrderMappingProfile() 
        {
            CreateMap<AddressDto, OrderAddress>().ReverseMap();
            CreateMap<OrderToReturnDto, Order>().ReverseMap();
        }
    }
}
