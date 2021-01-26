using FluentValidation.TestHelper;
using ProductStore.API.Dtos;
using ProductStore.API.Validators;
using Xunit;

namespace ProductStore.UnitTests.Validators
{
    public class ProductCreateRequestValidatorShould
    {
        private readonly ProductCreateRequestValidator _validator = new ProductCreateRequestValidator();

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("   ")]
        public void Have_Error_When_Name_Is_Invalid(string invalidName)
        {
            _validator.ShouldHaveValidationErrorFor(productUpdateRequest => productUpdateRequest.Name, invalidName);
        }

        [Fact]
        public void Have_Error_When_Price_Is_Zero()
        {
            _validator.ShouldHaveValidationErrorFor(productUpdateRequest => productUpdateRequest.Price, 0);
        }

        [Fact]
        public void Have_Error_When_DeliveryPrice_Is_Zero()
        {
            _validator.ShouldHaveValidationErrorFor(productUpdateRequest => productUpdateRequest.DeliveryPrice, 0);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("   ")]
        public void Have_Error_When_Description_Is_Invalid(string invalidDescription)
        {
            _validator.ShouldHaveValidationErrorFor(productUpdateRequest => productUpdateRequest.Description, invalidDescription);
        }

        [Fact]
        public void Have_Multiple_Errors_When_Price_DeliveryPrice_Are_Not_Provided()
        {
            var productCreateRequest = new ProductCreateRequestDto
            {
                Description = "Testing",
                Name = "Test"
            };

            var result = _validator.TestValidate(productCreateRequest);

            Assert.Equal(2, result.Errors.Count);
            result.ShouldHaveValidationErrorFor(request => request.Price);
            result.ShouldHaveValidationErrorFor(request => request.DeliveryPrice);
        }

        [Fact]
        public void Not_Have_Error_When_Valid_Request_Is_Empty()
        {
            var productCreateRequest = new ProductCreateRequestDto
            {
                Description = "Testing",
                Name = "Test",
                Price = 10,
                DeliveryPrice = 1
            };

            var result = _validator.TestValidate(productCreateRequest);

            Assert.Empty(result.Errors);
        }
    }
}