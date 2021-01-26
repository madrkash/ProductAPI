using Moq;
using ProductStore.Core.Models;
using ProductStore.UnitTests.Fixtures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using ProductStore.Core.Exceptions;
using ProductStore.Tests.Common.Builders;
using Xunit;

namespace ProductStore.UnitTests.Services
{
    public class ProductServiceTests
    {
        [Fact]
        public async Task GetByIdAsync_ReturnsProductFromDb_GivenValidProductId()
        {
            // Arrange
            var fixture = new ProductServiceFixture();
            var productId = Guid.NewGuid();
            var expectedProduct = new ProductBuilder()
                .WithDefaultValues()
                .WithId(productId)
                .Build();

            fixture.MockProductRepository.Setup(
                x => x.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(expectedProduct);

            // Act
            var result = await fixture.Sut().GetByIdAsync(productId);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeEquivalentTo(expectedProduct);
            fixture.MockProductRepository.Verify(
                x => x.GetByIdAsync(productId), Times.Once());
            fixture.MockProductRepository.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task GetByIdAsync_ThrowsProductNotFoundException_GivenNonExistentProductId()
        {
            // Arrange
            var fixture = new ProductServiceFixture();
            var productId = Guid.NewGuid();

            fixture.MockProductRepository.Setup(
                x => x.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync((Product)null);

            // Act
            // Assert
            var exception =
                await Assert.ThrowsAsync<ProductNotFoundException>(
                    async () => await fixture.Sut().GetByIdAsync(productId));
            exception.Message.Should().Be($"No product found with id {productId}");
            fixture.MockProductRepository.Verify(x => x.GetByIdAsync(It.IsAny<Guid>()), Times.Once());
            fixture.MockProductRepository.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task GetAllAsync_ReturnsProductList_GivenDataFoundInDb()
        {
            // Arrange
            var fixture = new ProductServiceFixture();
            var productId = Guid.NewGuid();

            var expectedProductList =
                new List<Product>
                {
                    new ProductBuilder()
                        .WithDefaultValues()
                        .WithId(productId)
                        .Build()
                };

            fixture.MockProductRepository.Setup(x => x.GetAllAsync()).ReturnsAsync(expectedProductList);

            // Act
            var result = await fixture.Sut().GetAllAsync();

            // Assert
            result.Should().NotBeNull();
            result.Should().BeEquivalentTo(expectedProductList);
            fixture.MockProductRepository.Verify(x => x.GetAllAsync(), Times.Once());
            fixture.MockProductRepository.VerifyNoOtherCalls();
           
        }

        [Fact]
        public async Task GetAllAsync_ThrowsEntityNotFoundException_GivenNoDataAvailableInDb()
        {
            // Arrange
            var fixture = new ProductServiceFixture();
            fixture.MockProductRepository.Setup(x => x.GetAllAsync()).ReturnsAsync(new List<Product>());

            // Act
            // Assert
            var exception =
                await Assert.ThrowsAsync<EntityNotFoundException>(async () => await fixture.Sut().GetAllAsync());
            exception.Message.Should().Be($"No products available in the store");
            fixture.MockProductRepository.Verify(x => x.GetAllAsync(), Times.Once());
            fixture.MockProductRepository.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task AddAsync_SavesAndReturnsProductId_GivenValidInput()
        {
            // Arrange
            var fixture = new ProductServiceFixture();
            var productId = Guid.NewGuid();
            var inputProduct = new ProductBuilder()
                .WithDefaultValues()
                .WithId(productId)
                .Build();

            fixture.MockProductRepository.Setup(x => x.AddAsync(inputProduct)).ReturnsAsync(productId);

            // Act
            var result = await fixture.Sut().AddAsync(inputProduct);

            // Assert
            productId.Should().Be(result);
            fixture.MockProductRepository.Verify(x => x.AddAsync(inputProduct), Times.Once());
            fixture.MockProductRepository.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task UpdateAsync_UpdatesSuccessfully_GivenValidInput()
        {
            // Arrange
            var fixture = new ProductServiceFixture();
            var productId = Guid.NewGuid();
            var inputProduct = new ProductBuilder()
                .WithDefaultValues()
                .WithId(productId)
                .Build();

            fixture.MockProductRepository.Setup(x => x.UpdateAsync(inputProduct)).ReturnsAsync(1);

            // Act
            await fixture.Sut().UpdateAsync(inputProduct);

            // Assert
            fixture.MockProductRepository.Verify(x => x.UpdateAsync(inputProduct), Times.Once());
            fixture.MockProductRepository.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task UpdateAsync_ThrowsProductNotFoundException_GivenInvalidInput()
        {
            // Arrange
            var fixture = new ProductServiceFixture();
            var productId = Guid.NewGuid();
            var inputProduct = new ProductBuilder()
                .WithDefaultValues()
                .WithId(productId)
                .Build();

            fixture.MockProductRepository.Setup(x => x.UpdateAsync(inputProduct)).ReturnsAsync(0);

            // Act
            // Assert
            var exception =
                await Assert.ThrowsAsync<ProductNotFoundException>(async () => await fixture.Sut().UpdateAsync(inputProduct));
            exception.Message.Should().Be($"No product found with id {productId}");
            fixture.MockProductRepository.Verify(x => x.UpdateAsync(inputProduct), Times.Once());
            fixture.MockProductRepository.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task DeleteAsync_DeletesSuccessfully_GivenValidInput()
        {
            // Arrange
            var fixture = new ProductServiceFixture();
            var productId = Guid.NewGuid();

            fixture.MockProductOptionService.Setup(x => x.DeleteListAsync(productId)).ReturnsAsync(4);
            fixture.MockProductRepository.Setup(x => x.DeleteAsync(productId)).ReturnsAsync(1);

            // Act
            await fixture.Sut().DeleteAsync(productId);

            // Assert
            fixture.MockProductOptionService.Verify(x => x.DeleteListAsync(productId), Times.Once());
            fixture.MockProductRepository.Verify(x => x.DeleteAsync(productId), Times.Once());
            fixture.MockProductRepository.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task DeleteAsync_ThrowsProductNotFoundException_GivenInvalidInput()
        {
            // Arrange
            var fixture = new ProductServiceFixture();
            var productId = Guid.NewGuid();
            var inputProduct = new ProductBuilder()
                .WithDefaultValues()
                .WithId(productId)
                .Build();

            fixture.MockProductOptionService.Setup(x => x.DeleteListAsync(productId)).ReturnsAsync(0);
            fixture.MockProductRepository.Setup(x => x.DeleteAsync(productId)).ReturnsAsync(0);

            // Act
            // Assert
            var exception =
                await Assert.ThrowsAsync<ProductNotFoundException>(async () => await fixture.Sut().DeleteAsync(productId));
            exception.Message.Should().Be($"No product found with id {productId}");
            fixture.MockProductOptionService.Verify(x => x.DeleteListAsync(productId), Times.Once());
            fixture.MockProductRepository.Verify(x => x.DeleteAsync(productId), Times.Once());
            fixture.MockProductRepository.VerifyNoOtherCalls();
        }
    }
}