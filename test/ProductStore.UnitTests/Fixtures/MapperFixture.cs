using AutoMapper;
using Moq;
using ProductStore.API.MappingConfigurations;
using ProductStore.Core.Contracts;
using ProductStore.Core.Services;

namespace ProductStore.UnitTests.Fixtures
{
    public class MapperFixture
    {
        public IMapper Mapper { get; }

        public MapperFixture()
        {
            var config = new MapperConfiguration(opts =>
            {
                opts.AddProfile<ProductOptionProfile>();
                opts.AddProfile<ProductProfile>();
            });

            Mapper = config.CreateMapper();
        }
    }
}