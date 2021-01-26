using FluentAssertions;
using ProductStore.API.Dtos;
using ProductStore.Core.Models;
using ProductStore.UnitTests.Fixtures;
using ProductStore.UnitTests.MappingConfigurations.DataSource;
using System;
using Xunit;

namespace ProductStore.UnitTests.MappingConfigurations
{
    public class ProductProfileTests
    {
        [Theory]
        [ClassData(typeof(ProductCreateRequestMapperDataSource))]
        public void Map_ProductCreateRequest_To_Product_Should_Work(ProductCreateRequestDto data)
        {
            var mapper = new MapperFixture().Mapper;
            var result = mapper.Map<Product>(data);

            result.Should().NotBeNull();
            Guid.TryParse(result.Id.ToString(), out _).Should().BeTrue();
        }

        [Theory]
        [ClassData(typeof(ProductUpdateRequestMapperDataSource))]
        public void Map_ProductUpdateRequest_To_Product_Should_Work(ProductUpdateRequestDto data)
        {
            var mapper = new MapperFixture().Mapper;
            var result = mapper.Map<Product>(data);

            result.Should().NotBeNull();
            result.Id.Should().Be(data.Id);
        }

        [Theory]
        [ClassData(typeof(ProductViewModelMapperDataSource))]
        public void Map_Product_To_ProductViewModel_Should_Work(Product data)
        {
            var mapper = new MapperFixture().Mapper;
            var result = mapper.Map<ProductResponseDto>(data);

            result.Should().NotBeNull();
            result.Should().BeEquivalentTo(data);
        }
    }
}