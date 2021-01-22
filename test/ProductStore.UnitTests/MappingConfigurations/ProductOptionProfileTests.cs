using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProductStore.API.ApiModels;
using ProductStore.Core.Models;
using ProductStore.UnitTests.Fixtures;
using ProductStore.UnitTests.MappingConfigurations.DataSource;
using Xunit;

namespace ProductStore.UnitTests.MappingConfigurations
{
    public class ProductOptionProfileTests
    {
        [Theory]
        [ClassData(typeof(ProductOptionCreateRequestMapperDataSource))]
        public void Map_ProductOptionCreateRequest_To_ProductOption_Should_Work(ProductOptionCreateRequest data)
        {
            var mapper = new MapperFixture().Mapper;
            var result = mapper.Map<ProductOption>(data);

            Assert.NotNull(result);
            Assert.True(Guid.TryParse(result.Id.ToString(), out _));
        }

        [Theory]
        [ClassData(typeof(ProductOptionUpdateRequestMapperDataSource))]
        public void Map_ProductUpdateRequest_To_ProductOption_Should_Work(ProductOptionUpdateRequest data)
        {
            var mapper = new MapperFixture().Mapper;
            var result = mapper.Map<ProductOption>(data);

            Assert.NotNull(result);
            Assert.Equal(result.Id, data.Id);
        }

        [Theory]
        [ClassData(typeof(ProductOptionViewModelMapperDataSource))]
        public void Map_ProductOption_To_ProductOptionViewModel_Should_Work(ProductOption data)
        {
            var mapper = new MapperFixture().Mapper;
            var result = mapper.Map<ProductOptionViewModel>(data);

            Assert.NotNull(result);
            Assert.True(result.Equals(data));
        }
    }
}