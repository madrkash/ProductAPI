using AutoMapper;
using ProductStore.API.ApiModels;
using ProductStore.Core.Models;
using System;

namespace ProductStore.API.MappingConfigurations
{
    public class ProductOptionProfile : Profile
    {
        public ProductOptionProfile()
        {
            CreateMap<ProductOptionCreateRequest, ProductOption>()
                .ForMember(target => target.Id,
                    config => config.MapFrom(source => Guid.NewGuid()));

            CreateMap<ProductOptionUpdateRequest, ProductOption>();
            CreateMap<ProductOption, ProductOptionViewModel>();
        }
    }
}