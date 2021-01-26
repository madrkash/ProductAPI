using System;
using System.Collections.Generic;
using System.Linq;
using ProductStore.IntegrationTests.Framework;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Newtonsoft.Json;
using ProductStore.API.Dtos;
using ProductStore.Tests.Common.Builders;
using Xunit;

namespace ProductStore.IntegrationTests.Controllers
{
    public class ProductOptionControllerTests
    {
        [Fact]
        public async Task AddProductOption_ReturnsCreatedResponse_GivenValidPayload()
        {
            using (var client = new TestServerFixture().Client)
            {
                //Arrange
                var validProductId = await GenerateValidProductId(client);

                var validProductOption = new ProductOptionCreateRequestBuilder()
                    .WithDefaultValues()
                    .WithProductId(validProductId)
                    .Build();

                HttpContent content = new StringContent(JsonConvert.SerializeObject(validProductOption), Encoding.UTF8,
                    "application/json");
                //Act
                var response = await client.PostAsync($"/v1/productoptions", content);

                //Assert
                response.EnsureSuccessStatusCode();
                response.StatusCode.Should().Be(HttpStatusCode.Created);
            }
        }

        [Fact]
        public async Task AddProductOption_ReturnsBadRequest_GivenInvalidPayload()
        {
            using (var client = new TestServerFixture().Client)
            {
                //Arrange
                var validProductOption = new ProductOptionCreateRequestBuilder()
                    .WithDefaultValues()
                    .WithEmptyName()
                    .Build();

                HttpContent content = new StringContent(JsonConvert.SerializeObject(validProductOption), Encoding.UTF8,
                    "application/json");

                //Act
                var response = await client.PostAsync($"/v1/productoptions", content);
                //Assert
                response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            }
        }

        [Fact]
        public async Task UpdateProductOption_ReturnsCreatedResponse_GivenValidPayload()
        {
            using (var client = new TestServerFixture().Client)
            {
                //Arrange
                var validProductId = await GenerateValidProductId(client);
                var validProductOptionId = await GenerateValidProductOptionId(client, validProductId);

                var validUpdateProductOption = new ProductOptionUpdateRequestBuilder()
                    .WithDefaultValues()
                    .WithId(validProductOptionId)
                    .WithProductId(validProductId)
                    .WithDescription("Update Product Option Integration testing")
                    .WithName("Integration Testing Update Product Option")
                    .Build();

                HttpContent updateProductOptionPayload = new StringContent(JsonConvert.SerializeObject(validUpdateProductOption),
                    Encoding.UTF8, "application/json");

                //Act
                var response = await client.PutAsync($"/v1/productoptions", updateProductOptionPayload);

                //Assert
                response.EnsureSuccessStatusCode();
                response.StatusCode.Should().Be(HttpStatusCode.Created);
            }
        }

        [Fact]
        public async Task UpdateProductOption_ReturnsNotFound_GivenNonExistentPayload()
        {
            using (var client = new TestServerFixture().Client)
            {
                //Arrange
                var nonExistentProductOption = new ProductOptionUpdateRequestBuilder()
                    .WithDefaultValues()
                    .Build();

                HttpContent content = new StringContent(JsonConvert.SerializeObject(nonExistentProductOption),
                    Encoding.UTF8, "application/json");

                //Act
                var response = await client.PutAsync($"/v1/productoptions", content);
                //Assert
                response.StatusCode.Should().Be(HttpStatusCode.NotFound);
            }
        }

        [Fact]
        public async Task UpdateProduct_ReturnsBadRequest_GivenInvalidPayload()
        {
            using (var client = new TestServerFixture().Client)
            {
                //Arrange
                var invalidProductOption = new ProductOptionUpdateRequestBuilder().Build();

                HttpContent content = new StringContent(JsonConvert.SerializeObject(invalidProductOption),
                    Encoding.UTF8, "application/json");
                //Act
                var response = await client.PutAsync($"/v1/productoptions", content);
                //Assert
                response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            }
        }

        [Fact]
        public async Task GetAllProductOptions_ReturnsSuccessWithProductList()
        {
            using (var client = new TestServerFixture().Client)
            {
                //Act
                var response = await client.GetAsync("/v1/productoptions");

                //Assert
                response.EnsureSuccessStatusCode();
                var result =
                    JsonConvert.DeserializeObject<IEnumerable<ProductOptionResponseDto>>(
                        await response.Content.ReadAsStringAsync());

                result.Should().NotBeNullOrEmpty();
            }
        }

        [Fact]
        public async Task GetProductOptionsByProductId_ReturnsSuccess_GivenValidInput()
        {
            using (var client = new TestServerFixture().Client)
            {
                //Arrange
                var validProductId = await GenerateValidProductId(client);
                var validProductOptionId = await GenerateValidProductOptionId(client, validProductId);

                //Act
                var response = await client.GetAsync($"/v1/Products/{validProductId}/Options");

                response.EnsureSuccessStatusCode();

                var result =
                    JsonConvert.DeserializeObject<IEnumerable<ProductOptionResponseDto>>(await response.Content.ReadAsStringAsync());
                //Assert
                result.Should().NotBeNullOrEmpty();
                result.Single().Id.Should().Be(validProductOptionId);
            }
        }

        [Fact]
        public async Task GetProductOptionsByProductId_ReturnsNotFound_GivenNonExistentProductId()
        {
            using (var client = new TestServerFixture().Client)
            {
                //Arrange
                var nonExistentGuid = Guid.NewGuid();

                //Act
                var response = await client.GetAsync($"/v1/Products/{nonExistentGuid}/Options");

                //Assert
                response.StatusCode.Should().Be(HttpStatusCode.NotFound);
            }
        }

        [Fact]
        public async Task GetProductOptionById_ReturnsSuccess_GivenValidInput()
        {
            using (var client = new TestServerFixture().Client)
            {
                //Arrange
                var validProductId = await GenerateValidProductId(client);
                var validProductOptionId = await GenerateValidProductOptionId(client, validProductId);

                //Act
                var response = await client.GetAsync($"/v1/productoptions/{validProductOptionId}");

                response.EnsureSuccessStatusCode();

                var result =
                    JsonConvert.DeserializeObject<ProductOptionResponseDto>(await response.Content.ReadAsStringAsync());
                //Assert
                result.Should().NotBeNull();
                result.Id.Should().Be(validProductOptionId);
            }
        }

        [Fact]
        public async Task GetProductOptionById_ReturnsNotFound_GivenNonExistentProductId()
        {
            using (var client = new TestServerFixture().Client)
            {
                var nonExistentGuid = Guid.NewGuid();
                var response = await client.GetAsync($"/v1/productoptions/{nonExistentGuid}");

                response.StatusCode.Should().Be(HttpStatusCode.NotFound);
            }
        }

        [Fact]
        public async Task DeleteProductOption_ReturnsAcceptedResponse_GivenValidProductId()
        {
            using (var client = new TestServerFixture().Client)
            {
                //Arrange
                var validProductId = await GenerateValidProductId(client);
                var validProductOptionId = await GenerateValidProductOptionId(client, validProductId);

                //Act
                var response = await client.DeleteAsync($"/v1/productoptions/{validProductOptionId}");

                //Assert
                response.EnsureSuccessStatusCode();
                response.StatusCode.Should().Be(HttpStatusCode.Accepted);
            }
        }

        [Fact]
        public async Task DeleteProductOption_ReturnsNotFound_GivenNonExistentProductId()
        {
            using (var client = new TestServerFixture().Client)
            {
                var nonExistentGuid = Guid.NewGuid();
                var response = await client.DeleteAsync($"/v1/productoptions/{nonExistentGuid}");

                response.StatusCode.Should().Be(HttpStatusCode.NotFound);
            }
        }

        #region Helper Methods

        private static async Task<Guid> GenerateValidProductId(HttpClient client)
        {
            //Add Product
            var validProduct = new ProductCreateRequestBuilder()
                .WithDefaultValues()
                .Build();

            HttpContent addProductPayload = new StringContent(JsonConvert.SerializeObject(validProduct), Encoding.UTF8,
                "application/json");
            var addProductResponse = await client.PostAsync($"/v1/products", addProductPayload);

            addProductResponse.EnsureSuccessStatusCode();

            Guid.TryParse(JsonConvert.DeserializeObject(await addProductResponse.Content.ReadAsStringAsync()).ToString(),
                out var validProductId);
            return validProductId;
        }

        private static async Task<Guid> GenerateValidProductOptionId(HttpClient client, Guid validProductId)
        {
            var validProductOption = new ProductOptionCreateRequestBuilder()
                .WithDefaultValues()
                .WithProductId(validProductId)
                .Build();

            HttpContent content = new StringContent(JsonConvert.SerializeObject(validProductOption), Encoding.UTF8,
                "application/json");
            //Act
            var response = await client.PostAsync($"/v1/productoptions", content);

            //Assert
            response.EnsureSuccessStatusCode();
            Guid.TryParse(JsonConvert.DeserializeObject(await response.Content.ReadAsStringAsync()).ToString(),
                out var validProductOptionId);
            return validProductOptionId;
        }

        #endregion Helper Methods
    }
}