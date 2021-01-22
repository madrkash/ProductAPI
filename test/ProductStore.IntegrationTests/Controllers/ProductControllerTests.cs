using System;
using System.Collections.Generic;
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
    public class ProductControllerTests
    {
        [Fact]
        public async Task AddProduct_ReturnsCreatedResponse_GivenValidPayload()
        {
            using (var client = new TestServerFixture().Client)
            {
                var validProduct = new ProductCreateRequestBuilder()
                    .WithDefaultValues()
                    .Build();

                HttpContent content = new StringContent(JsonConvert.SerializeObject(validProduct), Encoding.UTF8,
                    "application/json");
                var response = await client.PostAsync($"/v1/products", content);

                response.EnsureSuccessStatusCode();

                Assert.Equal(HttpStatusCode.Created, response.StatusCode);
            }
        }

        [Fact]
        public async Task AddProduct_ReturnsBadRequest_GivenInvalidPayload()
        {
            using (var client = new TestServerFixture().Client)
            {
                var validProduct = new ProductCreateRequestBuilder()
                    .WithDefaultValues()
                    .WithEmptyName()
                    .Build();

                HttpContent content = new StringContent(JsonConvert.SerializeObject(validProduct), Encoding.UTF8,
                    "application/json");
                var response = await client.PostAsync($"/v1/products", content);

                Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            }
        }

        [Fact]
        public async Task UpdateProduct_ReturnsCreatedResponse_GivenValidPayload()
        {
            using (var client = new TestServerFixture().Client)
            {
                var validAddProduct = new ProductCreateRequestBuilder()
                    .WithDefaultValues()
                    .WithDescription("Integration testing Add Product")
                    .WithName("Integration Testing Add Product")
                    .Build();

                HttpContent addProductPayload = new StringContent(JsonConvert.SerializeObject(validAddProduct),
                    Encoding.UTF8, "application/json");
                var addProductResponse = await client.PostAsync($"/v1/products", addProductPayload);

                addProductResponse.EnsureSuccessStatusCode();

                Guid.TryParse(JsonConvert.DeserializeObject(await addProductResponse.Content.ReadAsStringAsync()).ToString(),
                    out var validProductId);
                var validUpdateProduct = new ProductUpdateRequestBuilder()
                    .WithDefaultValues()
                    .WithId(validProductId)
                    .WithDescription("UpdateProduct Integration testing")
                    .WithName("Integration Testing UpdateProduct")
                    .Build();

                HttpContent updateProductPayload = new StringContent(JsonConvert.SerializeObject(validUpdateProduct),
                    Encoding.UTF8, "application/json");
                var response = await client.PostAsync($"/v1/products", updateProductPayload);

                response.EnsureSuccessStatusCode();

                Assert.Equal(HttpStatusCode.Created, response.StatusCode);
            }
        }

        [Fact]
        public async Task UpdateProduct_ReturnsNotFound_GivenNonExistentPayload()
        {
            using (var client = new TestServerFixture().Client)
            {
                var nonExistentProduct = new ProductUpdateRequestBuilder()
                    .WithDefaultValues()
                    .Build();

                HttpContent content = new StringContent(JsonConvert.SerializeObject(nonExistentProduct),
                    Encoding.UTF8, "application/json");
                var response = await client.PutAsync($"/v1/products", content);

                Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
            }
        }

        [Fact]
        public async Task UpdateProduct_ReturnsBadRequest_GivenInvalidPayload()
        {
            using (var client = new TestServerFixture().Client)
            {
                var validProduct = new ProductUpdateRequestBuilder().Build();

                HttpContent content = new StringContent(JsonConvert.SerializeObject(validProduct),
                    Encoding.UTF8, "application/json");
                var response = await client.PutAsync($"/v1/products", content);

                Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            }
        }

        [Fact]
        public async Task GetAllProducts_ReturnsSuccessWithProductList()
        {
            using (var client = new TestServerFixture().Client)
            {
                var response = await client.GetAsync("/v1/products");

                response.EnsureSuccessStatusCode();

                var payload =
                    JsonConvert.DeserializeObject<IEnumerable<ProductViewModel>>(
                        await response.Content.ReadAsStringAsync());

                Assert.NotEmpty(payload);
            }
        }

        [Fact]
        public async Task GetProductById_ReturnsSuccess_GivenValidInput()
        {
            using (var client = new TestServerFixture().Client)
            {
                var validProduct = new ProductCreateRequestBuilder()
                    .WithDefaultValues()
                    .WithDescription("Integration testing Get ProductById")
                    .WithName("Integration Testing Product")
                    .Build();

                HttpContent content = new StringContent(JsonConvert.SerializeObject(validProduct), Encoding.UTF8,
                    "application/json");
                var addProductResponse = await client.PostAsync($"/v1/products", content);

                addProductResponse.EnsureSuccessStatusCode();

                Guid.TryParse(JsonConvert.DeserializeObject(await addProductResponse.Content.ReadAsStringAsync()).ToString(),
                    out var validProductId);

                var response = await client.GetAsync($"/v1/products/{validProductId}");

                response.EnsureSuccessStatusCode();

                var payload =
                    JsonConvert.DeserializeObject<ProductViewModel>(await response.Content.ReadAsStringAsync());

                Assert.NotNull(payload);
                Assert.Equal(validProductId, payload.Id);
            }
        }

        [Fact]
        public async Task GetProductById_ReturnsNotFound_GivenNonExistentProductId()
        {
            using (var client = new TestServerFixture().Client)
            {
                var nonExistentGuid = Guid.NewGuid();
                var response = await client.GetAsync($"/v1/products/{nonExistentGuid}");

                Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
            }
        }

        [Fact]
        public async Task DeleteProduct_ReturnsAcceptedResponse_GivenValidProductId()
        {
            using (var client = new TestServerFixture().Client)
            {
                var validAddProduct = new ProductCreateRequestBuilder()
                    .WithDefaultValues()
                    .WithDescription("Integration testing Delete Product")
                    .WithName("Integration Testing Delete Product")
                    .Build();

                HttpContent addProductPayload = new StringContent(JsonConvert.SerializeObject(validAddProduct),
                    Encoding.UTF8, "application/json");
                var addProductResponse = await client.PostAsync($"/v1/products", addProductPayload);

                addProductResponse.EnsureSuccessStatusCode();

                Guid.TryParse(JsonConvert.DeserializeObject(await addProductResponse.Content.ReadAsStringAsync()).ToString(),
                    out var validProductId);
                var response = await client.DeleteAsync($"/v1/products/{validProductId}");

                response.EnsureSuccessStatusCode();

                Assert.Equal(HttpStatusCode.Accepted, response.StatusCode);
            }
        }

        [Fact]
        public async Task DeleteProduct_ReturnsNotFound_GivenNonExistentProductId()
        {
            using (var client = new TestServerFixture().Client)
            {
                var nonExistentGuid = Guid.NewGuid();
                var response = await client.DeleteAsync($"/v1/products/{nonExistentGuid}");

                Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
            }
        }
    }
}