using AutoMapper;
using ProductStore.Core.Models;
using System;
using ProductStore.API.Dtos;

namespace ProductStore.API.MappingConfigurations
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<ProductCreateRequestDto, Product>()
                .ForMember(target => target.Id, config => config.MapFrom(source => Guid.NewGuid()));

            CreateMap<ProductUpdateRequestDto, Product>();
            CreateMap<Product, ProductResponseDto>();
        }
    }
}