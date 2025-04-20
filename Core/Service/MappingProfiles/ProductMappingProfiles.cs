using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DomainLayer.Models;
using Service.MappingProfiles;
using Shared.DataTransferObjects;

namespace Service.Profiles
{
    public class ProductMappingProfiles : Profile
    {
        public ProductMappingProfiles()
        {
            CreateMap<Product, ProductDto>()
                .ForMember(dest => dest.TypeName, options => options.MapFrom(src => src.ProductType.Name))
                .ForMember(dest => dest.BrandName, options => options.MapFrom(src => src.ProductBrand.Name))
                .ForMember(dest => dest.PictureUrl, options => options.MapFrom<PictureUrlResolver>());

            CreateMap<ProductBrand, BrandDto>();

            CreateMap<ProductType, TypeDto>();
        }
    }
}
