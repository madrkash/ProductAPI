using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProductStore.UnitTests.Builders;

namespace ProductStore.UnitTests.MappingConfigurations.DataSource
{
    public class ProductOptionCreateRequestMapperDataSource : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[]
            {
                new ProductOptionCreateRequestBuilder()
                    .WithDefaultValues()
                    .Build()
            };
            yield return new object[]
            {
                new ProductOptionCreateRequestBuilder()
                    .WithName("Test Name")
                    .WithDescription("Test Description")
                    .Build()
            };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }

    public class ProductOptionUpdateRequestMapperDataSource : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[]
            {
                new ProductOptionUpdateRequestBuilder()
                    .WithDefaultValues()
                    .Build()
            };
            yield return new object[]
            {
                new ProductOptionUpdateRequestBuilder()
                    .WithId(Guid.NewGuid())
                    .WithName("Test Name")
                    .WithDescription("Test Description")
                    .Build()
            };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }

    public class ProductOptionViewModelMapperDataSource : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[]
            {
                new ProductOptionBuilder()
                    .WithDefaultValues()
                    .Build()
            };
            yield return new object[]
            {
                new ProductOptionBuilder()
                    .WithId(Guid.NewGuid())
                    .WithName("Test Name")
                    .WithDescription("Test Description")
                    .Build()
            };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}