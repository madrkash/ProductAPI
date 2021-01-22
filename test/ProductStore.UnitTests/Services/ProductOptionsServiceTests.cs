using Moq;
using ProductStore.API.Exceptions;
using ProductStore.Core.Models;
using ProductStore.UnitTests.Builders;
using ProductStore.UnitTests.Fixtures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace ProductStore.UnitTests.Services
{
    public class ProductOptionServiceTests
    {
        [Fact]
        public async Task GetByIdAsync_ReturnsProductFromDb_GivenValidProductOptionId()
        {
            // Arrange
            var fixture = new ProductOptionServiceFixture();
            var productOptionId = Guid.NewGuid();
            var expectedProductOption = new ProductOptionBuilder()
                .WithDefaultValues()
                .WithId(productOptionId)
                .Build();

            fixture.MockProductOptionRepository.Setup(
                    x => x.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(expectedProductOption);

            // Act
            var result = await fixture.Sut().GetByIdAsync(productOptionId);

            // Assert
            Assert.NotNull(result);
            fixture.MockProductOptionRepository.Verify(
                x => x.GetByIdAsync(productOptionId), Times.Once());
            Assert.True(result.Equals(expectedProductOption));
        }

        [Fact]
        public async Task GetByIdAsync_ThrowsProductOptionNotFoundException_GivenNonExistentProductOptionId()
        {
            // Arrange
            var fixture = new ProductOptionServiceFixture();
            var productOptionId = Guid.NewGuid();

            fixture.MockProductOptionRepository.Setup(
                    x => x.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync((ProductOption)null);

            // Act
            // Assert
            var exception =
                await Assert.ThrowsAsync<ProductOptionNotFoundException>(async () =>
                    await fixture.Sut().GetByIdAsync(productOptionId));
            Assert.Equal($"No product option found with id {productOptionId}", exception.Message);
        }

        [Fact]
        public async Task GetAllAsync_ReturnsProductOptionList_GivenDataFoundInDb()
        {
            // Arrange
            var fixture = new ProductOptionServiceFixture();
            var productOptionId = Guid.NewGuid();
            var expectedProductOptionList =
                new List<ProductOption>
                {
                    new ProductOptionBuilder()
                        .WithDefaultValues()
                        .WithId(productOptionId)
                        .Build()
                };

            fixture.MockProductOptionRepository.Setup(
                    x => x.GetAllAsync())
                .ReturnsAsync(expectedProductOptionList);

            // Act
            var result = await fixture.Sut().GetAllAsync();

            // Assert
            Assert.NotNull(result);
            fixture.MockProductOptionRepository.Verify(
                x => x.GetAllAsync(), Times.Once());
            Assert.True(result.SequenceEqual(expectedProductOptionList));
        }

        [Fact]
        public async Task GetAllAsync_ThrowsEntityNotFoundException_GivenNoDataAvailableInDb()
        {
            // Arrange
            var fixture = new ProductOptionServiceFixture();
            fixture.MockProductOptionRepository.Setup(
                x => x.GetAllAsync()).ReturnsAsync(new List<ProductOption>());

            // Act
            // Assert
            var exception = await Assert.ThrowsAsync<EntityNotFoundException>(
                async () => await fixture.Sut().GetAllAsync());
            Assert.Equal($"No product options available in the store", exception.Message);
        }

        [Fact]
        public async Task GetAllOptionsByProductIdAsync_ReturnsProductOptionList_GivenDataFoundInDb()
        {
            // Arrange
            var fixture = new ProductOptionServiceFixture();
            var productId = Guid.NewGuid();
            var productOptionId = Guid.NewGuid();
            var expectedProductOptionList =
                new List<ProductOption>
                {
                    new ProductOptionBuilder()
                        .WithDefaultValues()
                        .WithId(productOptionId)
                        .WithProductId(productId)
                        .Build()
                };

            fixture.MockProductOptionRepository.Setup(
                    x => x.GetAllOptionsByProductIdAsync(productId))
                .ReturnsAsync(expectedProductOptionList);

            // Act
            var result = await fixture.Sut().GetAllOptionsByProductIdAsync(productId);

            // Assert
            Assert.NotNull(result);
            fixture.MockProductOptionRepository.Verify(
                x => x.GetAllOptionsByProductIdAsync(productId), Times.Once());
            Assert.True(result.SequenceEqual(expectedProductOptionList));
        }

        [Fact]
        public async Task GetAllOptionsByProductIdAsync_ThrowsProductOptionNotFoundException_GivenNoDataAvailableInDb()
        {
            // Arrange
            var fixture = new ProductOptionServiceFixture();
            var productId = Guid.NewGuid();

            fixture.MockProductOptionRepository.Setup(
                    x => x.GetAllOptionsByProductIdAsync(productId))
                .ReturnsAsync(new List<ProductOption>());

            // Act
            // Assert
            var exception = await Assert.ThrowsAsync<ProductOptionNotFoundException>(async () =>
                await fixture.Sut().GetAllOptionsByProductIdAsync(productId));
            Assert.Equal($"No product options available for product with Id {productId}", exception.Message);
        }

        [Fact]
        public async Task AddAsync_SavesAndReturnsProductOptionId_GivenValidInput()
        {
            // Arrange
            var fixture = new ProductOptionServiceFixture();
            var productOptionId = Guid.NewGuid();
            var inputProductOption = new ProductOptionBuilder()
                .WithDefaultValues()
                .WithId(productOptionId)
                .Build();

            fixture.MockProductOptionRepository.Setup(
                    x => x.AddAsync(inputProductOption))
                .ReturnsAsync(productOptionId);

            // Act
            var result = await fixture.Sut().AddAsync(inputProductOption);

            // Assert
            Assert.Equal(productOptionId, result);
            fixture.MockProductOptionRepository.Verify(
                x => x.AddAsync(inputProductOption), Times.Once());
        }

        [Fact]
        public async Task UpdateAsync_UpdatesSuccessfully_GivenValidInput()
        {
            // Arrange
            var fixture = new ProductOptionServiceFixture();
            var productOptionId = Guid.NewGuid();
            var inputProductOption = new ProductOptionBuilder()
                .WithDefaultValues()
                .WithId(productOptionId)
                .Build();

            fixture.MockProductOptionRepository.Setup(
                x => x.UpdateAsync(inputProductOption)).ReturnsAsync(1);

            // Act
            await fixture.Sut().UpdateAsync(inputProductOption);

            // Assert
            fixture.MockProductOptionRepository.Verify(
                x => x.UpdateAsync(inputProductOption), Times.Once());
        }

        [Fact]
        public async Task UpdateAsync_ThrowsProductOptionNotFoundException_GivenInvalidInput()
        {
            // Arrange
            var fixture = new ProductOptionServiceFixture();
            var productOptionId = Guid.NewGuid();
            var inputProductOption = new ProductOptionBuilder()
                .WithDefaultValues()
                .WithId(productOptionId)
                .Build();

            fixture.MockProductOptionRepository.Setup(
                x => x.UpdateAsync(inputProductOption)).ReturnsAsync(0);

            // Act
            // Assert
            var exception =
                await Assert.ThrowsAsync<ProductOptionNotFoundException>(async () =>
                    await fixture.Sut().UpdateAsync(inputProductOption));
            Assert.Equal($"No product option found with id {productOptionId}", exception.Message);
            fixture.MockProductOptionRepository.Verify(expression:
                x => x.UpdateAsync(inputProductOption),
                Times.Once());
        }

        [Fact]
        public async Task DeleteAsync_DeletesSuccessfully_GivenValidInput()
        {
            // Arrange
            var fixture = new ProductOptionServiceFixture();
            var productOptionId = Guid.NewGuid();

            fixture.MockProductOptionRepository.Setup(
                x => x.DeleteAsync(productOptionId)).ReturnsAsync(1);

            // Act
            await fixture.Sut().DeleteAsync(productOptionId);

            // Assert
            fixture.MockProductOptionRepository.Verify(expression:
                x => x.DeleteAsync(productOptionId), Times.Once());
        }

        [Fact]
        public async Task DeleteAsync_ThrowsProductOptionNotFoundException_GivenInvalidInput()
        {
            // Arrange
            var fixture = new ProductOptionServiceFixture();
            var productOptionId = Guid.NewGuid();

            fixture.MockProductOptionRepository.Setup(
                x => x.DeleteAsync(productOptionId)).ReturnsAsync(0);

            // Act
            // Assert
            var exception =
                await Assert.ThrowsAsync<ProductOptionNotFoundException>(
                    async () => await fixture.Sut().DeleteAsync(productOptionId));
            Assert.Equal($"No product option found with id {productOptionId}", exception.Message);
            fixture.MockProductOptionRepository.Verify(expression:
                x => x.DeleteAsync(productOptionId), Times.Once());
        }

        [Fact]
        public async Task DeleteListAsync_Behaves_GivenAnyInput()
        {
            // Arrange
            var fixture = new ProductOptionServiceFixture();
            var productId = Guid.NewGuid();

            fixture.MockProductOptionRepository.SetupSequence(
                    x => x.DeleteListAsync(productId))
                .ReturnsAsync(1);

            // Act
            await fixture.Sut().DeleteListAsync(productId);

            // Assert
            fixture.MockProductOptionRepository.Verify(expression:
                x => x.DeleteListAsync(productId), Times.Once());
        }
    }
}