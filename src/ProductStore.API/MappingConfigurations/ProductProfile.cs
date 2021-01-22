using AutoMapper;
using ProductStore.API.ApiModels;
using ProductStore.Core.Models;
using System;

namespace ProductStore.API.MappingConfigurations
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<ProductCreateRequest, Product>()
                .ForMember(target => target.Id, config => config.MapFrom(source => Guid.NewGuid()));

            CreateMap<ProductUpdateRequest, Product>();
            CreateMap<Product, ProductViewModel>();
        }
    }
}