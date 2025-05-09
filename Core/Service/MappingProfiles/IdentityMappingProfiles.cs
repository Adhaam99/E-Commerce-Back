using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DomainLayer.Models.IdentityModule;
using Shared.DataTransferObjects.IdentityDtos;

namespace Service.MappingProfiles
{
    public class IdentityMappingProfiles : Profile
    {
        public IdentityMappingProfiles()
        {
            CreateMap<Address , AddressDto>().ReverseMap();
        }
    }
}
