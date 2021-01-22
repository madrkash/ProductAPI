using System;
using System.Collections.Generic;
using System.Linq;
using ProductStore.IntegrationTests.Framework;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using ProductStore.API.ApiModels;
using ProductStore.Tests.Common.Builders;
using ProductStore.UnitTests.Builders;
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
                Assert.Equal(HttpStatusCode.Created, response.StatusCode);
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
                Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
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
                var response = await client.PostAsync($"/v1/productoptions", updateProductOptionPayload);

                //Assert
                response.EnsureSuccessStatusCode();
                Assert.Equal(HttpStatusCode.Created, response.StatusCode);
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
                Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
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
                Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
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
                var payload =
                    JsonConvert.DeserializeObject<IEnumerable<ProductOptionViewModel>>(
                        await response.Content.ReadAsStringAsync());

                Assert.NotEmpty(payload);
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

                var payload =
                    JsonConvert.DeserializeObject<IEnumerable<ProductOptionViewModel>>(await response.Content.ReadAsStringAsync());
                //Assert
                Assert.NotEmpty(payload);
                Assert.Equal(validProductOptionId, payload.ToList()[0].Id);
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
                Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
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

                var payload =
                    JsonConvert.DeserializeObject<ProductOptionViewModel>(await response.Content.ReadAsStringAsync());
                //Assert
                Assert.NotNull(payload);
                Assert.Equal(validProductOptionId, payload.Id);
            }
        }

        [Fact]
        public async Task GetProductOptionById_ReturnsNotFound_GivenNonExistentProductId()
        {
            using (var client = new TestServerFixture().Client)
            {
                var nonExistentGuid = Guid.NewGuid();
                var response = await client.GetAsync($"/v1/productoptions/{nonExistentGuid}");

                Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
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
                Assert.Equal(HttpStatusCode.Accepted, response.StatusCode);
            }
        }

        [Fact]
        public async Task DeleteProductOption_ReturnsNotFound_GivenNonExistentProductId()
        {
            using (var client = new TestServerFixture().Client)
            {
                var nonExistentGuid = Guid.NewGuid();
                var response = await client.DeleteAsync($"/v1/productoptions/{nonExistentGuid}");

                Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
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