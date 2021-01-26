using AutoMapper;
using ProductStore.API.MappingConfigurations;

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