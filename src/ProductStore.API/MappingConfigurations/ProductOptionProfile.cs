using AutoMapper;
using ProductStore.API.Dtos;
using ProductStore.Core.Models;
using System;

namespace ProductStore.API.MappingConfigurations
{
    public class ProductOptionProfile : Profile
    {
        public ProductOptionProfile()
        {
            CreateMap<ProductOptionCreateRequestDto, ProductOption>()
                .ForMember(target => target.Id,
                    config => config.MapFrom(source => Guid.NewGuid()));

            CreateMap<ProductOptionUpdateRequestDto, ProductOption>();
            CreateMap<ProductOption, ProductOptionResponseDto>();
        }
    }
}