using ProductStore.Tests.Common.Builders;
using ProductStore.UnitTests.Builders;
using System;
using System.Collections;
using System.Collections.Generic;

namespace ProductStore.UnitTests.MappingConfigurations.DataSource
{
    public class ProductCreateRequestMapperDataSource : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[]
            {
                new ProductCreateRequestBuilder()
                    .WithDefaultValues()
                    .Build()
            };
            yield return new object[]
            {
                new ProductCreateRequestBuilder()
                    .WithName("Test Name")
                    .WithDescription("Test Description")
                    .Build()
            };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }

    public class ProductUpdateRequestMapperDataSource : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[]
            {
                new ProductUpdateRequestBuilder()
                    .WithDefaultValues()
                    .Build()
            };
            yield return new object[]
            {
                new ProductUpdateRequestBuilder()
                    .WithId(Guid.NewGuid())
                    .WithName("Test Name")
                    .WithDescription("Test Description")
                    .Build()
            };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }

    public class ProductViewModelMapperDataSource : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[]
            {
                new ProductBuilder()
                    .WithDefaultValues()
                    .Build()
            };
            yield return new object[]
            {
                new ProductBuilder()
                    .WithId(Guid.NewGuid())
                    .WithName("Test Name")
                    .WithDescription("Test Description")
                    .Build()
            };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}