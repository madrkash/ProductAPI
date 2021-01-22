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
    public class ProductProfileTests
    {
        [Theory]
        [ClassData(typeof(ProductCreateRequestMapperDataSource))]
        public void Map_ProductCreateRequest_To_Product_Should_Work(ProductCreateRequest data)
        {
            var mapper = new MapperFixture().Mapper;
            var result = mapper.Map<Product>(data);

            Assert.NotNull(result);
            Assert.True(Guid.TryParse(result.Id.ToString(), out _));
        }

        [Theory]
        [ClassData(typeof(ProductUpdateRequestMapperDataSource))]
        public void Map_ProductUpdateRequest_To_Product_Should_Work(ProductUpdateRequest data)
        {
            var mapper = new MapperFixture().Mapper;
            var result = mapper.Map<Product>(data);

            Assert.NotNull(result);
            Assert.Equal(result.Id, data.Id);
        }

        [Theory]
        [ClassData(typeof(ProductViewModelMapperDataSource))]
        public void Map_Product_To_ProductViewModel_Should_Work(Product data)
        {
            var mapper = new MapperFixture().Mapper;
            var result = mapper.Map<ProductViewModel>(data);

            Assert.NotNull(result);
            Assert.True(result.Equals(data));
        }
    }
}