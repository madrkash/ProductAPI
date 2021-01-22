using Moq;
using ProductStore.Core.Contracts;
using ProductStore.Core.Services;

namespace ProductStore.UnitTests.Fixtures
{
    public class ProductServiceFixture
    {
        public Mock<IProductRepository> MockProductRepository { get; }
        public Mock<IProductOptionService> MockProductOptionService { get; }

        public ProductServiceFixture()
        {
            MockProductRepository = new Mock<IProductRepository>();
            MockProductOptionService = new Mock<IProductOptionService>();
        }

        public ProductService Sut()
        {
            return new ProductService(
                MockProductRepository.Object,
                MockProductOptionService.Object);
        }
    }
}