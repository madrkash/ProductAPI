using FluentAssertions;
using ProductStore.API.Dtos;
using ProductStore.Core.Models;
using ProductStore.UnitTests.Fixtures;
using ProductStore.UnitTests.MappingConfigurations.DataSource;
using System;
using Xunit;

namespace ProductStore.UnitTests.MappingConfigurations
{
    public class ProductOptionProfileTests
    {
        [Theory]
        [ClassData(typeof(ProductOptionCreateRequestMapperDataSource))]
        public void Map_ProductOptionCreateRequest_To_ProductOption_Should_Work(ProductOptionCreateRequestDto data)
        {
            var mapper = new MapperFixture().Mapper;
            var result = mapper.Map<ProductOption>(data);

            result.Should().NotBeNull();
            Guid.TryParse(result.Id.ToString(), out _).Should().BeTrue();
        }

        [Theory]
        [ClassData(typeof(ProductOptionUpdateRequestMapperDataSource))]
        public void Map_ProductUpdateRequest_To_ProductOption_Should_Work(ProductOptionUpdateRequestDto data)
        {
            var mapper = new MapperFixture().Mapper;
            var result = mapper.Map<ProductOption>(data);

            result.Should().NotBeNull();
            result.Id.Should().Be(data.Id);
        }

        [Theory]
        [ClassData(typeof(ProductOptionViewModelMapperDataSource))]
        public void Map_ProductOption_To_ProductOptionViewModel_Should_Work(ProductOption data)
        {
            var mapper = new MapperFixture().Mapper;
            var result = mapper.Map<ProductOptionResponseDto>(data);

            result.Should().NotBeNull();
            result.Should().BeEquivalentTo(data);
        }
    }
}