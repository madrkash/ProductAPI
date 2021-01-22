using Moq;
using ProductStore.Core.Contracts;
using ProductStore.Core.Services;

namespace ProductStore.UnitTests.Fixtures
{
    public class ProductOptionServiceFixture
    {
        public Mock<IProductOptionRepository> MockProductOptionRepository { get; }

        public ProductOptionServiceFixture()
        {
            MockProductOptionRepository = new Mock<IProductOptionRepository>();
        }

        public ProductOptionService Sut()
        {
            return new ProductOptionService(MockProductOptionRepository.Object);
        }
    }
}